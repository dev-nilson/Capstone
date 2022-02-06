using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScreens : MonoBehaviour
{
	public Button quickGame;

	void Start()
	{
		Button quickGameBtn = quickGame.GetComponent<Button>();
		quickGameBtn.onClick.AddListener(quickGameClicked);
	}

	void quickGameClicked()
	{
		Debug.Log("quick game");

		SceneManager.LoadScene("GameBoard");
	}
}
