using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        foreach (GameObject go in aliens)
        {
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        foreach (GameObject go in aliens)
        {
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/
    }
}
