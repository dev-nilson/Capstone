using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController netController;
    public static Coordinates coordinates;


	[SerializeField]
	private new PhotonView photonView;

    private void Awake()
    {
        netController = this;
    }

    public void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("NetworkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    private void Update()
    {
        //add watcher that will continually see if player has disconnected yet or not
    }

    //public IEnumerator WaitForTurn()
    //{
    //    Debug.Log("Waiting for turn");

    //    while (networkMessageRecieved == false)
    //        yield return null;
    //    networkMessageRecieved = false;
    //    //gameController.NetworkMessageRecieved();
    //}

    public void SendCoordinates()
    {
        //Debug.Log("This is what we get for netPlayer" + NetworkPlayer.netPlayer);
        //if (NetworkPlayer.netPlayer = null)
        //{
        //    Debug.Log("This is what we get for netPlayer" + NetworkPlayer.netPlayer);
        //}
        Debug.Log("NetworkController, Send Coordinates Called");
        Debug.Log("The coordinates were: " + coordinates.X + " " + coordinates.Y);

        //if (NetworkPlayer.netPlayer != null)
        //{
        NetworkPlayer.netPlayer.SendCoordinates(coordinates);
        //    Debug.Log("net is not null");
        //}
        //else if (NetworkPlayer.netPlayer == null)
        //    Debug.Log("Somethin aint right, fix it");
        //NetworkPlayer.SendCoordinates(coordinates);
    }

    public static void SetCoordinates(Coordinates newCoordinates)
    {
        Debug.Log("SetCoordinates called");
        Debug.Log("Setting  coordinates " + newCoordinates.X + " " + newCoordinates.Y);
        coordinates = newCoordinates;
    }

    public Coordinates GetCoordinates()
    {
        Debug.Log("GetCoordinates called");
        if (coordinates == null || coordinates == new Coordinates())
        {
            Debug.Log("No coordinate set");
            return new Coordinates();
        }
        else
        {
            Debug.Log("Returning coordinates: " + coordinates.X + " " + coordinates.Y);

            // Clear "coordinates" variable after it is received, so it is does not get reused.
            Coordinates temp = coordinates;
            coordinates = new Coordinates();
            SendCoordinates();

            return temp;
        }
    }
}
