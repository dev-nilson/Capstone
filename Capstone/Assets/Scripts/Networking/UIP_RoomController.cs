/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static GameUtilities;

public class UIP_RoomController : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField]
    private GameObject JoinGamePanel; //display for when in lobby
    [SerializeField]
    private GameObject HostGamePanel; //display for when in room
    [SerializeField]
    private GameObject CharacterSelectionLobbyPanel;
    [SerializeField]
    private GameObject multiplayerMenuPanel;

    [SerializeField]
    private GameObject StartGameButton; //only for the master client. used to start the game and load the multiplayer scene
    [SerializeField]
    private GameObject FaceoffBackButton;
    [SerializeField]
    private GameObject ReadyUpButton;
    [SerializeField]
    private GameObject DisabledStartButton;

    [SerializeField]
    private GameObject PharoahButton;
    [SerializeField]
    private GameObject ScribeButton;
    [SerializeField]
    private GameObject WorkerButton;
    [SerializeField]
    private GameObject PeasantButton;

    [SerializeField]
    private Text WaitingForOpponents; //Updates as people leave/join the room to reflect room "state" (if you're waiting for someone to join or not)

    public List<NetPlayerItem> playerItemsList = new List<NetPlayerItem>();
    public NetPlayerItem playerItemPrefab;
    public Transform playerItemParent;

    private NetPlayerItem tempPlayer;
    private int localPlayerReady = 0;
    private bool readyUpStatus;
    #endregion

    #region StartUpdateLateUpdate
    private void Start()
    {
        SceneManager.activeSceneChanged += OnGameStarting;
    }

    private void Update()
    {
        if (CharacterSelectionLobbyPanel.activeInHierarchy)
        {
            if (tempPlayer.OpponentAlien() == "PHAROAH")
            {
                PharoahButton.SetActive(false);
                if (tempPlayer.alienChosen == "SCRIBE")
                {
                    WorkerButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "WORKER")
                {
                    ScribeButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "PEASANT")
                {
                    ScribeButton.SetActive(true);
                    WorkerButton.SetActive(true);
                }
            }
            else if (tempPlayer.OpponentAlien() == "SCRIBE")
            {
                ScribeButton.SetActive(false);
                if (tempPlayer.alienChosen == "PHAROAH")
                {
                    WorkerButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "WORKER")
                {
                    PharoahButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "PEASANT")
                {
                    PharoahButton.SetActive(true);
                    WorkerButton.SetActive(true);
                }
            }
            else if (tempPlayer.OpponentAlien() == "WORKER")
            {
                WorkerButton.SetActive(false);
                if (tempPlayer.alienChosen == "PHAROAH")
                {
                    ScribeButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "SCRIBE")
                {
                    PharoahButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "PEASANT")
                {
                    ScribeButton.SetActive(true);
                    PharoahButton.SetActive(true);
                }
            }
            else if (tempPlayer.OpponentAlien() == "PEASANT")
            {
                PeasantButton.SetActive(false);
                if (tempPlayer.alienChosen == "PHAROAH")
                {
                    WorkerButton.SetActive(true);
                    ScribeButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "SCRIBE")
                {
                    PharoahButton.SetActive(true);
                    WorkerButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "WORKER")
                {
                    PharoahButton.SetActive(true);
                    ScribeButton.SetActive(true);
                }
            }
            else if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
            {
                if (tempPlayer.alienChosen == "PHAROAH")
                {
                    WorkerButton.SetActive(true);
                    ScribeButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "SCRIBE")
                {
                    PharoahButton.SetActive(true);
                    WorkerButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "WORKER")
                {
                    PharoahButton.SetActive(true);
                    ScribeButton.SetActive(true);
                    PeasantButton.SetActive(true);
                }
                else if (tempPlayer.alienChosen == "PEASANT")
                {
                    ScribeButton.SetActive(true);
                    PharoahButton.SetActive(true);
                    WorkerButton.SetActive(true);
                }
            }
        }
    }

    private void LateUpdate()
    {
        Debug.Log("time to go is: " + tempPlayer.timeToGo + " time to go opp is: " + tempPlayer.TimeToStartOpponent());
        if (PhotonNetwork.IsMasterClient && 
            PhotonNetwork.CurrentRoom.PlayerCount == 2 && 
            localPlayerReady == 1 &&
            tempPlayer.TimeToStartOpponent() == 1)
        {
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
        }
    }
    #endregion

    public override void OnJoinedRoom()
    {
        CharacterSelectionLobbyPanel.SetActive(true);
        JoinGamePanel.SetActive(false);
        StartGameButton.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            DisabledStartButton.SetActive(true);
            //tempPlayer.playerProperties["playerAlien"] = null;
        }
        else
        {
            //tempPlayer.playerProperties["playerAlien"] = null;
            WaitingForOpponents.text = "Waiting for Host to start";
        }
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        WaitingForOpponents.text = "Waiting for Host to start";
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("PLAYER LEFT ROOM");
        UpdatePlayerList();
        if (PhotonNetwork.IsMasterClient)//if the local player is now the new master client then we activate the disabled start button
        {
            DisabledStartButton.SetActive(true);
        }
        if (tempPlayer.alienChosen == "PHAROAH")
        {
            ScribeButton.SetActive(true);
            WorkerButton.SetActive(true);
            PeasantButton.SetActive(true);
            Debug.Log("DONE");
        }
        else if (tempPlayer.alienChosen == "SCRIBE")
        {
            PharoahButton.SetActive(true);
            WorkerButton.SetActive(true);
            PeasantButton.SetActive(true);
        }
        else if (tempPlayer.alienChosen == "WORKER")
        {
            PharoahButton.SetActive(true);
            ScribeButton.SetActive(true);
            PeasantButton.SetActive(true);
        }
        else if (tempPlayer.alienChosen == "PEASANT")
        {
            PharoahButton.SetActive(true);
            ScribeButton.SetActive(true);
            WorkerButton.SetActive(true);
        }
        else if (tempPlayer.OpponentAlien() == "PHAROAH")
        {
            ScribeButton.SetActive(true);
            WorkerButton.SetActive(true);
            PeasantButton.SetActive(true);
            Debug.Log("DONE");
        }
        else if (tempPlayer.OpponentAlien() == "SCRIBE")
        {
            PharoahButton.SetActive(true);
            WorkerButton.SetActive(true);
            PeasantButton.SetActive(true);
        }
        else if (tempPlayer.OpponentAlien() == "WORKER")
        {
            PharoahButton.SetActive(true);
            ScribeButton.SetActive(true);
            PeasantButton.SetActive(true);
        }
        else if (tempPlayer.OpponentAlien() == "PEASANT")
        {
            PharoahButton.SetActive(true);
            ScribeButton.SetActive(true);
            WorkerButton.SetActive(true);
        }
        else if (tempPlayer.alienChosen == null)
        {
            PharoahButton.SetActive(true);
            ScribeButton.SetActive(true);
            WorkerButton.SetActive(true);
            PeasantButton.SetActive(true);
        }
        WaitingForOpponents.text = "Waiting for Opponents";
    }

    public void StartGameOnClick() //paired to the start button. will load all players into the multiplayer scene through the master client and AutomaticallySyncScene
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; //Comment out if you want player to join after the game has started
            FindObjectOfType<AudioManager>().Play("stoneButtonPress");
            localPlayerReady = 0;
            tempPlayer.GameStarted();
            PhotonNetwork.LoadLevel("GameBoard");
            FindObjectOfType<AudioManager>().StopCurrentSong(6);
            readyUpStatus = false;
        }
    }

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick() // paired to the back button in the room panel. will return the player to the lobby panel.
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            JoinGamePanel.SetActive(true);
            tempPlayer.timeToGo = 0;
            tempPlayer.playerStart["timeToGo"] = 0;
        }
        else
        {
            multiplayerMenuPanel.SetActive(true);
            tempPlayer.timeToGo = 0;
            tempPlayer.playerStart["timeToGo"] = 0;
        }
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");

        CharacterSelectionLobbyPanel.SetActive(false);
        FindObjectOfType<AudioManager>().StopCurrentSong(1);

        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }

    void UpdatePlayerList()
    {
        int flipCounter = 0;

        foreach (NetPlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        Debug.Log("Clearing playerList");

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Bye Bye");
            return;
        }

        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            NetPlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            flipCounter++;

            /*********************************************************************************************
            * Checks to see if the current KeyValuePair is the local player, if so lets make a temporary
            * varaible, tempPlayer, to store this player inside of (this allows us to change the aliens 
            * from this script) using choosePharoah, chooseScribe, etc.    
            **********************************************************************************************/

            if (player.Value == PhotonNetwork.LocalPlayer)
            { 
                tempPlayer = newPlayerItem;
                //tempPlayer.playerProperties["playerAlien"] = null;
            }
            if (player.Value == PhotonNetwork.LocalPlayer && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                newPlayerItem.FlipIt(player.Value);
                if (player.Value == PhotonNetwork.LocalPlayer)
                {
                    newPlayerItem.FlipIt(player.Value);
                }
            }

            playerItemsList.Add(newPlayerItem);
            Debug.Log("Added Item");
        }
    }

    #region Miscellaneous Functions
    private void OnGameStarting(Scene current, Scene next)
    {
        if (next.name == "GameBoard")
        {
            tempPlayer.OpponentAlien();
            tempPlayer.timeToGo = 0;
            tempPlayer.playerStart["timeToGo"] = 0;
            FindObjectOfType<AudioManager>().StopCurrentSong(6);
        }
        Debug.Log("Called OnGameStarting");
    }
    public void choosePharoah()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        int chosen = 0;
        tempPlayer.ChangeTimeToGo();

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PHAROAH);
            localPlayerReady = tempPlayer.timeToGo;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.PHAROAH);
        }
        tempPlayer.changeAlien(chosen);
        PharoahButton.SetActive(false);

    }

    public void chooseScribe()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        int chosen = 1;
        tempPlayer.ChangeTimeToGo();

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.SCRIBE);
            localPlayerReady = tempPlayer.timeToGo;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.SCRIBE);
        }

        tempPlayer.changeAlien(chosen);
        ScribeButton.SetActive(false);
    }

    public void chooseWorker()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        int chosen = 2;
        tempPlayer.ChangeTimeToGo();

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.WORKER);
            localPlayerReady = tempPlayer.timeToGo;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.WORKER);
        }

        tempPlayer.changeAlien(chosen);
        WorkerButton.SetActive(false);
    }

    public void choosePeasant()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        int chosen = 3;
        tempPlayer.ChangeTimeToGo();

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PEASANT);
            localPlayerReady = tempPlayer.timeToGo;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.PEASANT);
        }

        tempPlayer.changeAlien(chosen);
        PeasantButton.SetActive(false);
    }
    #endregion
}
