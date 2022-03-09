using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class MenuScreen : MonoBehaviour
{
	public Button quickGame;
	public Button multiplayer;
	public Button tutorial;
	public Button storyMode;
	public Button exitApp;
	public Button exitSettings;
	public Button settings;
	//public GameObject settingsPopUp;

	LevelChanger levelChanger;

	void Start()
	{
		//settingsPopUp.SetActive(false); // false to hide, true to show

		Button quickGameBtn = quickGame.GetComponent<Button>();
		quickGameBtn.onClick.AddListener(quickGameClicked);

		Button multiplayerBtn = multiplayer.GetComponent<Button>();
		multiplayerBtn.onClick.AddListener(multiplayerClicked);

		Button tutorialBtn = tutorial.GetComponent<Button>();
		tutorialBtn.onClick.AddListener(tutorialClicked);

		Button storyModeBtn = storyMode.GetComponent<Button>();
		storyModeBtn.onClick.AddListener(storyModeClicked);

		//Button exitAppBtn = exitApp.GetComponent<Button>();
		//exitAppBtn.onClick.AddListener(exitAppClicked);

		//Button settingsBtn = settings.GetComponent<Button>();
		//settingsBtn.onClick.AddListener(settingsClicked);

		//Button exitSettingsBtn = exitSettings.GetComponent<Button>();
		//exitSettingsBtn.onClick.AddListener(exitSettingsClicked);
	}

	void quickGameClicked()
	{
		Debug.Log("quick game");

		//levelChanger.FadeToLevel("QuickGame");

		SceneManager.LoadScene("QuickGame");
	}

	void multiplayerClicked()
	{
		Debug.Log("multiplayer game");
		setGameType(GameType.NETWORK);

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

	void settingsClicked()
	{
		Debug.Log("settings game");
		//settingsPopUp.SetActive(true); // false to hide, true to show
	}

	void exitSettingsClicked()
	{
		Debug.Log("exit");
		//settingsPopUp.SetActive(false); // false to hide, true to show
	}
}
