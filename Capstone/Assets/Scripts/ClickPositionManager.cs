using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = -Vector3.one;

            //Debug.Log(clickPosition);
        }
    }
}

