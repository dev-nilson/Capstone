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

	private static bool networkMessage;
	private static bool readyUpStatus;

	private void Start()
	{
		photonView = PhotonView.Get(this);
	}

	[PunRPC]
	public void RPC_NetworkMessage(bool message)
	{
		NetworkController.SetNetMessage(networkMessage);
		Debug.Log("Message sent over network: " + message);
	}

	[PunRPC]
	public void RPC_SendReadyUpStatus(bool newStatus)
	{
		NetworkController.SetReadyUpStatus(readyUpStatus);
		Debug.Log("Ready up status of other player is: " + readyUpStatus);
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

	public void SendNetworkMessage(bool message)
    {
		Debug.Log("SendNetworkMessage function in NETWORK PLAYER called");
		photonView.RPC("RPC_NetworkMessage", RpcTarget.Others, networkMessage);
	}

	public void SendReadyUpStatus(bool message)
	{
		Debug.Log("SendReadyUpStatus function in NETWORK PLAYER called");
		photonView.RPC("RPC_ReadyUpStatus", RpcTarget.Others, readyUpStatus);
	}

	//function for the playerinfo stuff
}

