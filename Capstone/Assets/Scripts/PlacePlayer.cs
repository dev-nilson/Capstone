using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    GameObject gridManager;
    GridManager Grid_M;

    public GameObject player1;
    public GameObject player2;

    int playerTurn = 1;
    int playerOneCount = 0;
    int playerTwoCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("GridManager");
        Grid_M = gridManager.GetComponent<GridManager>();

        OnMouseDown();
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(player1, transform.position, transform.rotation);
        }
    }

    void placePlayerOne()
    {
        Instantiate(player1, transform.position, transform.rotation);
        playerOneCount++;
    }

    void placePlayerTwo()
    {
        Instantiate(player2, transform.position, transform.rotation);
    }
}
