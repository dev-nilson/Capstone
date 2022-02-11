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
    public GameObject parent;

    GameObject boardController;
    GridManager Board;

    //public GameObject[,] BuildingGrid;

    private void Start()
    {
        boardController = GameObject.Find("GridManager");
        Board = boardController.GetComponent<GridManager>();
        //Debug.Log(Board.Grid[0, 0].transform.position);
    }

<<<<<<< HEAD
    private void OnMouseOver()
    {
        if (GameUtilities.CanBuild())
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

                //Debug.Log(transform.position);

                //Debug.Log(Board.Grid[,].name);

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
            //Debug.Log(boardController.Board[0, 0].name);
        }
    }
=======
    //private void OnMouseOver()
    //{
    //    // mouse was clicked
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        // Instantiate the projectile at the position and rotation of this transform
    //        Rigidbody clone;
    //        clone = Instantiate(level1, transform.position, transform.rotation);
    //        clone.transform.rotation = Quaternion.Euler(180, 0, 0);
    //        clone.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

    //        //Debug.Log(transform.position);

    //        //Debug.Log(Board.Grid[,].name);

    //        //GridManager.Grid[0, 0] = clone;
    //        //Debug.Log(GridManager.Grid[0,0]);



    //        /*//LOOP THROUGH EACH TILE
    //        for (int i = 0; i < 5; i++)
    //        {
    //            for (int j = 0; j < 5; j++)
    //            {
    //                Rigidbody clone;
    //                clone = Instantiate(level1, Grid_M.Grid[i, j].transform.position, transform.rotation);
    //                clone.transform.rotation = Quaternion.Euler(180, 0, 0);
    //                clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 1f, Grid_M.Grid[i, j].transform.position.z);
    //            }
    //        }

    //        for (int i = 0; i < 5; i++)
    //        {
    //            for (int j = 0; j < 5; j++)
    //            {
    //                Rigidbody clone;
    //                clone = Instantiate(level2, Grid_M.Grid[i, j].transform.position, transform.rotation);
    //                clone.transform.rotation = Quaternion.Euler(180, 0, 0);
    //                clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 1.5f, Grid_M.Grid[i, j].transform.position.z);
    //            }
    //        }

    //        for (int i = 0; i < 5; i++)
    //        {
    //            for (int j = 0; j < 5; j++)
    //            {
    //                Rigidbody clone;
    //                clone = Instantiate(level3, Grid_M.Grid[i, j].transform.position, transform.rotation);
    //                clone.transform.rotation = Quaternion.Euler(180, 0, 0);
    //                clone.transform.position = new Vector3(Grid_M.Grid[i, j].transform.position.x, 2f, Grid_M.Grid[i, j].transform.position.z);
    //            }
    //        }*/

    //    }

    //    //Instantiate(level1, gridManager.Grid[0, 0].transform.position, transform.rotation);
    //    //Debug.Log(boardController.Board[0, 0].name);
    //}
>>>>>>> 755bf74d4dfdef270ae29894d0472e31606539bb

}
