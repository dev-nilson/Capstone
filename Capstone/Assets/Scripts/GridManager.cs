using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public static GameObject[,] Grid;
    public static Coordinates selectedTile;

    GameController gameController;

    int Col = 5, Row = 5;
    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject tile4;
    public GameObject tile5;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;

    public GameObject player1;
    public GameObject player2;

    public GameObject parent;
    public GameObject levelParent;

    private Vector3 StartPoint;


    GameObject gridSpace;
    GameObject child;

    Color MouseOverColor = Color.cyan;
    Color Original;

    //get GameObject’s material and color
    MeshRenderer Renderer;

    void Start()
    {
        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();
        //get original color of the GameObject
        Original = Renderer.material.color;
    }

    public void createBoard(int[,] temp)
    {
        Grid = new GameObject[Col, Row];
        System.Random rnd = new System.Random();

        //place all tiles
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                int tileNum = rnd.Next(1, 6);   // creates a number between 1 and 5
                if(tileNum == 1)
                {
                    gridSpace = Instantiate(tile1, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 2)
                {
                    gridSpace = Instantiate(tile2, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 3)
                {
                    gridSpace = Instantiate(tile3, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 4)
                {
                    gridSpace = Instantiate(tile4, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                else if (tileNum == 5)
                {
                    gridSpace = Instantiate(tile5, new Vector3(j, Row - i, 0), Quaternion.identity);
                }
                gridSpace.name = (i + " " + j);
                gridSpace.transform.parent = parent.transform;
                Grid[i, j] = gridSpace;
            }
        }

        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(90, 0, 0);

        /*child = Instantiate(level1, Grid[0, 1].transform.position, Grid[0, 1].transform.rotation);
        child.transform.rotation = Quaternion.Euler(180, 0, 0);
        child.transform.position = new Vector3(Grid[0, 1].transform.position.x, 1f, Grid[0, 1].transform.position.z);
        child.transform.parent = levelParent.transform;*/
    }

    public void displayBoard(int[,] temp, Player P1, Player P2)
    {
        //place all levels
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                if (temp[i, j] == 1)
                {
                    //Debug.Log(gridSpace);
                    child = Instantiate(level1, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.rotation = Quaternion.Euler(0, 0, 0);
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, .70f, Grid[i, j].transform.position.z);
                    child.transform.localScale = new Vector3(.20f, .5f, .20f);
                }
                else if (temp[i, j] == 2)
                {
                    child = Instantiate(level2, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.rotation = Quaternion.Euler(0, 0, 0);
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 1.5f, Grid[i, j].transform.position.z);
                    child.transform.localScale = new Vector3(.36f, 1f, .36f);
                }
                else if (temp[i, j] == 3)
                {
                    child = Instantiate(level3, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.rotation = Quaternion.Euler(0, 0, 0);
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 2f, Grid[i, j].transform.position.z);
                    child.transform.localScale = new Vector3(.11f, .4f, .11f);
                }
                else if (temp[i, j] == 4)
                {
                    child = Instantiate(level4, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.rotation = Quaternion.Euler(0, 0, 0);
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 2.25f, Grid[i, j].transform.position.z);
                    child.transform.localScale = new Vector3(.165f, .3f, .165f);
                }
            }
        }

        Coordinates[] P1pawns = P1.GetPlayerCoordinates();
        Coordinates[] P2pawns = P2.GetPlayerCoordinates();

        Debug.Log("All pawns: " + P1pawns[0].X + "," + P1pawns[0].Y + "   " + P1pawns[1].X + "," + P1pawns[1].Y + "   " + P2pawns[0].X + "," + P2pawns[0].Y + "   " + P2pawns[1].X + "," + P2pawns[1].Y);
        //place all players
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                Coordinates loc = new Coordinates(i, j);
                if (P1pawns.Contains(loc))
                {
                    player1 = Instantiate(player1, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    if (temp[i, j] == 1)
                    {
                        player1.transform.position = new Vector3(Grid[i, j].transform.position.x, .7f, Grid[i, j].transform.position.z);
                    }
                    if (temp[i, j] == 2)
                    {
                        player1.transform.position = new Vector3(Grid[i, j].transform.position.x, 1.5f, Grid[i, j].transform.position.z);
                    }
                    if (temp[i, j] == 3)
                    {
                        player1.transform.position = new Vector3(Grid[i, j].transform.position.x, 2f, Grid[i, j].transform.position.z);
                    }
                    player1.transform.rotation = Quaternion.Euler(0, 0, 0);
                    player1.transform.localScale = new Vector3(.2f, .2f, .2f);
                    player1.transform.parent = Grid[i, j].transform;

                }
                else if (P2pawns.Contains(loc))
                {
                    player2 = Instantiate(player2, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    player2.transform.position = new Vector3(Grid[i, j].transform.position.x, 2f, Grid[i, j].transform.position.z);
                    if (temp[i,j] == 1)
                    {
                        player2.transform.position = new Vector3(Grid[i, j].transform.position.x, 2.5f, Grid[i, j].transform.position.z);
                    }
                    if (temp[i, j] == 2)
                    {
                        player2.transform.position = new Vector3(Grid[i, j].transform.position.x, 3.25f, Grid[i, j].transform.position.z);
                    }
                    if (temp[i, j] == 3)
                    {
                        player2.transform.position = new Vector3(Grid[i, j].transform.position.x, 3.5f, Grid[i, j].transform.position.z);
                    }
                    player2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    player2.transform.localScale = new Vector3(1f, 1f, 1f);
                    player2.transform.parent = Grid[i, j].transform;
                }
            }
        }

        //Debug.Log(Grid[0, 2].tag);
        //Grid[0, 0].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //Debug.Log(Grid[0, 0].name);
        //Debug.Log(Grid[0, 0].transform.position);
    }

    public void clearBoard()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Alien").Length);
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach(GameObject go in aliens)
        {
            Destroy(go);
        }
        Destroy(GameObject.FindWithTag("Alien"));
    }

    public static GameObject getBoardTile(int row, int col)
    {
        return Grid[row, col];
    }

    public static Coordinates getSelectedTile()
    {
        return selectedTile;
    }

    public void highlightValidTiles(List<Coordinates> locs)
    {
        for (var i = 0; i < Row; ++i)
        {
            for (var j = 0; j < Col; ++j)
            {
                if (locs.Contains(new Coordinates(i,j)))
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }
            }
        }
    }
    public void highlightValidTiles(int[,] temp)
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                if (temp[i, j] == 0)
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }
            }
        }
    }

    public void unhighlightTiles(List<Coordinates> locs)
    {
        for (var i = 0; i < Row; ++i)
        {
            for (var j = 0; j < Col; ++j)
            {
                if (locs.Contains(new Coordinates(i, j)))
                {
                    Grid[i, j].GetComponent<Renderer>().material.SetColor("_Color", Original);
                }
            }
        }
    }

    /*public Coordinates GetPlayerClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Col; j++)
                {
                    if (Grid[i, j].transform.position == transform.position)
                    {
                        int x = Int32.Parse(Grid[i, j].name.Split(' ')[0]);
                        int y = Int32.Parse(Grid[i, j].name.Split(' ')[1]);
                        
                        selectedTile = new Coordinates(x, y);
                        Debug.Log(x + " " + y);
                        return selectedTile;
                    }
                }
            }
        }
        selectedTile = new Coordinates(-1, -1);

        return selectedTile;
    }*/

    public void OnMouseDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (var i = 0; i < Row; i++)
            {
                for (var j = 0; j < Col; j++)
                {
                    if (Grid[i, j].transform.position == transform.position)
                    {
                        int x = Int32.Parse(Grid[i, j].name.Split(' ')[0]);
                        int y = Int32.Parse(Grid[i, j].name.Split(' ')[1]);

                        selectedTile = new Coordinates(x, y);
                        //Debug.Log(x + " " + y);
                    }
                }
            }
        }
        
    }
}


