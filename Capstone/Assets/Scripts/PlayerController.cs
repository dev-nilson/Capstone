using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject gridManager;
    GridManager Grid_M;

    public GameObject player1;
    public GameObject player2;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("GridManager");
        Grid_M = gridManager.GetComponent<GridManager>();
    }

    public void choosePlayer()
    {

    }

    public void placePlayer(int row, int col)
    {
        player = Instantiate(player1, Grid_M.Grid[row, col].transform.position, transform.rotation);
        player.transform.position = new Vector3(Grid_M.Grid[row, col].transform.position.x, 1f, Grid_M.Grid[row, col].transform.position.z);
    }
}
