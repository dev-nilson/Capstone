using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RoomController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static RoomController room;

    public bool intentionalDC = false;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;

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

    //private void Update()
    //{
    //    if (PhotonNetwork.NetworkClientState == ClientState.Disconnected && intentionalDisconnect == false)
    //    {
    //        if (LoadingCanvas.gameObject.activeSelf)
    //            LoadingCanvas.gameObject.SetActive(false);
    //        if (CreateOrJoinCanvas.gameObject.activeSelf)
    //            CreateOrJoinCanvas.gameObject.SetActive(false);
    //        if (RoomLobbyCanvas.gameObject.activeSelf)
    //            RoomLobbyCanvas.gameObject.SetActive(false);
    //        if (WaitingLoadingCanvas.gameObject.activeSelf)
    //            WaitingLoadingCanvas.gameObject.SetActive(false);

    //        DisconnectCanvas.gameObject.SetActive(true);
    //    }
    //}

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
        intentionalDisconnect = false;
        Debug.Log("OnConnectedToMaster succesfully entered");

        if (!CreateOrJoinCanvas.gameObject.activeSelf)
            CreateOrJoinCanvas.gameObject.SetActive(true);

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

        roomName = GameInfo.username + Random.Range(0, 100);
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        if (RoomLobbyCanvas.gameObject.activeSelf)
            RoomLobbyCanvas.gameObject.SetActive(false);
        if (WaitingLoadingCanvas.gameObject.activeSelf)
            WaitingLoadingCanvas.gameObject.SetActive(false);

        intentionalDisconnect = true;
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
        CreateOrJoinCanvas.gameObject.SetActive(false);
        RoomLobbyCanvas.gameObject.SetActive(false);

        WaitingLoadingCanvas.gameObject.SetActive(true);
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            StatusText.text = "Connected to room, waiting for host to start game...";
        }

        WaitingLoadingBackButton.gameObject.SetActive(true);
        StartButton.gameObject.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (intentionalDisconnect == true)
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                StatusText.text = "Player joined, ready to Start Game...";
                StartButton.SetActive(true);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            if (GameInfo.selectPieceAtStart == 2)
            {
                StatusText.text = "Host has left, press Back to leave...";
            }
            else
            {
                StatusText.text = "Player left, waiting for new player to join...";
                StartButton.SetActive(false);
                PhotonNetwork.CurrentRoom.IsOpen = true;
                PhotonNetwork.CurrentRoom.IsVisible = true;
            }
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Successfully left room.");

        WaitingLoadingCanvas.gameObject.SetActive(false);
        CreateOrJoinCanvas.gameObject.SetActive(true);

        CreateGameButton.SetActive(false);
        JoinGameButton.SetActive(false);
        CreateOrJoinBackButton.SetActive(false);
        LoadingCanvas.gameObject.SetActive(true);

        //Debug.Log("PhotonNetwork.Disconnect() called");
        PhotonNetwork.LeaveLobby();
    }
    #endregion

    #region ILobbyCallbacks
    public void OnJoinedLobby()
    {
        throw new System.NotImplementedException();
    }

    public void OnLeftLobby()
    {
        throw new System.NotImplementedException();
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        throw new System.NotImplementedException();
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        throw new System.NotImplementedException();
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

        roomName = GameInfo.username;
        PhotonNetwork.CreateRoom(roomName, roomOps);    // -> OnCreatedRoom / OnCreateRoomFailed
    }

    public void RemoveRoomListings()
    {
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    public void ListRoom(RoomInfo room)
    {

        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.SetRoom();

            roomListEntries.Add(room.Name, tempListing);
        }
    }


    #endregion

    #region Buttons

    /********************************************************************
     * Calls the create room function, when Create Game Button is clicked
     * GameInfo.slectPieceAtStart, ensures that the Host moves first
     * when the game starts
     ********************************************************************/
    public void OnCreateGameButtonClicked()
    {
        CreateRoom();

        GameInfo.selectPieceAtStart = 1;
    }

    /********************************************************************
    * GameInfo.selectPieceAtStart, ensures that the Client moves second
    * when the game starts
    ********************************************************************/
    public void OnJoinGameButtonClicked()
    {
        CreateOrJoinCanvas.gameObject.SetActive(false);
        RoomLobbyCanvas.gameObject.SetActive(true);

        roomListingPanel.gameObject.SetActive(true);

        GameInfo.selectPieceAtStart = 2;
    }

    public void OnCreateOrJoinBackButtonClicked()
    {
        intentionalDisconnect = true;
        PhotonNetwork.Disconnect();
        GameInfo.gameType = 'N';
        Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void OnRoomLobbyBackButtonClicked()
    {
        RoomLobbyCanvas.gameObject.SetActive(false);
        CreateOrJoinCanvas.gameObject.SetActive(true);

        CreateGameButton.SetActive(true);
        JoinGameButton.SetActive(true);
        CreateOrJoinBackButton.SetActive(true);

        // don't need to dc here
    }

    public void OnWaitingLoadingBackButtonClicked()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;

            PhotonNetwork.LeaveRoom();  // -> OnLeftRoom 
        }
        else
            PhotonNetwork.LeaveRoom();  // -> OnLeftRoom  

        StatusText.text = "Waiting for opponent...";
        intentionalDisconnect = true;
        PhotonNetwork.Disconnect();


        //GameInfo.gameType = 'N';
        //Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void OnDisconnectedBackButtonClicked()
    {
        GameInfo.gameType = 'N';
        Initiate.Fade("UserPreferences", Color.black, 4.0f);
    }

    public void OnStartButtonClicked()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    #endregion
}
