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

    public void CreateEmptyBoard()
    {
        Grid = new GameObject[Col, Row];

        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {

                child = Instantiate(prefab, new Vector3(j, Row - i, 0), Quaternion.identity);
                child.name = ("X: " + i + " Y: " + j);
                child.transform.parent = parent.transform;
                //Debug.Log(child.transform.position);
                Grid[i, j] = child;
            }
        }
        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}


