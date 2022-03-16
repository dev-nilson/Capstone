using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtilities;

public class UIP_LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static UIP_LobbyController lobby;

    [SerializeField]
    private GameObject hostRoomButton; //button used for joining a Lobby as the host.
    [SerializeField]
    private GameObject joinRoomButton; //button used for joining a Lobby as the client.
    [SerializeField]
    private GameObject hostRoomBackButton; //button used for joining a Lobby as the host.
    [SerializeField]
    private GameObject joinRoomBackButton; //button used for joining a Lobby as the client.


    [SerializeField]
    private GameObject multiplayerMenuPanel; //panel for displaying the main menu
    [SerializeField]
    private GameObject JoinGamePanel; //panel for displaying join lobby.
    [SerializeField]
    private GameObject HostGamePanel; //panel for displaying host lobby.
    [SerializeField]
    private GameObject CharacterSelectionLobbyPanel;

    [SerializeField]
    private GameObject roomListingPrefab; //prefab for displayer each room in the lobby
    [SerializeField]
    private Transform roomsContainer; //container for holding all the room listings

    [SerializeField]
    private InputField playerNameInput; //Input field so player can change their NickName
    //[SerializeField]
    //private Text NameOfHost; //text object for displaying the host of a room, replaced by GameInfo.username


    private string roomName; //string for saving room name, is usually the Players name or Players name + numbers
    public bool intentionalDisconnect = false;


    private List<RoomInfo> roomList; //list of current rooms
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;

    private void Awake()
    {
        lobby = this;

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, GameObject>();
        PhotonPeer.RegisterType(typeof(Coordinates), 1, Coordinates.Serialize, Coordinates.Deserialize);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
            StartCoroutine(DisconnectReconnect());

        PhotonNetwork.ConnectUsingSettings();   // -> OnConnectedToMaster

        if (PhotonNetwork.AutomaticallySyncScene == false)
            PhotonNetwork.AutomaticallySyncScene = true;

    }

    IEnumerator DisconnectReconnect()
    {
        intentionalDisconnect = true;
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
            yield return null;
    }

    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        hostRoomButton.SetActive(true); //activate button for connecting to lobby
        joinRoomButton.SetActive(true); //activate button for connecting to lobby
        roomList = new List<RoomInfo>(); //initializing roomListing

        //check for player name saved to player prefs
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0, 1000); //random player name when not set
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName"); //get saved player name
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000); //random player name when not set
        }
        playerNameInput.text = PhotonNetwork.NickName; //update input field with player name
        Debug.Log(playerNameInput.text);
        PlayerPrefs.SetString("NickName", PhotonNetwork.NickName);
    }

    public void PlayerNameUpdateInputChanged(string nameInput) //input function for player name. paired to player name input field
    {
        PhotonNetwork.NickName = nameInput;
        Debug.Log(nameInput);
        PlayerPrefs.SetString("NickName", nameInput);
        //Had a weird bug with this in Unity
        //For some reason it would not show/keep track of my input, even in DebugLog()
        //Deleting the inputfield and re-adding it fixed the problem
    }

    public void HostLobbyOnClick() //Paired to the Host button
    {
        multiplayerMenuPanel.SetActive(false);

        Debug.Log(PhotonNetwork.NickName);
        roomName = PlayerPrefs.GetString("NickName") + "'s Room";

        Debug.Log(roomName);
        CharacterSelectionLobbyPanel.SetActive(true);

        // Call to function in "GameUtilities.cs"
        SetPlayerTurn(PlayerTurn.ONE);

        //NameOfHost.text = roomName;
        CreateRoom();
    }

    public void HostGameBackButton() //Paired to the host game panels back button. Used to go back to the main menu
    {
        multiplayerMenuPanel.SetActive(true);
        CharacterSelectionLobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }

    public void JoinLobbyOnClick() //Paired to the Join button
    {
        multiplayerMenuPanel.SetActive(false);
        JoinGamePanel.SetActive(true);
        PhotonNetwork.JoinLobby(); //First tries to join a lobby

        // Call to function in "GameUtilities.cs"
        SetPlayerTurn(PlayerTurn.TWO);
    }

    public void JoinGameBackButton() //Paired to the host game panels back button. Used to go back to the main menu
    {
        multiplayerMenuPanel.SetActive(true);
        JoinGamePanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom() //function paired to the host room button
    {
        Debug.Log("Creating room now");
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2,
            BroadcastPropsChangeToAll = true
        };

        PhotonNetwork.CreateRoom(roomName, roomOps); //attempting to create a new room
    }

    /*
     * create room will fail if room already exists
     * this most often happens when there is already a room by that name
     * This function will take the username that was the same, and try
     * to create a new room under that same username + a random addition of numbers
     */
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);

        RoomOptions roomOps = new RoomOptions()
        {
            EmptyRoomTtl = 1,
            PlayerTtl = 1,
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2,
            BroadcastPropsChangeToAll = true
        };

        roomName = PlayerPrefs.GetString("NickName") + Random.Range(0, 100);
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //Once in lobby this function is called every time there is an update to the room list
    {
        Debug.Log("OnRoomListUpdate was called");
        int tempIndex;
        foreach (RoomInfo room in roomList) //loop through each room in room list
        {
            Debug.Log("Looping through room list...");
            if (this.roomList != null) //try to find existing room listing
            {
                tempIndex = this.roomList.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1) //remove listing because it has been closed
            {
                this.roomList.RemoveAt(tempIndex);
                Destroy(roomsContainer.GetChild(tempIndex).gameObject);
                Debug.Log("Room removed from Listing");
            }
            if (room.PlayerCount > 0) //add room listing because it is new
            {
                this.roomList.Add(room);
                ListRoom(room);
                Debug.Log("Room added to Listing");
                Debug.Log("The name of the room added was: " + room.Name);
            }
        }
    }

    public override void OnJoinedRoom()//called when the local player joins the room
    {
        for (int i = roomsContainer.childCount - 1; i == 0; i--)
        {
            Destroy(roomsContainer.GetChild(i).gameObject);
        }
    }

    static System.Predicate<RoomInfo> ByName(string name) //predicate function for seach through room list
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room) //displays new room listing for the current room
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            UIP_RoomButton tempButton = tempListing.GetComponent<UIP_RoomButton>();
            tempButton.SetRoom(room.Name);
        }
    }
}


