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
using static GameUtilities;

public class UIP_RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiPlayerSceneIndex; //scene index for loading multiplayer scene

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

    public override void OnJoinedRoom()
    {
        CharacterSelectionLobbyPanel.SetActive(true); //activate the display for being in a room
        JoinGamePanel.SetActive(false); //hide the display for being in a lobby
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2) //if master client then activate the start button
        {
            StartGameButton.SetActive(true);
        }
        else
        {
            WaitingForOpponents.text = "Waiting for Host to start";
            StartGameButton.SetActive(false);
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
        UpdatePlayerList();
        if (PhotonNetwork.IsMasterClient)//if the local player is now the new master client then we activate the start button
        {
            StartGameButton.SetActive(true);
        }
        WaitingForOpponents.text = "Waiting for Opponents";
    }

    public void StartGameOnClick() //paired to the start button. will load all players into the multiplayer scene through the master client and AutomaticallySyncScene
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; //Comment out if you want player to join after the game has started
            PhotonNetwork.LoadLevel(1);
            FindObjectOfType<AudioManager>().StopCurrentSong(6);
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
        CharacterSelectionLobbyPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }

    void UpdatePlayerList()
    {
        int flipCounter;

        foreach (NetPlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        Debug.Log("Clearing");

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Bye Bye");
            return;
        }

        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            NetPlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //newPlayerItem.FlipIt(player.Value);
                if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    StartGameButton.SetActive(true);
                }
            }

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                //newPlayerItem.ApplyLocalChanges();
                //if (player.Value == PhotonNetwork.LocalPlayer && (!PhotonNetwork.IsMasterClient))
                //{
                //    newPlayerItem.FlipIt(player.Value);
                //}
                tempPlayer = newPlayerItem;
            }

            playerItemsList.Add(newPlayerItem);
            Debug.Log("Added Item");
        }
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
    }
}
