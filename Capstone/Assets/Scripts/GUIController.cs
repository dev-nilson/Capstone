using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    //PLACE PLAYERS
    //BUILD
    //CHANGE SCREENS??

    GridManager gridManager;

    public GameObject[,] Grid;
    public GameObject prefab;
    public GameObject parent;
    public GameObject board;

    GameObject child;

    int Col = 5, Row = 5;

    void Awake()
    {
        gridManager = board.GetComponent<GridManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //CREATE BOARD
        gridManager.CreateEmptyBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
