using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject boardController;
    GridManager Board;

    public GameObject player1;
    public GameObject player2;
    GameObject player;

    public GameObject playerParent;
    GameObject child;

    public GameObject[,] PlayerPosition;
    GameObject[,] tempPlayer;


    int Col = 5, Row = 5;


    // Start is called before the first frame update
    void Start()
    {
        boardController = GameObject.Find("GridManager");
        Board = boardController.GetComponent<GridManager>();
    }

    public void choosePlayer()
    {
    }

    /*public void movePlayer(int startRow, int startCol, int endRow, int endCol)
    {
        GameObject playerToBeDestroyed;
        playerToBeDestroyed = 

        Destroy(playerToBeDestroyed);

        player = Instantiate(player1, Grid_M.Grid[endRow, endCol].transform.position, transform.rotation);
        player.transform.position = new Vector3(Grid_M.Grid[endRow, endCol].transform.position.x, 1f, Grid_M.Grid[endRow, endCol].transform.position.z);
        player.name = ("X: " + endRow + " Y: " + endCol);
        player.transform.parent = playerParent.transform;

        PlayerPosition[endRow, endCol] = player;
        Debug.Log(PlayerPosition[1, 1]);
    }*/

    public void placePlayer(int row, int col)
    {
        PlayerPosition = new GameObject[Row, Col];

        player = Instantiate(player1, Board.getBoardTile(row, col).transform.position, transform.rotation);
        player.transform.position = new Vector3(Board.getBoardTile(row, col).transform.position.x, 1f, Board.getBoardTile(row, col).transform.position.z);
        player.name = ("X: " + row + " Y: " + col);
        player.transform.parent = playerParent.transform;

        PlayerPosition[row, col] = player;
        Debug.Log(PlayerPosition[1,1]);
    }
}
