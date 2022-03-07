﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NetPlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAlien;
    public Sprite[] aliens;

    Photon.Realtime.Player player;

    public void SetPlayerInfo(Photon.Realtime.Player netPlayer)
    {
        //AlienPic.transform.RotateAround(transform.position, transform.up, 280f);
        Debug.Log("Felpiing");
        player = netPlayer;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
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
}