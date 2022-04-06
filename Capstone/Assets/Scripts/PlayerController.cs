﻿/*
 *  Author: Laura Grace Ashburn
 *  Description: ...
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameUtilities;
using AmazingGame;

public class PlayerController : MonoBehaviour
{
    GridManager boardController;
    GridManager Board;

    GameObject player;

    public GameObject playerParent;
    GameObject child;

    public GameObject[,] PlayerPosition;
    GameObject[,] tempPlayer;

    public GameObject board;

    private NetworkController networkController;

    private bool AIdone = false;

    // Start is called before the first frame update
    void Start()
    {
        //ai = new AIController();
        boardController = board.GetComponent<GridManager>();
        Board = boardController.GetComponent<GridManager>();
    }

    public Coordinates GetPlacement(GameBoard board, Player player)
    {
        if (player.Type() == Player.Tag.LOCAL)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
            {
                Coordinates loc = GridManager.getSelectedTile();
                if (getGameType() == GameType.NETWORK)
                {
                    NetworkController.SetCoordinates(loc);
                    NetworkController.SendCoordinates();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            Coordinates loc = NetworkController.GetCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            Coordinates loc = AIController.PlacePawns(board);
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
            {
                Debug.Log("AI (easy) failed to return a valid coordinate");
                return new Coordinates();
            }
        }
        else if (GameUtilities.getGameType() == GameType.DIFFICULT)
        {
            Coordinates loc = AIController.PlacePawns(board);
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
            {
                Debug.Log("AI (hard) failed to return a valid coordinate");
                return new Coordinates();
            }
        }
        else
        {
            Debug.Log("ERROR: NO GAME TYPE SET");
            return new Coordinates();
        }
    }

    public Coordinates GetPawn(GameBoard board, Player currentPlayer, Player waitingPlayer)
    {
        if (currentPlayer.Type() == Player.Tag.LOCAL)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
            {
                Coordinates loc = GridManager.getSelectedTile();
                if (currentPlayer.HasThisPawn(loc))
                {
                    if (getGameType() == GameType.NETWORK)
                    {
                        NetworkController.SetCoordinates(loc);
                        NetworkController.SendCoordinates();
                    }
                    return loc;
                }
            }
            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            Coordinates loc = NetworkController.GetCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            AIController.SimulateTurn(currentPlayer, waitingPlayer, board);
            Debug.Log("Playing easy AI");
            return AIController.bestNode.GetMoveFrom();
        }
        else if (GameUtilities.getGameType() == GameType.DIFFICULT)
        {
            StartCoroutine(_SimulateTurnExpert(currentPlayer, waitingPlayer, board));
            if (AIdone)
            {
                AIdone = false;
                return AIController.bestNode.GetMoveFrom();
            }
            else
                return new Coordinates();


            //AIController.SimulateTurnExpert(currentPlayer, waitingPlayer, board);
            //Debug.Log("Playing hard AI");
            //return AIController.bestNode.GetMoveFrom();
        }
        else
        {
            Debug.Log("ERROR: NO GAME TYPE SET");
            return new Coordinates();
        }
    }

    private IEnumerator _SimulateTurnExpert(Player currentPlayer, Player waitingPlayer, GameBoard board)
    {
        
        AIController.SimulateTurn(currentPlayer, waitingPlayer, board);
        AIdone = true;
        yield return null;
    }

    public Coordinates GetMove(GameBoard board, Player player)
    {
        if (player.Type() == Player.Tag.LOCAL)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
            {
                Coordinates loc = GridManager.getSelectedTile();
                if (getGameType() == GameType.NETWORK)
                {
                    //networkController = new NetworkController();
                    NetworkController.SetCoordinates(loc);
                    NetworkController.SendCoordinates();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            Coordinates loc = NetworkController.GetCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            return AIController.bestNode.GetMoveTo();
        }
        else if (GameUtilities.getGameType() == GameType.DIFFICULT)
        {
            return AIController.bestNode.GetMoveTo();
        }
        else
        {
            Debug.Log("ERROR: NO GAME TYPE SET");
            return new Coordinates();
        }
    }

    public Coordinates GetBuild(GameBoard board, Player player)
    {
        if (player.Type() == Player.Tag.LOCAL)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
            {
                Coordinates loc = GridManager.getSelectedTile();
                if (getGameType() == GameType.NETWORK)
                {
                    NetworkController.SetCoordinates(loc);
                    NetworkController.SendCoordinates();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            Coordinates loc = NetworkController.GetCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            return AIController.bestNode.BuildTo();
        }
        else if (GameUtilities.getGameType() == GameType.DIFFICULT)
        {
            return AIController.bestNode.BuildTo();
        }
        else
        {
            Debug.Log("ERROR: NO GAME TYPE SET");
            return new Coordinates();
        }
    }
}
