using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    // Assign a Rigidbody component in the inspector to instantiate
    public Rigidbody level1;
    public Rigidbody level2;
    public Rigidbody level3;

    //public GameObject[,] BuildingGrid;

    private void OnMouseOver()
    {
        // mouse was clicked
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(level1, transform.position, transform.rotation);
            //GridManager.Grid[0, 0] = 
            //Debug.Log(GridManager.Grid[0,0]);
        }
    }
}
