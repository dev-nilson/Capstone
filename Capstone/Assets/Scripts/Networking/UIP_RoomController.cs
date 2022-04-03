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
        }
    }

    private void LateUpdate()
    {
        if (PhotonNetwork.IsMasterClient && 
            PhotonNetwork.CurrentRoom.PlayerCount == 2 && 
            tempPlayer.alienChosen != null && 
            tempPlayer.OpponentAlien() != null)
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
        }
        else
        {
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
        if (PhotonNetwork.IsMasterClient)//if the local player is now the new master client then we activate the start button
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
        }
        else
        {
            multiplayerMenuPanel.SetActive(true);
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
        tempPlayer.OpponentAlien();
        if (next.name == "GameBoard")
        {
            FindObjectOfType<AudioManager>().StopCurrentSong(6);
        }
        Debug.Log("Called OnGameStarting");
    }
    public void choosePharoah()
    {
        int chosen = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PHAROAH);
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
        int chosen = 1;

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PHAROAH);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.PHAROAH);
        }

        tempPlayer.changeAlien(chosen);
        ScribeButton.SetActive(false);
    }

    public void chooseWorker()
    {
        int chosen = 2;

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PHAROAH);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.PHAROAH);
        }

        tempPlayer.changeAlien(chosen);
        WorkerButton.SetActive(false);
    }

    public void choosePeasant()
    {
        int chosen = 3;

        if (PhotonNetwork.IsMasterClient)
        {
            setP1avatar(PlayerAvatar.PHAROAH);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            setP2avatar(PlayerAvatar.PHAROAH);
        }

        tempPlayer.changeAlien(chosen);
        PeasantButton.SetActive(false);
    }
    #endregion
}
