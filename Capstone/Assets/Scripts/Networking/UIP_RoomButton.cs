using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UIP_RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText; //display for room name


    private string roomName; //string for saving room name

    public void JoinRoomOnClick() //paired the button that is the room listing. joins the player to a room by its name
    {
        Debug.Log("clicked!!");
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetRoom(string nameInput) //public function called in CMM lobby contoller for each new room listing created
    {
        roomName = nameInput;
        nameText.text = nameInput;
    }
}
