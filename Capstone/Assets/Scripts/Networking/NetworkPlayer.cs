/*
 *  Author: Brendon McDonald
 *  Description: ...
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
	[SerializeField] private NetworkController networkController;
	[SerializeField] private PhotonView photonView;

	public static Coordinates coordinates;
	public static bool playerIntentionallyLeftRoom;

	private void Start()
	{
		photonView = PhotonView.Get(this);
		coordinates = new Coordinates();
	}

    [PunRPC]
    public void RPC_SendPlayerLeft(bool playerStatus)
    {
        NetworkController.SetPlayerLeftStatus(playerStatus);
        Debug.Log("Message sent over network: " + playerStatus);
    }

    //Will simply call the setCoordinate function in NetworkController
    [PunRPC]
	public void RPC_SendCoordinates(Coordinates coordinates)
	{
		Debug.Log("Receiving coordinates...");
		Debug.Log("Received coordinates were: " + coordinates.X + " " + coordinates.Y);
		NetworkController.SetCoordinates(coordinates);
	}

	//Calls the RPC function above and sends the result of that RPC call to the player other than the one that made the call
	public void SendCoordinates(Coordinates coordinates)
	{
		Debug.Log("SendCoordinates function in NETWORK PLAYER called");
		Debug.LogWarning("coordinates are " + coordinates.X + " " + coordinates.Y);
		photonView.RPC("RPC_SendCoordinates", RpcTarget.OthersBuffered, coordinates);
	}

    public void SendPlayerLeft(bool playerStatus)
    {
        Debug.Log("SendPlayerLeft function in NETWORK PLAYER called");
        photonView.RPC("RPC_SendPlayerLeft", RpcTarget.Others, playerStatus);
    }

}

