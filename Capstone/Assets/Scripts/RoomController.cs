using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; //temporary using statement

public class RoomController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static RoomController room;

    //Buttons
    public GameObject HostGameButton; //button used for joining a Lobby as the host.
    [SerializeField]
    private GameObject HostGameBackButton;
    [SerializeField]
    private GameObject JoinGameButton; //button used for joining a Lobby as the client.
    [SerializeField]
    private GameObject JoinGameBackButton;
    [SerializeField]
    private GameObject FaceOffBackButton; //Button used for going back to multi menu
    [SerializeField]
    private GameObject StartGameButton; //starts game
    [SerializeField]
    private GameObject ReadyUpButton; //once both players are ready leads into faceoff screen
    [SerializeField]
    private GameObject MultiplayerBackButton; //back button to main menu


    //Panels
    [SerializeField]
    private GameObject MultiplayerMenuPanel; //panel for displaying the main menu
    [SerializeField]
    private GameObject HostGamePanel; //panel for displaying host lobby.
    [SerializeField]
    private GameObject JoinGamePanel; //panel for displaying join lobby.
    [SerializeField]
    private GameObject CharacterSelectionLobbyPanel; //panel for displaying host lobby.
    [SerializeField]
    private GameObject DisconnectedPanel; //panel for displaying host lobby.
    [SerializeField]
    private GameObject LoadingPanel; //panel for displaying host lobby.


    [SerializeField]
    private GameObject roomListingPrefab; //prefab for displayer each room in the lobby
    [SerializeField]
    private Transform roomsContainer; //container for holding all the room listings
    [SerializeField]
    private InputField playerNameInput; //Input field so player can change their NickName

    public string roomName;

    public bool intentionalDC = false;

    private List<RoomInfo> RoomList;
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;


    #region TempVariables
    [SerializeField]
    private Text NameOfHost; //text object for displaying the host of a room, replaced by GameInfo.username

    public int whoMovesFirst;
    #endregion

    #region AwakeStartUpdate

    private void Awake()
    {
        room = this;

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, GameObject>();

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

    /********************************************************************
    * If the player cannot connect to the Photon Netowkr or intentionally
    * disconnects from it, everything currently showing will be hidden
    * and the disconnectCanvas will be shown
    ********************************************************************/
    private void Update()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.Disconnected && intentionalDC == false)
        {
            if (MultiplayerMenuPanel.gameObject.activeSelf)
                MultiplayerMenuPanel.gameObject.SetActive(false);
            if (JoinGamePanel.gameObject.activeSelf)
                JoinGamePanel.gameObject.SetActive(false);
            if (HostGamePanel.gameObject.activeSelf)
                HostGamePanel.gameObject.SetActive(false);
            if (CharacterSelectionLobbyPanel.gameObject.activeSelf)
                CharacterSelectionLobbyPanel.gameObject.SetActive(false);

            DisconnectedPanel.gameObject.SetActive(true);
        }
    }

    IEnumerator DisconnectReconnect()
    {
        intentionalDC = true;
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
            yield return null;
    }
    #endregion

    #region Puncallbacks
    public override void OnConnectedToMaster()
    {
        intentionalDC = false;
        Debug.Log("OnConnectedToMaster succesfully entered");

        if (!MultiplayerMenuPanel.gameObject.activeSelf)
            MultiplayerMenuPanel.gameObject.SetActive(true);

        PhotonNetwork.JoinLobby();  // -> OnJoinedLobby
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.AutomaticallySyncScene == false)
            PhotonNetwork.AutomaticallySyncScene = true;    // -> OnJoinedRoom
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        RoomOptions roomOps = new RoomOptions()
        {
            EmptyRoomTtl = 1,
            PlayerTtl = 1,
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        roomName = PlayerPrefs.GetString("NickName") + Random.Range(0, 100) + "'s Room";
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        if (JoinGamePanel.gameObject.activeSelf)
            JoinGamePanel.gameObject.SetActive(false);
        if (HostGamePanel.gameObject.activeSelf)
            HostGamePanel.gameObject.SetActive(false);

        intentionalDC = true;
        PhotonNetwork.Disconnect();
    }

    /********************************************************************
* OnJoinedRoom is called when the local player joins the room.
* The first two lines of code are there to deactivate the canvas/panels
* that were there while the local player was trying/waiting to find 
* a game.
* The line after that then activates a loading screen for the player
* and if they are not the MasterClient (the Host) sends them a 
* message letting them know they are waiting on the host to start
* the game
* A back button is then also made active and the start button for that 
* screen is deactivated because the player is not the host, thus
* they don't need to be able to start the game
********************************************************************/
    public override void OnJoinedRoom()
    {
        JoinGamePanel.gameObject.SetActive(false);

        HostGamePanel.gameObject.SetActive(true);

        HostGameBackButton.gameObject.SetActive(true);
        ReadyUpButton.gameObject.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (intentionalDC == true)
            PhotonNetwork.ConnectUsingSettings();
        else
            Debug.Log("Loss of connection here");
    }

    private void ClearRoomListView()
    {
        foreach (GameObject entry in roomListEntries.Values)
        {
            Destroy(entry.gameObject);
        }
        roomListEntries.Clear();
    }

    private void UpdateRoomListView()
    {
        foreach (RoomInfo Item in cachedRoomList.Values)
        {
            ListRoom(Item);
        }
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }
                continue;
            }

            // Update cached room info
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Player Entered Room!");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                ReadyUpButton.SetActive(true);
                Debug.Log("Time to ready up!");
            }
        }
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            if (whoMovesFirst == 2)
            {
                Debug.Log("Host has left, press Back to leave...");
            }
            else
            {
                Debug.Log("Player left, waiting for new player to join...");
                ReadyUpButton.SetActive(false);
                PhotonNetwork.CurrentRoom.IsOpen = true;
                PhotonNetwork.CurrentRoom.IsVisible = true;
            }
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Successfully left room.");

        HostGamePanel.gameObject.SetActive(false);
        MultiplayerMenuPanel.gameObject.SetActive(true);

        HostGameButton.SetActive(false);
        JoinGameButton.SetActive(false);
        MultiplayerBackButton.SetActive(false);
        LoadingPanel.gameObject.SetActive(true);

        //Debug.Log("PhotonNetwork.Disconnect() called");
        PhotonNetwork.LeaveLobby();
    }
    #endregion

    #region ILobbyCallbacks
    public override void OnJoinedLobby()
    {
        HostGameButton.SetActive(true);
        JoinGameButton.SetActive(true);
        MultiplayerBackButton.SetActive(false);
        LoadingPanel.gameObject.SetActive(true);
    }

    public override void OnLeftLobby()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
        print("Inside OnRoomListUpdate");
    }
    #endregion

    #region Functions
    public void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions()
        {
            EmptyRoomTtl = 1,
            PlayerTtl = 1,
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        roomName = PlayerPrefs.GetString("NickName") + "'s Room";
        PhotonNetwork.CreateRoom(roomName, roomOps);    // -> OnCreatedRoom / OnCreateRoomFailed
    }

    public void RemoveRoomListings()
    {
        while (roomsContainer.childCount != 0)
        {
            Destroy(roomsContainer.GetChild(0).gameObject);
        }
    }

    public void ListRoom(RoomInfo room)
    {

        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.SetRoom();

            roomListEntries.Add(room.Name, tempListing);
        }
    }


    #endregion

    #region Buttons

    /********************************************************************
     * Calls the create room function, when Host Game Button is clicked
     * GameInfo.slectPieceAtStart, ensures that the Host moves first
     * when the game starts
     ********************************************************************/
    public void OnHostGameButtonClicked()
    {
        MultiplayerMenuPanel.SetActive(false);

        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "User" + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "User" + Random.Range(0, 1000);
        }

        HostGamePanel.SetActive(true);
        CreateRoom();
        whoMovesFirst = 1;
    }

    /********************************************************************
    * GameInfo.selectPieceAtStart, ensures that the Client moves second
    * when the game starts
    ********************************************************************/
    public void OnJoinGameButtonClicked()
    {
        MultiplayerMenuPanel.gameObject.SetActive(false);
        JoinGamePanel.gameObject.SetActive(true);

        //roomListingPanel.gameObject.SetActive(true);

        whoMovesFirst = 2;
    }

    public void MultiplayerMenuBackButtonClicked()
    {
        intentionalDC = true;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
        //GameInfo.gameType = 'N';
        //Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void OnJoinGameBackButtonClicked()
    {
        JoinGamePanel.gameObject.SetActive(false);
        MultiplayerMenuPanel.gameObject.SetActive(true);

        HostGameButton.SetActive(true);
        JoinGameButton.SetActive(true);
        MultiplayerBackButton.SetActive(true);

        // don't need to dc here
    }

    public void HostGameBackButtonClicked()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;

            PhotonNetwork.LeaveRoom();  // -> OnLeftRoom
            HostGamePanel.gameObject.SetActive(false);
            MultiplayerMenuPanel.gameObject.SetActive(false);
            LoadingPanel.gameObject.SetActive(true);

            //HostGameButton.SetActive(true);
            //JoinGameButton.SetActive(true);
            //MultiplayerBackButton.SetActive(true);
        }
        else
            PhotonNetwork.LeaveRoom();  // -> OnLeftRoom  

        //StatusText.text = "Waiting for opponent...";
        intentionalDC = true;
        PhotonNetwork.Disconnect();


        //GameInfo.gameType = 'N';
        //Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    //public void OnDisconnectedBackButtonClicked()
    //{
    //    GameInfo.gameType = 'N';
    //    Initiate.Fade("UserPreferences", Color.black, 4.0f);
    //}

    public void OnStartButtonClicked()
    {
        PhotonNetwork.LoadLevel("GameBoard");
    }

    #endregion

    #region TempLogic
    public void PlayerNameUpdateInputChanged(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        Debug.Log(nameInput);
        PlayerPrefs.SetString("NickName", nameInput);

        /********************************************************************
        * Had a weird bug with this in Unity. For some reason it would not 
        * show my input, even in DebugLog()
        * Deleting the inputfield and re-adding it fixed the problem
         ********************************************************************/
    }
    #endregion
}
