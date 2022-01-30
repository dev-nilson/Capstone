using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMainCamera : MonoBehaviour
{
	GameObject gridManager;
	GridManager Grid_M;

	public Button rotateRight;
	public Button rotateLeft;

	//lol
	void Start()
	{
		Button right = rotateRight.GetComponent<Button>();
		right.onClick.AddListener(rotateRightClick);

		Button left = rotateLeft.GetComponent<Button>();
		left.onClick.AddListener(rotateLeftClick);

		gridManager = GameObject.Find("GridManager");
		Grid_M = gridManager.GetComponent<GridManager>();
	}

	void rotateRightClick()
	{
		Debug.Log("You have clicked the right button!");
		//Grid_M.transform.position = new Vector3(100f, 0f, 0f);
		Grid_M.transform.rotation = Quaternion.Euler(90, 90, 0);

	}

	void rotateLeftClick()
	{
		Debug.Log("You have clicked the left button!");
	}
}
