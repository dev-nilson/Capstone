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

    public GameObject winPopup;
    public GameObject losePopup;


    public GameObject player1;

    GameObject child;

    //get GameObject’s material and color
    MeshRenderer Renderer;

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

        

        //INITIALIZE GAME: AI HARD, AI EASY, NETWORK? -- should be done via menus !!!!!!!!!!!!!
        // gameType is currently defaulting to easy. Why???
        // setGameType(GameType.EASY)

        
        //  STARTING PLAYER?
        //SetPlayerTurn(PlayerTurn.ONE);

        P1 = new Player(true, P1username);

        P2 = new Player(false, P2username);

        //GC:  INITIALIZE BOARD
        board_gc = new GameBoard();

        //GUI: CREATE EMPTY BOARD
        boardHeights = board_gc.GetHeights();
        boardController.createBoard(boardHeights);
        boardController.displayBoard(boardHeights, P1, P2);
        

        //player1TurnActive = true;
        //if (GetPlayerTurn() == PlayerTurn.ONE)
        //    Debug.Log("P1's turn!");




        //player.name = ("X: " + row + " Y: " + col);
        //player.transform.parent = playerParent.transform;


        

        // Game begins with no place pawn, move, or build phase
        DisablePhases();
        SwapPlacePawnPhase();
        //SwapMovePhase();
        //SwapBuildPhase();
    }

    // Update is called once per frame
    void Update()
    {
        Player CurrentPlayer;
        Player WaitingPlayer;

        if (GetPlayerTurn() == PlayerTurn.ONE)
        {
            CurrentPlayer = P1;
            WaitingPlayer = P2;
        }
        else
        {
            CurrentPlayer = P2;
            WaitingPlayer = P1;
        }

        if (CanPlacePawn())
        {
            //if (GetPlayerTurn() == PlayerTurn.ONE) Debug.Log("P1's turn!");
            //else Debug.Log("P2's turn!");

            // Store the current player's selected coordinate
            Coordinates loc = playerController.GetPlacement(board_gc, CurrentPlayer); //boardController.getSelectedTile();

            // If the pawn was successfully placed in the game core board...
            if (board_gc.PlacePawn(CurrentPlayer, loc))
            {
                // Clear the pawns from the board then re-display them
                boardController.clearBoard();
                boardController.displayBoard(board_gc.GetHeights(), P1, P2);

                // If both players have placed their pawns, turn off the "place pawn" phase, turn on the "move" phase, and swap player turns
                if (Player.GetBothPlayersPawns()[1] != new Coordinates() && Player.GetBothPlayersPawns()[3] != new Coordinates())
                {
                    SwapPlacePawnPhase();
                    SwapMovePhase();
                    SwapPlayerTurn();
                }

                // Otherwise, if only the current player has placed his pawns simply swap player turns
                else if (CurrentPlayer.GetPlayerCoordinates()[1] != new Coordinates())
                    SwapPlayerTurn();
            }
            else
            {
                // Should we notify the player that they are not clicking a valid tile?

                // Debug.Log("Failed to place player's pawn :(");
            }
        }
        if (CanMove())
        {
            if (CurrentPlayer.HasNoMoves(board_gc))
            {
                // GAME OVER: CURRENT PLAYER HAS NO AVAILABLE MOVES AND THEREFORE LOSES
                if (CurrentPlayer.Type() == Player.Tag.LOCAL)
                {
                    losePopup.SetActive(true);
                    Debug.Log("Local player loses: no available moves");
                }
                else
                {
                    Debug.Log("Opposing player loses: no available moves");
                    winPopup.SetActive(true);
                }
                DisablePhases();
                //board.SetActive(false);
                

            }
            else
            {
                //If not currently waiting for any tiles, change this to wait for tiles
                if (!WaitingForFirstTile() && !WaitingForSecondTile())
                    ReadyForTwoTiles();
                else if (WaitingForFirstTile())
                {
                    // If the mouse was clicked, store that new coordinate

                    curLoc = playerController.GetPawn(board_gc, CurrentPlayer, WaitingPlayer);

                    // Collect the first tile
                    if (curLoc != null && CurrentPlayer.HasThisPawn(curLoc)) // Make sure the tile is not null and is the location of a pawn
                    {
                        Debug.Log("GameController: collect first tile");

                        // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                        validTiles = board_gc.AvailableMoves(curLoc);

                        if (validTiles.Count == 0)
                        {
                            // NOTIFY PLAYER THAT THIS PAWN HAS NO AVAILABLE MOVES
                            // THEY MUST MOVE THE OTHER PAWN
                        }
                        else
                        {
                            if (CurrentPlayer.Type() == Player.Tag.LOCAL) boardController.highlightValidTiles(validTiles);

                            // Record the fact that the first tile has been collected for the "move" phase. Will begin waiting for the second tile
                            CollectedFirstTile();
                        }
                    }
                    else
                    {
                        // Should we notify the player that they are not clicking a valid tile (containing a pawn)?
                    }
                }
                else //WaitingForSecondTile()
                {
                    // If the mouse was clicked, store that new coordinate
                    newLoc = playerController.GetMove(board_gc, CurrentPlayer);

                    // The new coordinate could be the current or other pawn, if so update curLoc to contain the most recently selected pawn
                    if (newLoc != null && CurrentPlayer.HasThisPawn(newLoc))
                    {
                        curLoc = newLoc;

                        if (CurrentPlayer.Type() == Player.Tag.LOCAL)
                        {
                            // Unhlighlight valid tiles for previous pawn then clear "validTiles" of those coordinates
                            boardController.unhighlightTiles(validTiles);
                            validTiles.Clear();
                            // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                            validTiles = board_gc.AvailableMoves(curLoc);
                            boardController.highlightValidTiles(validTiles);
                        }
                    }
                    // Otherwise, see if the new location would work as a move for the current pawn
                    else
                    {
                        // Collect the second tile
                        MoveType moveStatus = board_gc.MovePawn(CurrentPlayer, curLoc, newLoc);

                        // If the pawn was successfully moved in the game core board...
                        if (moveStatus == MoveType.VALID || moveStatus == MoveType.WINNING)
                        {
                            //Section for debugging
                            Debug.Log("GameController: collected second tile and moved pawn");
                            Coordinates[] Pawns = Player.GetBothPlayersPawns();

                            // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                            boardController.unhighlightTiles(validTiles);
                            boardController.clearBoard();
                            boardController.displayBoard(board_gc.GetHeights(), P1, P2);

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
                            // GAME OVER: NOTIFY CURRENT PLAYER THAT THEY WIN
                            if (CurrentPlayer.Type() == Player.Tag.LOCAL)
                            {
                                winPopup.SetActive(true);
                                Debug.Log("Local player wins: reached the third tier of a tower!");
                            }
                            else
                            {
                                losePopup.SetActive(true);
                                Debug.Log("Opposing player wins: reached the third tier of a tower!");
                            }
                            DisablePhases();
                            //board.SetActive(false);



                        }
                        else if (moveStatus == MoveType.INVALID)
                        {
                            // Should we notify the player that they are not clicking a valid tile?
                        }
                    }
                }
            }
        }
        if (CanBuild())
        {
            //If not currently waiting for any tiles, change this to wait for tiles
            if (!WaitingForFirstTile() && !WaitingForSecondTile())
            {
                Debug.Log("Testing");
                ReadyForTwoTiles();

                // Since the build must occur from the moved pawn, curLoc becomes newLoc
                curLoc = newLoc;

                // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                validTiles = board_gc.AvailableBuilds(curLoc);

                if (validTiles.Count == 0)
                {
                    // GAME OVER: THE MOVED PAWN HAS NO AVAILABLE BUILDS AND THEREFORE THE CURRENT PLAYER LOSES
                    Debug.Log("Current player loses: no available builds");
                    if (CurrentPlayer.Type() == Player.Tag.LOCAL)
                    {
                        losePopup.SetActive(true);
                        Debug.Log("Local player loses: no available builds");
                    }
                    else
                    {
                        winPopup.SetActive(true);
                        Debug.Log("Opposing player loses: no available builds");
                    }
                    DisablePhases();
                    //board.SetActive(false);
   

                }
                else
                {
                    if (CurrentPlayer.Type() == Player.Tag.LOCAL) boardController.highlightValidTiles(validTiles);

                    // Record the fact that the first tile has been collected for the "build" phase. Will begin waiting for the second tile
                    CollectedFirstTile();
                }
            }
            else //WaitingForSecondTile()
            {
                // If the mouse was clicked, store that new coordinate
                newLoc = playerController.GetBuild(board_gc, CurrentPlayer);

                // If the pawn was successfully moved in the game core board...
                if (board_gc.BuildPiece(curLoc, newLoc))
                {
                    //Section for debugging
                    Debug.Log("GameController: collected second tile and built piece on " + newLoc.X + "," + newLoc.Y);

                    // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                    boardController.unhighlightTiles(validTiles);
                    boardController.clearBoard();
                    boardController.buildAnimation(newLoc);
                    boardController.displayBoard(board_gc.GetHeights(), P1, P2);

                    // Record the fact that the second tile has been collected for the "build" phase. Then turn off the "build" phase
                    CollectedSecondTile();
                    validTiles.Clear();
                    SwapBuildPhase();

                    //For testing purposes, return to place pawn phase
                    //SwapPlacePawnPhase();
                    SwapMovePhase();

                    SwapPlayerTurn();
                }
                else
                {
                    // Should we notify the player that they are not clicking a valid tile?
                }
            }
        }
    }
}
