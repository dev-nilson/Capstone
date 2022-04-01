﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtilities;

// setP1username(string username); -- always local
// setP2username(string username); -- always opponent
// setP1avatar(PlayerAvatar playerAvatar)
// setP2avatar(PlayerAvatar playerAvatar)
//      PlayerAvatar.PHAROAH,
//      PlayerAvatar.SCRIBE,
//      PlayerAvatar.PEASANT,
//      PlayerAvatar.WORKER
// RandomPlayerAvatar(PlayerAvatar avatar_A) -- one of the three avatars that is not "avatar_A"


public class NetPlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAlien;
    public Sprite[] aliens;

    private PhotonView photonView;

    Photon.Realtime.Player player;

    public void SetPlayerInfo(Photon.Realtime.Player netPlayer)
    {
        //AlienPic.transform.RotateAround(transform.position, transform.up, 280f);
        player = netPlayer;
        string myNickname = PlayerPrefs.GetString("NickName");
        Debug.Log("This is my Nickname: " + myNickname);

        if (player == PhotonNetwork.LocalPlayer)
        {
            playerName.text = PlayerPrefs.GetString("NickName");
            setP1username(playerName.text);

            //if (!PhotonNetwork.IsMasterClient)
            //{
            //    FlipIt(netPlayer);
            //} 
        }
        else if (player != PhotonNetwork.LocalPlayer)
        {
            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                if (p.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    
                    playerName.text = p.NickName;
                    setP2username(playerName.text);
                }
            }
        }

        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        //leftArrowButton.SetActive(true);
        //rightArrowButton.SetActive(true);
    }

    public void FlipIt(Photon.Realtime.Player netPlayer)
    {
        playerAlien.transform.RotateAround(transform.position, transform.up, 180f);
        Debug.Log("Flipped it!");
    }

    public void OnClickLeftArrow()
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            if ((int)playerProperties["playerAlien"] == 0)
            {
                playerProperties["playerAlien"] = aliens.Length - 1;
            }
            else
            {
                playerProperties["playerAlien"] = (int)playerProperties["playerAlien"] - 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    public void OnClickRightArrow()
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            if ((int)playerProperties["playerAlien"] == aliens.Length - 1)
            {
                playerProperties["playerAlien"] = 0;
            }
            else
            {
                playerProperties["playerAlien"] = (int)playerProperties["playerAlien"] + 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }

    }

    void UpdatePlayerItem(Photon.Realtime.Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAlien"))
        {
            playerAlien.sprite = aliens[(int)player.CustomProperties["playerAlien"]];
            playerProperties["playerAlien"] = (int)player.CustomProperties["playerAlien"];
        }
        else
        {
            playerProperties["playerAlien"] = 0;
        }
    }

    public void changeAlien(int chosen)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            if (chosen == 0)
            {
                playerProperties["playerAlien"] = 0;
                setP1avatar(PlayerAvatar.PHAROAH);
            }
            else if (chosen == 1)
            {
                playerProperties["playerAlien"] = 1;
                setP1avatar(PlayerAvatar.SCRIBE);
            }
            else if (chosen == 2)
            {
                playerProperties["playerAlien"] = 2;
                setP1avatar(PlayerAvatar.WORKER);
            }
            else if (chosen == 3)
            {
                playerProperties["playerAlien"] = 3;
                setP1avatar(PlayerAvatar.PEASANT);
            }

            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    public void OpponentAlien()
    {
        if (player != PhotonNetwork.LocalPlayer)
        {
            foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            {
                if (p.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    if ((bool)(playerProperties["playerAlien"] = 0))
                    {
                        setP2avatar(PlayerAvatar.PHAROAH);
                    }
                    else if ((bool)(playerProperties["playerAlien"] = 1))
                    {
                        setP2avatar(PlayerAvatar.SCRIBE);
                    }
                    else if ((bool)(playerProperties["playerAlien"] = 2))
                    {
                        setP2avatar(PlayerAvatar.WORKER);
                    }
                    else if ((bool)(playerProperties["playerAlien"] = 3))
                    {
                        setP2avatar(PlayerAvatar.PEASANT);
                    }
                }
            }
        }
    }
}
