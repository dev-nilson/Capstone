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

    public GameObject player1;
    GameObject player;

    public GameObject playerParent;
    GameObject child;

    public GameObject[,] PlayerPosition;
    GameObject[,] tempPlayer;

    int[,] boardHeights;

    private static Coordinates curLoc;
    private static Coordinates newLoc;

    public GameObject board;

    AIController ai;

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
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
                return GridManager.getSelectedTile();
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
                    return loc;
            }
            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {
            // If the mouse was clicked, return that coordinate
            if (Input.GetMouseButtonDown(0))
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {
            AIController.SimulateTurn(player, board);
            return AIController.chosenTurn.GetMoveFrom();
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
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {

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
                return GridManager.getSelectedTile();
            else
                return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {

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
