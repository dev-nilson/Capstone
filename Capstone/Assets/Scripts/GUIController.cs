using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    //PLACE PLAYERS
    //BUILD
    //CHANGE SCREENS??

    GridManager gridManager;
    ClickPositionManager clickPositionManager;


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
        clickPositionManager = player.GetComponent<ClickPositionManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //CREATE BOARD
        gridManager.CreateEmptyBoard();

        player = Instantiate(player1, clickPositionManager.transform.position, clickPositionManager.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
