using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public GameObject[,] Grid;
    int Col = 5, Row = 5;
    int Horizontal = 5, Vertical = 5;
    public GameObject prefab;
    public GameObject child;
    public GameObject parent;


    void Start()
    {
        Grid = new GameObject[Col, Row];
        CreateEmptyBoard();

        Debug.Log(Grid[1,1].name);
        Grid[0, 0].GetComponent<Renderer>().material.color = Color.green;
    }

    private void CreateEmptyBoard()
    {
        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                child = Instantiate(prefab, new Vector3(j, Row - i, 0), Quaternion.identity);
                child.name = ("X: " + i + " Y: " + j);
                child.transform.parent = parent.transform;
                Grid[i,j] = child;
            }
        }

        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(70, 0, 0);
    }
}
