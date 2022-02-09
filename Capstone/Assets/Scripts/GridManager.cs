using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GameObject[,] Grid;
    public static Coordinates selectedTile;

    int Col = 5, Row = 5;
    public GameObject tile;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public GameObject parent;
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


    public void displayBoard(int[,] temp)
    {
        Grid = new GameObject[Col, Row];

        //place all tiles
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                gridSpace = Instantiate(tile, new Vector3(j, Row - i, 0), Quaternion.identity);
                gridSpace.name = (i + " " + j);
                gridSpace.transform.parent = parent.transform;
                Grid[i, j] = gridSpace;
            }
        }
        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(90, 0, 0);

        //place all tiles
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                if (temp[i, j] == 1)
                {
                    child = Instantiate(level1, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.rotation = Quaternion.Euler(180, 0, 0);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 3f, Grid[i, j].transform.position.z);
                }
                else if (temp[i, j] == 2)
                {
                    child = Instantiate(level2, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.rotation = Quaternion.Euler(180, 0, 0);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 3f, Grid[i, j].transform.position.z);
                }
                else if (temp[i, j] == 3)
                {
                    child = Instantiate(level3, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.rotation = Quaternion.Euler(180, 0, 0);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 3f, Grid[i, j].transform.position.z);
                }
            }
        }

        Grid[0, 0].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //Debug.Log(Grid[0, 0].name);
        //Debug.Log(Grid[0, 0].transform.position);

    }

    public GameObject getBoardTile(int row, int col)
    {
        return Grid[row, col];
    }

    public Coordinates getSelectedTile()
    {
        return selectedTile;
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

    public Coordinates GetPlayerClick()
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
        return null;
    }

    /*public void OnMouseDown()
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
                    }
                }
            }
        }
    }*/
}


