/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameUtilities;

public class UIP_LobbyController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Variables
    public static UIP_LobbyController lobby;

    //Buttons
    [SerializeField]
    private GameObject hostRoomButton;
    [SerializeField]
    private GameObject joinRoomButton;
    [SerializeField]
    private GameObject hostRoomBackButton;
    [SerializeField]
    private GameObject joinRoomBackButton;
    [SerializeField]
    private GameObject multiplayerMenuBackButton;
    [SerializeField]
    private GameObject disconnectedBackButton;

    //Panels
    [SerializeField]
    private GameObject multiplayerMenuPanel;
    [SerializeField]
    private GameObject JoinGamePanel;
    [SerializeField]
    private GameObject HostGamePanel;
    [SerializeField]
    private GameObject CharacterSelectionLobbyPanel;
    [SerializeField]
    private GameObject DisconnectedPanel;
    [SerializeField]
    private GameObject LoadingPanel;

    //Misc
    [SerializeField]
    private GameObject roomListingPrefab; //prefab for displayer each room in the lobby
    [SerializeField]
    private Transform roomsContainer; //container for holding all the room listings

    [SerializeField]
    private InputField playerNameInput; //Input field so player can change their NickName


    private string roomName; //string for saving room name, is usually the Players name or Players name + numbers
    public bool intentionalDisconnect = false;


    private List<RoomInfo> roomList; //list of current rooms
    #endregion

    #region AwakeStartUpdate
    private void Awake()
    {
        lobby = this;

        //This line is needed in order for Photon to know how to send types that aren't your basic primitive types (int, char, bool, etc)
        PhotonPeer.RegisterType(typeof(Coordinates), 1, Coordinates.Serialize, Coordinates.Deserialize);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
            StartCoroutine(DisconnectReconnect());

        PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.AutomaticallySyncScene == false)
            PhotonNetwork.AutomaticallySyncScene = true;

    }

    private void Update()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.Disconnected && intentionalDisconnect == false)
        {
            if (HostGamePanel.activeSelf)
            {
                HostGamePanel.SetActive(false);
            }
            if (JoinGamePanel.activeSelf)
            {
                JoinGamePanel.SetActive(false);
            }
            if (CharacterSelectionLobbyPanel.activeSelf)
            {
                CharacterSelectionLobbyPanel.SetActive(false);
            }
            if (LoadingPanel.activeSelf)
            {
                LoadingPanel.SetActive(false);
            }
            if (multiplayerMenuPanel.activeSelf)
            {
                multiplayerMenuPanel.SetActive(false);
            }

            DisconnectedPanel.SetActive(true);
        }
    }

    IEnumerator DisconnectReconnect()
    {
        intentionalDisconnect = true;
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
            yield return null;
    }
    #endregion

    #region PUNCallbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        intentionalDisconnect = false;
        multiplayerMenuPanel.SetActive(true);
        roomList = new List<RoomInfo>();

        //=============================================================================
        // Callback function for when the first PhotonNetwork connection is
        // successfully established.
        //=============================================================================
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
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

        //=============================================================================
        // On some occassions, the creation of a room may fail to occur. The most common
        // reason for this is due to there already being an exisiting room by that name.
        // This function addresses this occurence by creating another room under that
        // same name but with a random addition of numbers at the end.
        //=============================================================================
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        //hide current panel, show disconnected panel

        intentionalDisconnect = true;
        PhotonNetwork.Disconnect();
    }

    //Once the player has connect to the PhotonNetwork and is in a Lobby this function is called every time there is an update to the room list
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
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
    #endregion

    #region Functions
    public void PlayerNameUpdateInputChanged()
    {
        string text = playerNameInput.text;
        if (!string.IsNullOrWhiteSpace(text))
        {
            hostRoomButton.SetActive(true);
            joinRoomButton.SetActive(true);
        }
        else if (string.IsNullOrWhiteSpace(text) && PlayerPrefs.GetString("NickName") == "")
        {
            hostRoomButton.SetActive(false);
            joinRoomButton.SetActive(false);
        }

        PhotonNetwork.NickName = text;
        PlayerPrefs.SetString("NickName", text);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now");
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2,
            BroadcastPropsChangeToAll = true
        };

        PhotonNetwork.CreateRoom(roomName, roomOps);
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
    #endregion

    #region Buttons
    public void HostLobbyOnClick()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        multiplayerMenuPanel.SetActive(false);
        roomName = "Join " + PlayerPrefs.GetString("NickName")+ "'s room";

        CharacterSelectionLobbyPanel.SetActive(true);
        FindObjectOfType<AudioManager>().StopCurrentSong(4);

        // Call to function in "GameUtilities.cs"
        SetPlayerTurn(PlayerTurn.ONE);
        setP1username(roomName);

        //NameOfHost.text = roomName;
        CreateRoom();
    }

    public void MultiplayerMenuBackButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            intentionalDisconnect = true;
            PhotonNetwork.Disconnect(); ;
        }

        Debug.Log("Are we connected to PhotonNetwork? " + PhotonNetwork.NetworkClientState);
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        SceneManager.LoadScene("Menu");
    }

    public void DisconnectedBackButton()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        SceneManager.LoadScene("Menu");
    }

    public void HostGameBackButton()
    {
        multiplayerMenuPanel.SetActive(true);
        CharacterSelectionLobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }

    public void JoinLobbyOnClick() //Paired to the Join button
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        multiplayerMenuPanel.SetActive(false);
        JoinGamePanel.SetActive(true);
        PhotonNetwork.JoinLobby();
        FindObjectOfType<AudioManager>().StopCurrentSong(4);

        // Call to function in "GameUtilities.cs"
        SetPlayerTurn(PlayerTurn.TWO);
        setP2username(roomName);
    }

    public void JoinGameBackButton() //Paired to the host game panels back button. Used to go back to the main menu
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        multiplayerMenuPanel.SetActive(true);
        JoinGamePanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
    #endregion
}


