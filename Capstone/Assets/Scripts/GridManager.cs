using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public GameObject[,] Grid;
    int Col = 5, Row = 5;
    public GameObject prefab;
    public GameObject child;
    public GameObject parent;


    void Start()
    {
        Grid = new GameObject[Col, Row];
        CreateEmptyBoard();

        //Debug.Log(Grid[1,1].name);
        Grid[0, 0].GetComponent<Renderer>().material.color = Color.green;
    }

    void CreateEmptyBoard()
    {
        //Grid = new GameObject[Col, Row];

        /*for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                child = Instantiate(prefab, new Vector3(j, Row - i, 0), Quaternion.identity);
                child.name = ("X: " + i + " Y: " + j);
                child.transform.SetParent(parent.transform);
                Grid[i,j] = child;
            }
        }

        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(70, 0, 0);*/

        for (var i = 0; i < Row; i++)
        {
            for (var j = 0; j < Col; j++)
            {
                child = Instantiate(prefab, new Vector3(j, Row - i, 0), Quaternion.identity);
                child.name = ("X: " + i + " Y: " + j);
                child.transform.parent = parent.transform;
                Grid[i, j] = child;
            }
        }
        parent.transform.position = new Vector3(0f, 0f, 0f);
        parent.transform.localScale = new Vector3(5f, 5f, 1f);
        parent.transform.rotation = Quaternion.Euler(70, 0, 0);

        /*Vector3 sumVector = new Vector3(0f, 0f, 0f);

        foreach (Transform child in parent.transform)
        {
            sumVector += child.position;
        }

        Vector3 groupCenter = sumVector / parent.transform.childCount;*/

        //parent.transform.position = groupCenter;

        //parent.transform.RotateAround(groupCenter, parent.transform.V, 90);
    }

}


