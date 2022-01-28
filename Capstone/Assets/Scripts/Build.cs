using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    // Assign a Rigidbody component in the inspector to instantiate
    public Rigidbody level1;
    public Rigidbody level2;
    public Rigidbody level3;

    private GameObject prefab;
    private GameObject child;
    private GameObject parent;

    GridManager gridManager;

    //public GameObject[,] BuildingGrid;

    private void OnMouseOver()
    {
        // mouse was clicked
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(level1, transform.position, transform.rotation);
            clone.transform.rotation = Quaternion.Euler(180, 0, 0);
            clone.transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
            //GridManager.Grid[0, 0] = clone;
            //Debug.Log(GridManager.Grid[0,0]);*/

            /*Rigidbody clone;
            clone = Instantiate(level1, transform.position, transform.rotation);
            child.transform.parent = parent.transform;
            //Grid[i, j] = child;*/

            //Debug.Log(Input.mousePosition);
        }


    }

    private void OnMouseDown()
    {
        //Debug.Log(gridManager.Grid[0, 0]);
        // Check for mouse input
        /*if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            Debug.Log("This hit at " + hit.point);
        }*/
    }
}
