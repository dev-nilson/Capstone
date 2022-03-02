using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
	[SerializeField] private NetworkController networkController;
	[SerializeField] private PhotonView photonView;
	
	public static NetworkPlayer netPlayer;
	public static Coordinates moveLocation;
	public static Coordinates buildLocation;

	//however we represent the move and build

	private void Start()
	{
		photonView = PhotonView.Get(this);
		netPlayer = this;
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
	public void RPC_SendMove(Coordinates moveLocation)
	{
		Debug.Log("send move rpc called");
		if (!photonView.IsMine)
			return;

		Debug.Log("Receiving move...");
		networkController.SetMoveCoordinates(moveLocation);
	}

	[PunRPC]
	public void RPC_SendBuild(Coordinates buildLocation)
	{
		Debug.Log("send build rpc called");
		if (!photonView.IsMine)
			return;

		Debug.Log("Receiving build...");
		networkController.SetBuildCoordinates(buildLocation);
	}


	//These will call the RPC functions above and send the result of those RPC calls to all players in room
	public void SendMove(Coordinates moveLocation)

	{
		Debug.Log("send move function called");
		photonView.RPC("RPC_SendMove", RpcTarget.All, moveLocation);
	}


	public void SendBuild(Coordinates buildLocation)

	{
		Debug.Log("send move function called");
		photonView.RPC("RPC_SendBuild", RpcTarget.All, buildLocation);
	}

}
