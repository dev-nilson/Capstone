using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GridManager gridManager;
        gridManager = new GridManager();
        gridManager.CreateEmptyBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
