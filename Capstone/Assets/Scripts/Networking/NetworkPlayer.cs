using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
	[SerializeField] private NetworkController networkController;
	[SerializeField] private new PhotonView photonView;
	
	//public static NetworkPlayer netPlayer;
	public static Coordinates coordinates;
	//public static Coordinates buildLocation;

	//however we represent the move and build

	private void Start()
	{
		photonView = PhotonView.Get(this);
		//netPlayer = this;
	}

	[PunRPC]
	public void RPC_receiveNetworkMessage(string message)
	{
		Debug.Log("network message rpc called");

		if (!photonView.IsMine)
			return;

		Debug.Log("Message sent over network: " + message);
	}

	//Checks if they have photon view and then does specific function calls in network controller based on what it currently needs to do
	[PunRPC]
	public void RPC_SendCoordinates(Coordinates coordinates)
	{
		Debug.LogWarning("coordinates are " + coordinates.X + " " + coordinates.Y);
		Debug.Log("send coordinates rpc called");
		if (!photonView.IsMine)
			Debug.Log("that photon view aint yours fool");
			return;

		Debug.Log("Receiving move...");
		NetworkController.SetCoordinates(coordinates);
	}

	//[PunRPC]
	//public void RPC_SendBuild(Coordinates buildLocation)
	//{
	//	Debug.Log("send build rpc called");
	//	if (!photonView.IsMine)
	//		return;

	//	Debug.Log("Receiving build...");
	//	networkController.SetBuildCoordinates(buildLocation);
	//}


	//These will call the RPC functions above and send the result of those RPC calls to all players in room
	public static void SendCoordinates(Coordinates coordinates)
	{
		Debug.Log("send coordinates function in NETWORK PLAYER called");
		Debug.LogWarning("coordinates are " + coordinates.X + " " + coordinates.Y);
		photonView.RPC("RPC_SendCoordinates", RpcTarget.All, coordinates);
	}


	//public void SendBuild(Coordinates buildLocation)
	//{
	//	Debug.Log("send move function called");
	//	photonView.RPC("RPC_SendBuild", RpcTarget.All, buildLocation);
	//}

}
