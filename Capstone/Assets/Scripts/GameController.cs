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

        P1 = new Player(true);

        P2 = new Player(false);

        //GC:  INITIALIZE BOARD
        board_gc = new GameBoard();

        //GUI: CREATE EMPTY BOARD
        boardHeights = board_gc.GetHeights();
        boardController.createBoard(boardHeights);
        //boardController.displayBoard(boardHeights, P1, P2);

        HelpTimer.Set();

        // Game begins with only place pawn phase
        TurnOffGameOver();
        DisablePhases();
        SwapPlacePawnPhase();
        PlayGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GamePaused())
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
                // Store the current player's selected coordinate
                Coordinates loc = playerController.GetPlacement(board_gc, CurrentPlayer); //boardController.getSelectedTile();

                // If the pawn was successfully placed in the game core board...
                if (board_gc.PlacePawn(CurrentPlayer, loc))
                {
                    // If both players have placed their pawns, turn off the "place pawn" phase, turn on the "move" phase, and swap player turns
                    if (Player.GetBothPlayersPawns()[1] != new Coordinates() && Player.GetBothPlayersPawns()[3] != new Coordinates())
                    {
                        SwapPlacePawnPhase();
                        SwapMovePhase();
                        SwapPlayerTurn();
                    }

                    // Otherwise, if only the current player has placed his pawns simply swap player turns
                    else if (CurrentPlayer.GetPlayerCoordinates()[1] != new Coordinates())
                    {
                        SwapPlayerTurn();
                    }

                    /* This function call occurs after phase changes to accommodate for animation coroutines. */
                    boardController.placePlayer(board_gc.GetHeights(), loc, P1, P2);

                    HelpTimer.Set();
                }
                else
                {
                    // Should we notify the player that they are not clicking a valid tile?

                    // Debug.Log("Failed to place player's pawn :(");
                }
            }
            else if (CanMove())
            {
                if (CurrentPlayer.HasNoMoves(board_gc))
                {
                    // GAME OVER: CURRENT PLAYER HAS NO AVAILABLE MOVES AND THEREFORE LOSES
                    if (GetPlayerTurn() == PlayerTurn.ONE) SetWinningPlayer(PlayerTurn.TWO);
                    else SetWinningPlayer(PlayerTurn.ONE);

                    DisablePhases();
                    //board.SetActive(false);

                    HelpTimer.TurnOff();

                    ClearGame(board_gc);
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

                        Debug.Log("ALL PAWNS? " + Player.GetBothPlayersPawns()[0].X + "," + Player.GetBothPlayersPawns()[0].Y + " " + Player.GetBothPlayersPawns()[1].X + "," + Player.GetBothPlayersPawns()[1].Y + " " + Player.GetBothPlayersPawns()[2].X + "," + Player.GetBothPlayersPawns()[2].Y + " " + Player.GetBothPlayersPawns()[3].X + "," + Player.GetBothPlayersPawns()[3].Y);
                        Debug.Log("current player has this pawn? " + CurrentPlayer.HasThisPawn(curLoc));
                        // Collect the first tile
                        if (curLoc != null && CurrentPlayer.HasThisPawn(curLoc)) // Make sure the tile is not null and is the location of a pawn
                        {
                            if (curLoc != null) Debug.Log("curLoc = " + curLoc.X + " " + curLoc.Y);

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
                        Debug.Log(newLoc.X + " - " + newLoc.Y);

                        if (playerController.GetMove(board_gc, CurrentPlayer) != new Coordinates(-1, -1))
                        {
                            Debug.Log("Execute");

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
                                    Coordinates[] Pawns = Player.GetBothPlayersPawns();

                                    // Record the fact that the second tile has been collected for the "move" phase. Then turn off the "move" phase
                                    CollectedSecondTile();
                                    SwapMovePhase();

                                    // Turn on the "build" phase
                                    SwapBuildPhase();

                                    // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                                    boardController.unhighlightTiles(validTiles);
                                    validTiles.Clear();
                                    /* This function call occurs after phase changes to accommodate for animation coroutines. */
                                    boardController.movePlayer(curLoc, newLoc, P1, P2, board_gc);

                                    HelpTimer.Set();
                                }
                                if (moveStatus == MoveType.WINNING)
                                {
                                    // GAME OVER: NOTIFY CURRENT PLAYER THAT THEY WIN
                                    if (GetPlayerTurn() == PlayerTurn.ONE) SetWinningPlayer(PlayerTurn.ONE);
                                    else SetWinningPlayer(PlayerTurn.TWO);

                                    DisablePhases();
                                    //board.SetActive(false);

                                    HelpTimer.TurnOff();

                                    ClearGame(board_gc);
                                }
                                else if (moveStatus == MoveType.INVALID)
                                {
                                    // Should we notify the player that they are not clicking a valid tile?
                                }
                            }
                        }

                    }
                }
            }
            else if (CanBuild())
            {
                //If not currently waiting for any tiles, change this to wait for tiles
                if (!WaitingForFirstTile() && !WaitingForSecondTile())
                {
                    ReadyForTwoTiles();

                    // Since the build must occur from the moved pawn, curLoc becomes newLoc
                    curLoc = newLoc;

                    // Get the coordinates of available moves surrounding that pawn and then highlight those tiles
                    validTiles = board_gc.AvailableBuilds(curLoc);

                    if (validTiles.Count == 0)
                    {
                        // GAME OVER: THE MOVED PAWN HAS NO AVAILABLE BUILDS AND THEREFORE THE CURRENT PLAYER LOSES
                        if (GetPlayerTurn() == PlayerTurn.ONE) SetWinningPlayer(PlayerTurn.TWO);
                        else SetWinningPlayer(PlayerTurn.ONE);

                        DisablePhases();
                        //board.SetActive(false);

                        HelpTimer.TurnOff();

                        ClearGame(board_gc);
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

                    if (playerController.GetBuild(board_gc, CurrentPlayer) != new Coordinates(-1, -1))
                    {
                        // If the pawn was successfully moved in the game core board...
                        if (board_gc.BuildPiece(curLoc, newLoc))
                        {
                            // Record the fact that the second tile has been collected for the "build" phase. Then turn off the "build" phase
                            CollectedSecondTile();
                            SwapBuildPhase();

                            // Turn on the "move" phase
                            SwapMovePhase();

                            SwapPlayerTurn();

                            // Unhighlight the highlighted tiles, clear the pawns from the board then re-display them
                            boardController.unhighlightTiles(validTiles);
                            validTiles.Clear();

                            /* This function call occurs after phase changes to accommodate for animation coroutines. */
                            //BUILD
                            boardController.buildLevel(board_gc.GetHeights(), newLoc);

                            HelpTimer.Set();
                        }
                        else
                        {
                            // Should we notify the player that they are not clicking a valid tile?
                        }
                    }
                }
            }
        }
    }
}
