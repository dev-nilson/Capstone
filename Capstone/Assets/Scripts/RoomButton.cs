using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text name;
    public string roomName;

    public void SetRoom()
    {
        name.text = roomName;
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
