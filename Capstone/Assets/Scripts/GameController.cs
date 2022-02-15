/*
 *  Author: Laura Grace Ashburn
 *  Description: ...
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUtilities;

public class GameController : MonoBehaviour
{
    GameBoard board_gc;
    int[,] boardHeights;
    Player P1;
    Coordinates curLoc;
    Coordinates newLoc;

    GridManager boardController;
    GridManager Board;

    PlayerController playerController;

    public GameObject[,] Grid;
    public GameObject prefab;
    public GameObject parent;
    public GameObject board;
    public GameObject player;

    public GameObject player1;

    GameObject child;

    int Col = 5, Row = 5;
    Coordinates loc = new Coordinates(-1, -1);

    //public bool player1TurnActive = false;

    void Awake()
    {
        boardController = board.GetComponent<GridManager>();
        Board = boardController.GetComponent<GridManager>();
        //clickPositionManager = player.GetComponent<ClickPositionManager>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

        

        // Variables
        //int x, y, newx, newy;
        //MoveType status;

        //INITIALIZE GAME: AI HARD, AI EASY, NETWORK?
        //  OPPONENT'S USERNAME
        //  STARTING PLAYER?

        //GC:  INITIALIZE BOARD
        board_gc = new GameBoard();
        //GUI: CREATE EMPTY BOARD
        boardHeights = board_gc.GetHeights();
        boardController.createBoard(boardHeights);
        boardController.displayBoard(boardHeights);

        // GUI: GET A USERNAME FROM USER
        string username = "Player one";
        bool local = true;

        //GC: CREATE PLAYERS
        P1 = new Player(local, username);
        //playerController.placePlayer(3, 4);

        //player1TurnActive = true;
        if (GetPlayerTurn() == PlayerTurn.ONE)
            Debug.Log("P1's turn!");

        Coordinates newLoc = new Coordinates(0, 2);
        board_gc.PlacePawn(P1, newLoc);
        //List<Coordinates> validTiles = board_gc.AvailableMoves(newLoc);
        //boardController.highlightValidTiles(validTiles);
        boardController.displayBoard(boardHeights);

        //Coordinates newLoc = new Coordinates(0, 2);
        //board_gc.PlacePawn(P1, newLoc);
        //List<Coordinates> validTiles = board_gc.AvailableMoves(newLoc);
        //boardController.highlightValidTiles(validTiles);


        //boardController.displayBoard(boardHeights);

        //List<Coordinates> validTiles = board_gc.AvailableMoves(newLoc);

        //boardController.highlightValidTiles(validTiles);

        //player.name = ("X: " + row + " Y: " + col);
        //player.transform.parent = playerParent.transform;

        /*Coordinates loc;
        loc = boardController.getSelectedTile();
        Debug.Log(loc.X + " " + loc.Y);*/

        //GC: UPDATE BOARD AND PASS BACK
        //board_gc.PlacePawn(P1, loc);

        //Debug.Log(loc.X + " " + loc.Y);
        //playerController.placePlayer(loc.X, loc.Y);

        //who is starting player?

        //PLACE PLAYERS
        //GUI: GET COORDINATE FROM PLAYER

        /*if(boardController.getSelectedTile() != null)
        {
            Coordinates loc;
            loc = boardController.getSelectedTile();

            Debug.Log(loc.X);
        }*/


        // Game begins with no place pawn, move, or build phase
        DisablePhases();
        //SwapPlacePawnPhase();
        SwapMovePhase();
        //SwapBuildPhase();

    }

    // Update is called once per frame
    void Update()
    {
        //boardHeights = board_gc.GetHeights();
        //boardController.displayBoard(boardHeights);
        

        if (Input.GetMouseButtonDown(0))
        {
            if (CanPlacePawn())
            {
                
                //Debug.Log("here");
                Coordinates loc = boardController.getSelectedTile();
                if (board_gc.PlacePawn(P1, loc)) SwapPlacePawnPhase();
                List<Coordinates> validTiles = board_gc.AvailableMoves(loc);
                //boardController.highlightValidTiles(validTiles);

                //loc = boardController.getSelectedTile();
                //if (loc != null)
                //{
                //    Debug.Log(loc);
                //}

                //playerController.placePlayer(1, 1);

                //GC: UPDATE BOARD AND PASS BACK
                //board_gc.PlacePawn(P1, newLoc);

                //SwapPlacePawnPhase();
                //SwapMovePhase();
            }
            if (CanMove())
            {
                // If no tile has been clicked yet, do nothing

                // If first file is clicked & while waiting for the second tile to be clicked
                if (playerController.FirstTileClicked())
                {
                    Debug.Log("TESTING");

                    // Collect the first tile
                    curLoc = boardController.getSelectedTile();

                    List<Coordinates> validTiles = board_gc.AvailableMoves(curLoc);
                    boardController.highlightValidTiles(validTiles);
                }
                else
                {
                    Debug.Log("TESTING2");
                    newLoc = boardController.getSelectedTile();

                    if (board_gc.MovePawn(P1, curLoc, newLoc) == MoveType.VALID)
                    {
                        //display the board
                        //Board.displayBoard(boardHeights);
                        

                        Debug.Log("Pawn1: " + P1.GetPlayerCoordinates()[0].X + ", " + P1.GetPlayerCoordinates()[0].Y);
                        Debug.Log("Pawn2: " + P1.GetPlayerCoordinates()[1].X + ", " + P1.GetPlayerCoordinates()[1].Y);

                        //board_gc.PlacePawn(P1, newLoc);
                        //List<Coordinates> validTiles = board_gc.AvailableMoves(newLoc);
                        //boardController.highlightValidTiles(validTiles);
                    }
                }
            }




            //LOOP WHILE NO ERROR
            //MOVE PLAYERS ------
            //GC: IF NEITHER OF PLAYER'S PAWNS CAN MOVE, YOU LOSE (break loop)
            //GC: TELL GUI IF ONE OF THE PAWNS HAS NO MOVES
            //Coordinates[] pawnLocs = P1.GetPlayerCoordinates();
            //if (board.AvailableMoves(pawnLocs[0]).Count == 0)
            //GUI: TELL GC WHICH PAWN WAS CLICKED
            int x = 1, y = 1;
            //GC: TELL GUI WHAT MOVES ARE VALID FOR THAT PAWN
            //int[,] validMoves = ConvertToBinaryBoard(board_gc.AvailableMoves(new Coordinates(x, y)));
            //GUI: HIGHLIGHT THE VALID MOVES
            //boardController.highlightValidTiles(validMoves);
            //GUI: SEND GC WHAT TILE WAS CLICKED
            //GC: UPDATE BOARD, RETURN BOARD TO GUI, ALERT IF YOU WIN (break loop)
            //GUI: UPDATE BOARD

            //BUILD------
            //GC: GIVES ME ALL THE VALID BUILD SPACES FOR THE PAWN
            //GUI: SEND GC THE SELECTED TILE TO BUILD ON
            //GC: UPDATE BOARD, RETURN BOARD TO GUI
            //GUI UPDATE BOARD

            //GC: SWAP PLAYERS

            //DETERMINE IF WON OR LOST & REACT TO THAT



            //playerController.movePlayer(1, 1, 1, 3);

            //BUILD AT A SPECIFIC LOCATION


        }
    }
}
