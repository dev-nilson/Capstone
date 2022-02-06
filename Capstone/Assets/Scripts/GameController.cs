/*
 *  Author: Laura Grace Ashburn
 *  Description: ...
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
        //INITIALIZE GAME: AI HARD, AI EASY, NETWORK?
        //  OPPONENT'S USERNAME
        //  STARTING PLAYER?

        //GC:  INITIALIZE BOARD
        GameBoard board = new GameBoard();
        //GUI: CREATE EMPTY BOARD
        int[,] boardHeights = board.GetHeights();
        boardController.displayBoard(boardHeights);
        //boardController.highlightValidTiles(boardHeights);


        // GUI: GET A USERNAME FROM USER
        string username = "Player one";
        bool local = true;

        //GC: CREATE PLAYERS
        Player P1 = new Player(local, username);

        //who is starting player?

        //PLACE PLAYERS
        //GUI: GET COORDINATE FROM PLAYER
        //playerController.placePlayer(1, 1);

        //GC: UPDATE BOARD AND PASS BACK

        //GUI: GET COORDINATE FROM PLAYER
        //playerController.placePlayer(1, 2);

        //GC: UPDATE BOARD AND PASS BACK



        //LOOP WHILE NO ERROR
            //MOVE PLAYERS ------
            //GC: IF NEITHER OF PLAYER'S PAWNS CAN MOVE, YOU LOSE (break loop)
            //GC: TELL GUI IF ONE OF THE PAWNS HAS NO MOVES
            //GUI: TELL GC WHICH PAWN WAS CLICKED
            //GC: TELL GUI WHAT MOVES ARE VALID FOR THAT PAWN
            //GUI: HIGHLIGHT THE VALID MOVES
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

    // Update is called once per frame
    void Update()
    {

    }
}
