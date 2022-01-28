﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{

    GameObject gridManager;
    GridManager Grid_M;
    GameObject clicked;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("GridManager");
        Grid_M = gridManager.GetComponent<GridManager>();

        //Debug.Log(Grid_M.Grid[0, 1].name);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = this.gameObject;
            Debug.Log(clicked);
        }
    }

    private void Update()
    {
        OnMouseDown();
    }
}

