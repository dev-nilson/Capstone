/*
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

    private Timer timer;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        //ai = new AIController();
        boardController = board.GetComponent<GridManager>();
        Board = boardController.GetComponent<GridManager>();

        networkController = new NetworkController();

        delay = 4.0F;
        //timer = new Timer(delay);
        
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
                    networkController = new NetworkController();
                    networkController.SetMoveCoordinates(loc);
                    networkController.SendMove();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            networkController = new NetworkController();
            Coordinates loc = networkController.GetMoveCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
    }

    public Coordinates GetPawn(GameBoard board, Player player)
    {
        if (player.Type() == Player.Tag.LOCAL)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
            {
                Coordinates loc = GridManager.getSelectedTile();
                if (player.HasThisPawn(loc))
                {
                    if (getGameType() == GameType.NETWORK)
                    {
                        networkController = new NetworkController();
                        networkController.SetMoveCoordinates(loc);
                        networkController.SendMove();
                    }
                    return loc;
                }
            }
            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            networkController = new NetworkController();
            Coordinates loc = networkController.GetMoveCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            //if (timer == null) timer = new Timer(delay);
            //else if (timer.Set() && timer.Over())
            //{
                AIController.SimulateTurn(player, board);
                return AIController.chosenTurn.GetMoveFrom();
            //}
            //else if (!timer.Set())
            //    //StartCoroutine(timer.Start());
            
            //return new Coordinates();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
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
                    networkController.SetMoveCoordinates(loc);
                    networkController.SendMove();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            networkController = new NetworkController();
            Coordinates loc = networkController.GetMoveCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            return AIController.chosenTurn.GetMoveTo();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {

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
                    networkController = new NetworkController();
                    networkController.SetMoveCoordinates(loc);
                    networkController.SendMove();
                }
                return loc;
            }
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            networkController = new NetworkController();
            Coordinates loc = networkController.GetMoveCoordinates();
            if (GameBoard.IsValidCoord(loc))
                return loc;
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            return AIController.chosenTurn.BuildTo();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {

            return new Coordinates();
        }
    }
}
