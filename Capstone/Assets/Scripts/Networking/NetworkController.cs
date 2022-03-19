/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using static GameUtilities;

public class NetworkController : MonoBehaviourPunCallbacks
{
    #region Variables
    public static NetworkController netController;
    public static Coordinates coordinates;
    public static NetworkPlayer netPlayer;

    [SerializeField]
    private PhotonView photonView;
    #endregion

    #region AwakeStartUpdate
    private void Awake()
    {
        netController = this;
    }

    public void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("networkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
        netPlayer = player.GetComponent<NetworkPlayer>();
    }

    private void Update()
    {
        //add watcher that will continually see if player has disconnected yet or not
        if (PhotonNetwork.NetworkClientState == ClientState.Disconnected)
        {
            //code for however we decide to tell player they've disconnected

            //disable game phases
            DisablePhases();
            Debug.Log("You have lost network connection");

            // MAGGIE: some pop up that says you are disconnected with button to return to main menu
            //then clear the game when you hit "okay" or whatever!


        }
        else if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            //code to tell person still left in the room that their opponent has disconnected

            //disable game phases
            DisablePhases();
            Debug.Log("Your opponent has lost network connection");

            // MAGGIE: tell player that their opponent disconnected -- button to return to main menu
            //then clear the game when you hit "okay" or whatever!


            // disconnect our end of network stuff

        }
    }
    #endregion

    #region Functions
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

    #endregion
}


