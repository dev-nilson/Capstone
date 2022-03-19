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

    int x = 0, z = 0;

	void Start()
	{
		Button right = rotateRight.GetComponent<Button>();
		right.onClick.AddListener(rotateRightClick);

		Button left = rotateLeft.GetComponent<Button>();
		left.onClick.AddListener(rotateLeftClick);

		boardController = GameObject.Find("GridManager");
		Board = boardController.GetComponent<GridManager>();

        float totalX = 0f;
        float totalY = 0f;

        GameObject[] tilesInGame = GameObject.FindGameObjectsWithTag("Board");
        foreach (GameObject tile in tilesInGame)
        {
            totalX += tile.transform.position.x;
            totalY += tile.transform.position.y;
        }
        float centerX = totalX / tilesInGame.Length;
        float centerY = totalY / tilesInGame.Length;

        Debug.Log("center is : " + centerX + " " + centerY);

        Vector3 center = boardController.GetComponent<Renderer>().bounds.center;

        Debug.Log("center of gridmanager is : " + center);

        boardController.transform.RotateAround(center, Vector3.right , 1 * Time.deltaTime);

    }
    void rotateLeftClick()
    {
        float timeCount = 0.0f;

        if (x == 0)
        {
            x = -5;
            z = 25;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 90, 0);
        }
        else if (x == -5)
        {
            x = 20;
            z = 30;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        else if (x == 20)
        {
            x = 25;
            z = 5;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, -270);
        }
        else if (x == 25)
        {
            x = 0;
            z = 0;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        Debug.Log("You have clicked the right button!");

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
            x = 25;
            z = 5;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 90);
        }
        else if (x == 25)
        {
            Debug.Log("HERE");

            x = 20;
            z = 30;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 180);
        }
        else if (x == 20)
        {
            x = -5;
            z = 25;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 270);
        }
        else if (x == -5)
        {
            x = 0;
            z = 0;
            Board.transform.position = new Vector3(x, 0f, z);
            Board.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        Debug.Log("You have clicked the left button!");

        /*GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject go in aliens)
        {
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/
    }
}
