using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    GridManager boardController;
    GridManager Board;

    ClickPositionManager clickPositionManager;
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
        //CREATE EMPTY BOARD
        boardController.CreateEmptyBoard();

        //PLACE PLAYERS
        playerController.placePlayer(1,1);
        playerController.placePlayer(1,2);

        //MOVE PLAYERS
        //playerController.movePlayer(1, 1, 1, 3);

        //BUILD AT A SPECIFIC LOCATION



    }

    // Update is called once per frame
    void Update()
    {

    }
}
