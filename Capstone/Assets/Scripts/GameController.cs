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
    Player P2;
    Coordinates curLoc;
    Coordinates newLoc;
    List<Coordinates> validTiles;

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

    //get GameObject’s material and color
    MeshRenderer Renderer;

    int Col = 5, Row = 5;

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

        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();


        //INITIALIZE GAME: AI HARD, AI EASY, NETWORK?
        //  OPPONENT'S USERNAME
        //  STARTING PLAYER?
        SetPlayerTurn(PlayerTurn.ONE);

        //GC:  INITIALIZE BOARD
        board_gc = new GameBoard();

        //GUI: CREATE EMPTY BOARD
        boardHeights = board_gc.GetHeights();
        boardController.createBoard(boardHeights);
        boardController.displayBoard(boardHeights);

        // GUI: GET A USERNAME FROM USER
        string username = "Player one";
        P1 = new Player(true, username);


        // GET USERNAME FROM OPPONENT
        username = "Player two";
        P2 = new Player(false, username);
        

        //player1TurnActive = true;
        if (GetPlayerTurn() == PlayerTurn.ONE)
            Debug.Log("P1's turn!");




        //player.name = ("X: " + row + " Y: " + col);
        //player.transform.parent = playerParent.transform;

        
        

        //who is starting player?

        

        // Game begins with no place pawn, move, or build phase
        DisablePhases();
        SwapPlacePawnPhase();
        //SwapMovePhase();
        //SwapBuildPhase();
    }

    // Update is called once per frame
    void Update()
    {

        if (CanPlacePawn())
        {
            if (Input.GetMouseButtonDown(0))
            {
                // If the mouse was clicked, store that coordinate
                Coordinates loc = boardController.getSelectedTile();

                // If the pawn was successfully placed in the game core board...
                if (board_gc.PlacePawn(P1, loc))
                {
                    // Clear the pawns from the board then re-display them
                    boardController.clearBoard();
                    boardController.displayBoard(board_gc.GetHeights());

                    // Turn off the "place pawn" phase and turn on the "move" phase
                    SwapPlacePawnPhase();
                    SwapMovePhase();
                }
                else
                {
                    // Should we notify the player that they are not clicking a valid tile?
                }
            }
        }
        if (CanMove())
        {
            //If not currently waiting for any tiles, change this to wait for tiles
            if (!WaitingForFirstTile() && !WaitingForSecondTile())
                ReadyForTwoTiles();
            else if (WaitingForFirstTile())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // If the mouse was clicked, store that new coordinate
                    curLoc = boardController.getSelectedTile();

                    // Collect the first tile
                    if (curLoc != null && Player.IsAPawn(curLoc)) // Make sure the tile is not null and is the location of a pawn
                    {
                        Debug.Log("GameController: collect first tile");

                        // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                        validTiles = board_gc.AvailableMoves(curLoc);
                        boardController.highlightValidTiles(validTiles);

                        // Record the fact that the first tile has been collected for the "move" phase. Will begin waiting for the second tile
                        CollectedFirstTile();
                    }
                    else
                    {
                        // Should we notify the player that they are not clicking a valid tile?
                    }
                }
            }
            else //WaitingForSecondTile()
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // If the mouse was clicked, store that new coordinate
                    newLoc = boardController.getSelectedTile();

                    // Collect the second tile
                    MoveType moveStatus = board_gc.MovePawn(P1, curLoc, newLoc);

                    // If the pawn was successfully moved in the game core board...
                    if (moveStatus == MoveType.VALID || moveStatus == MoveType.WINNING)
                    {
                        //Section for debugging
                        Debug.Log("GameController: collected second tile and moved pawn");
                        Coordinates[] Pawns = Player.GetBothPlayersPawns();
                        Debug.Log("All pawns: " + Pawns[0].X + "," + Pawns[0].Y + "   " + Pawns[1].X + "," + Pawns[1].Y + "   " + Pawns[2].X + "," + Pawns[2].Y + "   " + Pawns[3].X + "," + Pawns[3].Y);
                        //Debug.Log("Pawn1: " + P1.GetPlayerCoordinates()[0].X + ", " + P1.GetPlayerCoordinates()[0].Y);
                        //Debug.Log("Pawn2: " + P1.GetPlayerCoordinates()[1].X + ", " + P1.GetPlayerCoordinates()[1].Y);


                        // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                        boardController.unhighlightTiles(validTiles);
                        boardController.clearBoard();
                        boardController.displayBoard(board_gc.GetHeights());

                        // Record the fact that the second tile has been collected for the "move" phase. Then turn off the "move" phase
                        CollectedSecondTile();
                        validTiles.Clear();
                        SwapMovePhase();

                        //For testing purposes, return to place pawn phase
                        //SwapPlacePawnPhase();
                        SwapBuildPhase();
                    }
                    if (moveStatus == MoveType.WINNING)
                    {
                        //NOTIFY PLAYER!!!!!!!!!!!!
                    }
                    else if (moveStatus == MoveType.INVALID)
                    {
                        // Should we notify the player that they are not clicking a valid tile?
                    }
                }
            }
        }
        if (CanBuild())
        {
            //If not currently waiting for any tiles, change this to wait for tiles
            if (!WaitingForFirstTile() && !WaitingForSecondTile())
                ReadyForTwoTiles();
            else if (WaitingForFirstTile())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // If the mouse was clicked, store that new coordinate
                    curLoc = boardController.getSelectedTile();

                    // Collect the first tile
                    if (curLoc != null && Player.IsAPawn(curLoc)) // Make sure the tile is not null and is the location of a pawn
                    {
                        Debug.Log("GameController: collect first tile for build");

                        // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                        validTiles = board_gc.AvailableBuilds(curLoc);
                        boardController.highlightValidTiles(validTiles);

                        // Record the fact that the first tile has been collected for the "build" phase. Will begin waiting for the second tile
                        CollectedFirstTile();
                    }
                    else
                    {
                        // Should we notify the player that they are not clicking a valid tile?
                    }
                }
            }
            else //WaitingForSecondTile()
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // If the mouse was clicked, store that new coordinate
                    newLoc = boardController.getSelectedTile();

                    // If the pawn was successfully moved in the game core board...
                    if (board_gc.BuildPiece(curLoc, newLoc))
                    {
                        //Section for debugging
                        Debug.Log("GameController: collected second tile and built piece");

                        // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                        boardController.unhighlightTiles(validTiles);
                        boardController.clearBoard();
                        boardController.displayBoard(board_gc.GetHeights());

                        // Record the fact that the second tile has been collected for the "move" phase. Then turn off the "move" phase
                        CollectedSecondTile();
                        validTiles.Clear();
                        SwapBuildPhase();

                        //For testing purposes, return to place pawn phase
                        SwapPlacePawnPhase();
                    }
                    else
                    {
                        // Should we notify the player that they are not clicking a valid tile?
                    }
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

        
    }
}
