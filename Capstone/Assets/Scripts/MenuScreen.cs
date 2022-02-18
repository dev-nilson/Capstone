using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
	public Button quickGame;
	public Button multiplayer;
	public Button tutorial;
	public Button storyMode;
	public Button exitApp;


	void Start()
	{
		Button quickGameBtn = quickGame.GetComponent<Button>();
		quickGameBtn.onClick.AddListener(quickGameClicked);

		Button multiplayerBtn = multiplayer.GetComponent<Button>();
		multiplayerBtn.onClick.AddListener(multiplayerClicked);

		Button tutorialBtn = tutorial.GetComponent<Button>();
		tutorialBtn.onClick.AddListener(tutorialClicked);

		Button storyModeBtn = storyMode.GetComponent<Button>();
		storyModeBtn.onClick.AddListener(storyModeClicked);

		Button exitAppBtn = exitApp.GetComponent<Button>();
		exitAppBtn.onClick.AddListener(exitAppClicked);
	}

	void quickGameClicked()
	{
		Debug.Log("quick game");

		SceneManager.LoadScene("GameBoard");
	}

	void multiplayerClicked()
	{
		Debug.Log("multiplayer game");

		SceneManager.LoadScene("Multiplayer");
	}

	void tutorialClicked()
	{
		Debug.Log("tutorial");

		SceneManager.LoadScene("Tutorial");
	}

	void storyModeClicked()
	{
		Debug.Log("story mode game");

		SceneManager.LoadScene("StoryMode");
	}

	void exitAppClicked()
	{
		Debug.Log("story mode game");

		Application.Quit();
	}
}
