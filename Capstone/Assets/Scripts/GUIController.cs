using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
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
        playerController = player.GetComponent<PlayerController>();

    }

    // Start is called before the first frame update
    /*void Start()
    {
        //GC INITIALIZE BOARD
        //CREATE EMPTY BOARD
        boardController.CreateEmptyBoard();

        //CREATE PLAYERS
       /*
         * //Initialize P1;
            Console.WriteLine("P1's username: ");
            string username = Console.ReadLine();
            bool local = true;

            Player P1 = new Player(local, username);*/

        //PLACE PLAYERS
        //playerController.placePlayer(1,1);
        //playerController.placePlayer(1,2);

        //GC UPDATE BOARD AND PASS BACK 


        //LOOP WHILE NO ERROR

            //MOVE PLAYERS ------
            //IF NEITHER OF PLAYER'S PAWNS CAN MOVE, YOU LOSE (break loop)
            //till move is selected
                //GUI TELLS GC WHICH PAWN WAS CLICKED
                //GC TELLS GUI WHAT MOVES ARE VALID
                //GUI HIGHLIGHTs THE VALID MOVES
                //GUI SENDS GC WHAT TILE WAS CLICKED (break)
            //GC UPDATE BOARD AND SEND TO ME
            //I UPDATE
        
            //BUILD------
            //GC GIVES ME ALL THE VALID BUILD SPACES
            // I SEND GC THE CLICK
            //GC BUILDS, UPDATES, SEND BACK TO ME
            //I UPDATE BOARD


        //IF WON OR LOST REACT TO THAT

        //playerController.movePlayer(1, 1, 1, 3);

        //BUILD AT A SPECIFIC LOCATION



}