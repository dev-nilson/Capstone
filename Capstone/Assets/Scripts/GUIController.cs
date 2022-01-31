using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    GridManager gridManager;
    GridManager Grid_M;

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
        gridManager = board.GetComponent<GridManager>();
        Grid_M = gridManager.GetComponent<GridManager>();
        //clickPositionManager = player.GetComponent<ClickPositionManager>();
        playerController = player.GetComponent<PlayerController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //CREATE EMPTY BOARD
        gridManager.CreateEmptyBoard();
        playerController.placePlayer(1,1);
        playerController.placePlayer(1,2);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
