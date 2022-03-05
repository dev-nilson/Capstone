﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController netController;
    public static Coordinates moveLocation;
    public static Coordinates buildLocation;
    public string roomName;

	[SerializeField]
	private PhotonView photonView;
	[SerializeField]
	private PlayerController playerController;
	private static char networkMessage;
	private static bool networkMessageRecieved = false;

    private void Awake()
    {
        netController = this;
    }

    public void Start()
    {
        //playerLeftRoomFunctionCalled = false;
        GameObject player = PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    private void Update()
    {
        //add watcher that will continually see if player has disconnected yet or not
    }

    public void setPlayerControllerReference(PlayerController controller)
    {
        playerController = controller;
    }

    //public IEnumerator WaitForTurn()
    //{
    //    Debug.Log("Waiting for turn");

    //    while (networkMessageRecieved == false)
    //        yield return null;
    //    networkMessageRecieved = false;
    //    //gameController.NetworkMessageRecieved();
    //}

    public void SendMove()
    {
        //Debug.Log("This is what we get for netPlayer" + NetworkPlayer.netPlayer);
        //if (NetworkPlayer.netPlayer = null)
        //{
        //    Debug.Log("This is what we get for netPlayer" + NetworkPlayer.netPlayer);
        //}
        Debug.Log("NetworkController, Send Move Called");
        Debug.Log("Move coordinates were: " + moveLocation.X + " " + moveLocation.Y);

        if (NetworkPlayer.netPlayer != null)
        {
            NetworkPlayer.netPlayer.SendMove(moveLocation);
            Debug.Log("net is not null");
        }
        //else
        //    Debug.Log("Somethin aint right, fix it");
    }

    public void SendBuild()
    {
        Debug.Log("NetworkController, Send Build Called");
        Debug.Log("Build coordinates were: " + buildLocation.X + " " + buildLocation.Y);
        NetworkPlayer.netPlayer.SendBuild(buildLocation);
    }

    public void SetMoveCoordinates(Coordinates newCoordinates)
    {
        Debug.Log("SetMoveCoordinates called");
        Debug.Log("Setting move coodrinates " + newCoordinates.X + " " + newCoordinates.Y);
        moveLocation = newCoordinates;
    }
    public Coordinates GetMoveCoordinates()
    {
        Debug.Log("GetMoveCoordinates called");
        if (moveLocation == null)
            Debug.Log("No coordinate set");
        else
            Debug.Log("Returning move coordinates: " + moveLocation.X + " " + moveLocation.Y);
        return moveLocation;
    }

    public void SetBuildCoordinates(Coordinates newCoordinates)
    {
        Debug.Log("SetBuildCoordinates called");
        Debug.Log("Setting build coodrinates " + newCoordinates.X + " " + newCoordinates.Y);
        buildLocation = newCoordinates;
    }
    public Coordinates GetBuildCoordinates()
    {
        Debug.Log("GetBuildCoordinates called");
        if (buildLocation == null)
            Debug.Log("No coordinate set");
        else
            Debug.Log("Returning build coordinates: " + buildLocation.X + " " + buildLocation.Y);
        return buildLocation;
    }
}
