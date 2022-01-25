using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMainCamera : MonoBehaviour
{
	public Button rotateRight;
	public Button rotateLeft;

	void Start()
	{
		Button right = rotateRight.GetComponent<Button>();
		right.onClick.AddListener(rotateRightClick);

		Button left = rotateLeft.GetComponent<Button>();
		left.onClick.AddListener(rotateLeftClick);
	}

	void rotateRightClick()
	{
		Debug.Log("You have clicked the right button!");
	}

	void rotateLeftClick()
	{
		Debug.Log("You have clicked the left button!");
	}
}
