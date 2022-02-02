using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] Grid;
    int Col = 5, Row = 5;
    public GameObject prefab;
    public GameObject parent;
    GameObject child;

    public void displayBoard()
    {
        Grid = new GameObject[Col, Row];

        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                // IF THE HEIGHT IS 0 THEN I WILL JUST CREATE THE TILE
                child = Instantiate(prefab, new Vector3(j, Row - i, 0), Quaternion.identity);
                child.name = ("X: " + i + " Y: " + j);
                child.transform.parent = parent.transform;
                //Debug.Log(child.transform.position);
                Grid[i, j] = child;

                //IF HEIGHT IS 1 THEN BUILD TILE PLUE ONE LEVEL

                // ETC

                //
            }
        }
        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(90, 0, 0);

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


