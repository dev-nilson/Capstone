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

    GameObject gridManager;
    GridManager Grid_M;

    //public GameObject[,] BuildingGrid;

    private void Start()
    {
        gridManager = GameObject.Find("GridManager");
        Grid_M = gridManager.GetComponent<GridManager>();

        //Debug.Log(Grid_M.Grid[0, 0].name);
    }

    private void OnMouseOver()
    {
        //gridManager.Grid[0, 0];
        // mouse was clicked
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(level1, transform.position, transform.rotation);
            clone.transform.rotation = Quaternion.Euler(180, 0, 0);
            clone.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            //GridManager.Grid[0, 0] = clone;
            //Debug.Log(GridManager.Grid[0,0]);



            /*//LOOP THROUGH EACH TILE
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Rigidbody clone;
                    clone = Instantiate(level1, Grid_M.Grid[i, j].transform.position, transform.rotation);
                    clone.transform.rotation = Quaternion.Euler(180, 0, 0);
                    clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 1f, Grid_M.Grid[i, j].transform.position.z);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Rigidbody clone;
                    clone = Instantiate(level2, Grid_M.Grid[i, j].transform.position, transform.rotation);
                    clone.transform.rotation = Quaternion.Euler(180, 0, 0);
                    clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 1.5f, Grid_M.Grid[i, j].transform.position.z);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Rigidbody clone;
                    clone = Instantiate(level3, Grid_M.Grid[i, j].transform.position, transform.rotation);
                    clone.transform.rotation = Quaternion.Euler(180, 0, 0);
                    clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 2f, Grid_M.Grid[i, j].transform.position.z);
                }
            }*/

        }

        //Instantiate(level1, gridManager.Grid[0, 0].transform.position, transform.rotation);
       // Debug.Log(gridManager.Grid[0, 0].name);
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
