using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] Grid;
    public GameObject[,] Buildings;
    int Col = 5, Row = 5;
    public GameObject tile;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public GameObject parent;
    GameObject gridSpace;
    GameObject child;


    public void displayBoard(int[,] temp)
    {
        Grid = new GameObject[Col, Row];
        Buildings = new GameObject[Col, Row];

        //place all tiles
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                gridSpace = Instantiate(tile, new Vector3(j, Row - i, 0), Quaternion.identity);
                gridSpace.name = ("X: " + i + " Y: " + j);
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
                if(temp[i,j] == 0)
                {
                    child = Instantiate(level3, Grid[i, j].transform.position, Grid[i, j].transform.rotation);
                    child.transform.rotation = Quaternion.Euler(180, 0, 0);
                    child.transform.parent = Grid[i, j].transform;
                    child.transform.position = new Vector3(Grid[i, j].transform.position.x, 3f, Grid[i, j].transform.position.z);
                }
            }
        }

        

        Grid[0, 0].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    public void Board(GameObject[,] updatedBoard)
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                Grid[i, j] = updatedBoard[i,j];
            }
        }
    }
}


