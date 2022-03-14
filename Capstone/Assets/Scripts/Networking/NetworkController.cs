using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public static NetworkController netController;
    public static Coordinates coordinates;
    public static NetworkPlayer netPlayer;

    [SerializeField]
	private PhotonView photonView;

    private void Awake()
    {
        netController = this;
        //netPlayer = GameObject.Find("networkPlayer").GetComponent<NetworkPlayer>();
    }

    public void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("networkPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
       netPlayer = player.GetComponent<NetworkPlayer>();
    }

    private void Update()
    {
        //add watcher that will continually see if player has disconnected yet or not
    }

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
            if(coordinates == null)
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
            coordinates = new Coordinates();
            SendCoordinates();

            return temp;
        }
    }
}
