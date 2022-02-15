using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    GridManager boardController;
    GridManager Board;

    public GameObject player1;
    GameObject player;

    public GameObject playerParent;
    GameObject child;

    public GameObject[,] PlayerPosition;
    GameObject[,] tempPlayer;

    int[,] boardHeights;
    private bool moveStatus; //False means waiting for first click, True means waiting for second click
    private static Coordinates curLoc;
    private static Coordinates newLoc;

    public GameObject board;

    int Col = 5, Row = 5;


    // Start is called before the first frame update
    void Start()
    {
        boardController = board.GetComponent<GridManager>();
        Board = boardController.GetComponent<GridManager>();
        moveStatus = false;
    }

    public Coordinates GetPlayerClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Col; j++)
                {
                    if (player1.transform.position == transform.position)
                    {
                        /*int x = Int32.Parse(Grid[i, j].name.Split(' ')[0]);
                        int y = Int32.Parse(Grid[i, j].name.Split(' ')[1]);

                        selectedTile = new Coordinates(x, y);
                        Debug.Log(x + " " + y);
                        return selectedTile;*/
                        Debug.Log(player1.transform.position);
                    }
                }
            }
        }
        return null;
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

    private void OnMouseOver()
    {
        if (GameUtilities.CanPlacePawn())
        {
            //CODE TO PLACE A PLAYER
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPosition = new GameObject[Row, Col];
                //playerParent = Board.getBoardTile(row, col);

                player = Instantiate(player1, transform.position, transform.rotation);
                player.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                //player.name = ("X: " + row + " Y: " + col);
                player.transform.parent = playerParent.transform;

                //PlayerPosition[row, col] = player;
               
            }

        }

        //CODE TO MOVE A PLAYER

        if (Input.GetMouseButtonDown(0))
        {
            if (GameUtilities.CanMove())
            {
                if (moveStatus == false)
                {
                    //Get the player they want to move
                    curLoc = boardController.getSelectedTile();
                }
                else
                {
                    //Get the player they want to move
                    newLoc = boardController.getSelectedTile();

                    //DESTROY THE PAWN

                    //CREATE A NEW PAWN

                }
            }
        }

    }

    public bool FirstTileClicked()
    {
        return !moveStatus;
    }

    /*void FireRay()
    {

        Ray ray = new Ray(transform.position, transform.localPosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        GameObject hitObject = hitData.transform.gameObject;
        Debug.Log(hitObject.tag);
    }*/
}
