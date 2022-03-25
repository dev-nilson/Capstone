using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtilities;

public class RotateMainCamera : MonoBehaviour
{
	GameObject boardController;
	GridManager Board;

	public Button rotateRight;
	public Button rotateLeft;

    float x = 0, z = 0;

	void Start()
	{
		Button right = rotateRight.GetComponent<Button>();
		right.onClick.AddListener(rotateRightClick);

		Button left = rotateLeft.GetComponent<Button>();
		left.onClick.AddListener(rotateLeftClick);

		boardController = GameObject.Find("GridManager");
		Board = boardController.GetComponent<GridManager>();

        //Code to find center of an object!!!
        //How do I rotate around the center point now?
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        GameObject[] tilesInGame = GameObject.FindGameObjectsWithTag("Board");
        foreach (GameObject tile in tilesInGame)
        {
            totalX += tile.transform.position.x;
            totalY += tile.transform.position.y;
            totalZ += tile.transform.position.z;
        }
        float centerX = totalX / tilesInGame.Length;
        float centerY = totalY / tilesInGame.Length;
        float centerZ = totalY / tilesInGame.Length;

        Debug.Log("center is : " + centerX + " " + centerY + " " + centerZ);

    }

    //LAURA GRACE how do I safely disable and restore phases while the board is being rotated??????

    void rotateLeftClick()
    {
        if (x == 0)
        {
            x = -8.75f;
            z = 28.75f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 90, 0);
        }
        else if (x == -8.75)
        {
            x = 20f;
            z = 37.5f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        else if (x == 20)
        {
            x = 28.75f;
            z = 8.75f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, -270);
        }
        else if (x == 28.75)
        {
            x = 0;
            z = 0;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        Debug.Log("You have clicked the left button!");

        //This code always makes the players face you when you rotate
        /*GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject player in aliens)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/
    }

    void rotateRightClick()
    {
        if (x == 0)
        {
            x = 28.75f;
            z = 8.75f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 90);
        }
        else if (x == 28.75f)
        {
            x = 20;
            z = 37.5f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        else if (x == 20)
        {
            x = -8.75f;
            z = 28.75f;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 270);
        }
        else if (x == -8.75f)
        {
            x = 0;
            z = 0;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        Debug.Log("You have clicked the right button!");

        //This code always makes the players face you when you rotate
        /*GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject player in aliens)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/
    }



    // WORK IN PROGRESS: rotate board slowly
    void rotateRightClick2()
    {
        StartCoroutine(rotateBoardRight());
    }


    IEnumerator rotateBoardRight()
    {
        StorePhases();
        DisablePhases();

        // center is (x,z) = (10.0, 18.75)
        //float radius = 21.25f;

        float DEGREES = 5;

        Debug.Log("Rotate board right begin");
        for (int i = 0; i < 90; i+=5)
        {
            // convert angle to radians
            float angle = DEGREES * Mathf.PI / 180.0f;

            // get current point
            float x = Board.transform.position.x;
            float z = Board.transform.position.z;
            Debug.Log("current x: " + x);
            Debug.Log("current z: " + z);
            
            // calculate cos & sin
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);
            Debug.Log("current cos: " + cos);
            Debug.Log("current sin: " + sin);

            float newx = cos * (x - 10.0f) - sin * (z - 18.75f) + 10.0f;
            float newz = - sin * (x - 10.0f) + cos * (z - 18.75f) + 18.75f;
            Debug.Log("new x: " + newx);
            Debug.Log("new z: " + newz);

            Board.transform.position = new Vector3(x, 0f, z);

            float f = Board.transform.rotation.z + DEGREES;
            Board.transform.rotation = Quaternion.Euler(90, 0, f);

            yield return new WaitForSeconds(.01f);
        }
        //startLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, 7f, Grid[location.X, location.Y].transform.position.z);
        //endLocation = new Vector3(Grid[location.X, location.Y].transform.position.x, levelHeight, Grid[location.X, location.Y].transform.position.z);

        //for (float i = startLocation.y; i >= levelHeight; i -= .2f)
        //{
        //    endLocation.y = i;
        //    child.transform.position = endLocation;
        //    yield return new WaitForSeconds(.001f);
        //}

        yield return new WaitForSeconds(1.0f);
        RestorePhases();
    }
}
