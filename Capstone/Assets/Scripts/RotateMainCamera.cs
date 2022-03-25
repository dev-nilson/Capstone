using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtilities;

public class RotateMainCamera : MonoBehaviour
{
    static bool rotateOn = true;

	GameObject boardController;
	GridManager Board;

	public Button rotateRight;
	public Button rotateLeft;

    float x = 0, z = 0;

	void Start()
	{
		Button right = rotateRight.GetComponent<Button>();
		right.onClick.AddListener(rotateRightClick2);

		Button left = rotateLeft.GetComponent<Button>();
		left.onClick.AddListener(rotateLeftClick2);

		boardController = GameObject.Find("GridManager");
		Board = boardController.GetComponent<GridManager>();
    }

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
            //Board.transform.position = new Vector3(x, 0f, z);
            //Board.transform.rotation = Quaternion.Euler(90, 0, 90);
            
            Board.transform.RotateAround(new Vector3(10.0f, 0f, 18.75f), Vector3.up, 90);// * Time.deltaTime);
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

    void rotateRightClick2()
    {
        if (rotateOn)
            StartCoroutine(rotateBoardRight());
    }

    IEnumerator rotateBoardRight()
    {
        PauseGame();

        int degrees = 1;
        for (int i = 0; i < 30; i += degrees)
        {
            Board.transform.RotateAround(new Vector3(10.0f, 0f, 18.75f), Vector3.up, -degrees);

            yield return new WaitForSeconds(.05f);
        }

        PlayGame();
    }

    void rotateLeftClick2()
    {
        if (rotateOn)
            StartCoroutine(rotateBoardLeft());
    }

    IEnumerator rotateBoardLeft()
    {
        PauseGame();

        int degrees = 1;
        for (int i = 0; i < 30; i += degrees)
        {
            Board.transform.RotateAround(new Vector3(10.0f, 0f, 18.75f), Vector3.up, degrees);

            yield return new WaitForSeconds(.05f);
        }

        PlayGame();
    }

    public static void DisableRotation()
    {
        rotateOn = false;
    }

    public static void EnableRotation()
    {
        rotateOn = true;
    }
}
