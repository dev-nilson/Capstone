using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{

    void Update()
    {
        
    }

    void GetClickPostion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = transform.position;

            Debug.Log(clickPosition);
        }
    }
}

