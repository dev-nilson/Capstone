using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int[,] Grid;
    int Col = 5, Row = 5;
    int Horizontal = 5, Vertical = 5;
    public GameObject prefab;

    void Start()
    {
        Grid = new int[Col, Row];
        CreateEmptyBoard();
    }

    private void CreateEmptyBoard()
    {
        for (var i = 0; i < Col; i++)
        {
            for (var j = 0; j < Row; j++)
            {
                Grid[i, j] = Random.Range(0, 10);
                GameObject tile = new GameObject("X: " + i + "Y: " + j);
                Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);
                tile.transform.position = new Vector3(i - (Horizontal + 1f), j - (Vertical - 1f));
            }
        }
    }
}
