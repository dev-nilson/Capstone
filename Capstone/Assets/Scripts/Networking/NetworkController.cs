/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class NetworkController : MonoBehaviourPunCallbacks
{

    #region Variables
    public static NetworkController netController;
    public static Coordinates coordinates;
    public static NetworkPlayer netPlayer;
    

    [SerializeField]
    private PhotonView photonView;

    private static bool networkMessage;
    public static bool playerIntentionallyLeftRoom;
    #endregion

    #region AwakeStartUpdate
    private void Awake()
    {
        netController = this;
    }

    public void Start()
    {
        if (getGameType() == GameType.NETWORK)
        {
            //add in code from laura grace to make sure this is network game
            GameObject player = PhotonNetwork.Instantiate("networkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
            netPlayer = player.GetComponent<NetworkPlayer>();
            playerIntentionallyLeftRoom = false;
            coordinates = new Coordinates();
        }
    }

    private void Update()
    {
        if (getGameType() == GameType.NETWORK)
        {
            //add in code from laura grace to make sure this is network game
            playerIntentionallyLeftRoom = GetPlayerLeftStatus();
            Debug.Log("Player intentionally left room: " + playerIntentionallyLeftRoom);

            if (getGameType() == GameType.NETWORK && PhotonNetwork.NetworkClientState == ClientState.Disconnected) //local player loses connection
            {
                //disable game phases
                DisablePhases();
                Debug.Log("You have lost network connection");

                SetLocalDisconnect();
            }
            else if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom.PlayerCount < 2) //players opponent initentionally leaves the game
            {
                if (playerIntentionallyLeftRoom == true)
                {
                    DisablePhases();
                    Debug.Log("Your opponent has intentionally left the network");

                    //SetOpponentDisconnect();
                    SetOpponentLeft();

                    PhotonNetwork.LeaveRoom();
                    PhotonNetwork.LeaveLobby();
                }
                else if (playerIntentionallyLeftRoom == false)
                {
                    //disable game phases
                    DisablePhases();
                    Debug.Log("Your opponent has lost network connection");

                    SetOpponentDisconnect();

                    PhotonNetwork.LeaveRoom();
                    PhotonNetwork.LeaveLobby();
                }
            }
            else if (PhotonNetwork.IsConnected && playerIntentionallyLeftRoom == true) //local player intentionally leaves the game
            {
                Debug.Log("You have left the network");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LeaveLobby();
            }
        }
    }
    #endregion

    #region GameplayRelatedFunctions
    public static void SendCoordinates()
    {
        Debug.Log("SendCoordinates function in NETWORK CONTROLLER called");
        Debug.Log("Sending the coordinates: " + coordinates.X + " " + coordinates.Y);
        netPlayer.SendCoordinates(coordinates);
    }

    public static void SetCoordinates(Coordinates newCoordinates)
    {
        Debug.Log("SetCoordinates function in NETWORK CONTROLLER called");
        Debug.Log("coordinates being set are " + newCoordinates.X + " " + newCoordinates.Y);
        coordinates = newCoordinates;

        Debug.Log("These coordinates should be the same as the coordinates being set: " + coordinates.X + " " + coordinates.Y);
    }

    public static Coordinates GetCoordinates()
    {
        Debug.Log("GetCoordinates function in NETWORK CONTROLLER called");
        if (coordinates == null || coordinates == new Coordinates())
        {
            Debug.Log("No coordinate set");
            if (coordinates == null)
            {
                Debug.Log("This is the location we get is null");
            }
            return new Coordinates();
        }
        else
        {
            Debug.Log("Returning coordinates: " + coordinates.X + " " + coordinates.Y);

            // Clear "coordinates" variable after it is received, so it is does not get reused.
            Coordinates temp = coordinates;

            SendCoordinates();
            coordinates = null;
            return temp;
        }
    }

    public static void SendPlayerLeft()
    {
        if (getGameType() == GameType.NETWORK)
        {
            netPlayer.SendPlayerLeft(playerIntentionallyLeftRoom);
        }
    }

    public static void SetPlayerLeftStatus(bool playerStatus)
    {
        if (getGameType() == GameType.NETWORK)
        {
            playerIntentionallyLeftRoom = playerStatus;
        }
    }

    public static bool GetPlayerLeftStatus()
    {
        return playerIntentionallyLeftRoom;
    }
    #endregion

    public static void DisconnectPlayer()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
    }
}


