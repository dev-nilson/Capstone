using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class MenuScreen : MonoBehaviour, IPointerDownHandler
{
	//Menu Items
	public Button quickGame;
	public Button multiplayer;
	public Button tutorial;
	public Button storyMode;
	public Button artBook;

	//Upper lefthand buttons
	public Button exitApp;

	public Button help;

	public Button exitHelp;
	public GameObject helpPanel;
	public GameObject clearPanel;

	LevelChanger levelChanger;

	float screenDelay = 5f;

	public GameObject teamIntro;
	public GameObject gameIntro;
	public GameObject introductionPanel;

	int count = 0;
	static bool firstTimeThrough = true;

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

		Button artBookBtn = artBook.GetComponent<Button>();
		artBookBtn.onClick.AddListener(artBookClicked);

		Button exitAppBtn = exitApp.GetComponent<Button>();
		exitAppBtn.onClick.AddListener(exitAppClicked);

		Button helpBtn = help.GetComponent<Button>();
		helpBtn.onClick.AddListener(helpClicked);

		Button exitHelpBtn = exitHelp.GetComponent<Button>();
		exitHelpBtn.onClick.AddListener(exitHelpClicked);

		helpPanel.SetActive(false);

		PlayingStoryMode = false;
		SetGameOver();

		GameBoardScreen.EnableButtons();
		Scroll.EnableButtons();

		if (firstTimeThrough)
		{
			teamIntro.SetActive(true);
			startTeamIntroVideo();
		}
	}
	void startTeamIntroVideo()
	{
		firstTimeThrough = false;
		StartCoroutine("startTeamVideo");
	}

	IEnumerator startTeamVideo()
	{
		yield return new WaitForSeconds(screenDelay);
		teamIntro.SetActive(false);
		startGameIntroVideo();
	}

	void startGameIntroVideo()
	{
		gameIntro.SetActive(true);
		StartCoroutine("startGameVideo");
	}

	IEnumerator startGameVideo()
	{
		yield return new WaitForSeconds(screenDelay);
		gameIntro.SetActive(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        Debug.Log(count);

        count++;
        if (count == 1)
        {
            Debug.Log("first click");
            StopCoroutine("startTeamVideo");
            startGameIntroVideo();
        }
        else
        {
            StopCoroutine("startGameVideo");
            introductionPanel.SetActive(false);
        }
    }

	void quickGameClicked()
	{
		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("QuickGame");
	}

	void multiplayerClicked()
	{
		setGameType(GameType.NETWORK);

		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("Multiplayer");
	}

	void tutorialClicked()
	{
		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("Tutorial");
	}

	void storyModeClicked()
	{
		PlayingStoryMode = true;

		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("StoryMode");

		FindObjectOfType<AudioManager>().StopCurrentSong(2);
	}

	void artBookClicked()
	{
		FindObjectOfType<AudioManager>().Play("stoneButtonPress");
		SceneManager.LoadScene("ArtBook");

		FindObjectOfType<AudioManager>().StopCurrentSong(5);
	}

	void exitAppClicked()
	{
		FindObjectOfType<AudioManager>().Play("goldButtonPress");
		Application.Quit();
	}

	void helpClicked()
	{
		FindObjectOfType<AudioManager>().Play("goldButtonPress");
		helpPanel.SetActive(true);
	}

    void exitHelpClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        helpPanel.SetActive(false);
    }
}
