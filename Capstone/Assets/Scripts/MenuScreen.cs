﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class MenuScreen : MonoBehaviour
{
	//Menu Items
	public Button quickGame;
	public Button multiplayer;
	public Button tutorial;
	public Button storyMode;
	public Button artBook;

	//Upper lefthand buttons
	public Button exitApp;
	public Button exitSettings;
	public Button settings;
	public Button help;

	public Button exitHelp;

	//These are for the setting scroll (to make it fold)
	public GameObject scroll1;
	public GameObject scroll2;
	public GameObject scroll3;
	public GameObject scroll4;
	public GameObject scroll5;
	public GameObject scroll6;

	public GameObject helpPanel;

	public GameObject clearPanel;

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

		Button artBookBtn = artBook.GetComponent<Button>();
		artBookBtn.onClick.AddListener(artBookClicked);

		Button exitAppBtn = exitApp.GetComponent<Button>();
		exitAppBtn.onClick.AddListener(exitAppClicked);

		Button settingsBtn = settings.GetComponent<Button>();
		settingsBtn.onClick.AddListener(settingsClicked);

		Button helpBtn = help.GetComponent<Button>();
		helpBtn.onClick.AddListener(helpClicked);

		Button exitHelpBtn = exitHelp.GetComponent<Button>();
		exitHelpBtn.onClick.AddListener(exitHelpClicked);

		Button exitSettingsBtn = exitSettings.GetComponent<Button>();
		exitSettingsBtn.onClick.AddListener(delayDisplay);

		helpPanel.SetActive(false);

		PlayingStoryMode = false;
	}

	void quickGameClicked()
	{
		//levelChanger.FadeToLevel("QuickGame");

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
		Debug.Log("tutorial");

		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("Tutorial");
	}

	void storyModeClicked()
	{
		PlayingStoryMode = true;

		Debug.Log("story mode game");

		FindObjectOfType<AudioManager>().Play("stoneButtonPress");

		SceneManager.LoadScene("StoryMode");
	}

	void artBookClicked()
	{
		FindObjectOfType<AudioManager>().Play("stoneButtonPress");
		SceneManager.LoadScene("ArtBook");

		FindObjectOfType<AudioManager>().StopCurrentSong(5);
	}

	void exitAppClicked()
	{
		Application.Quit();
	}

	void settingsClicked()
	{
		clearPanel.SetActive(true);
		scroll1.SetActive(true);
	}

	void helpClicked()
	{
		helpPanel.SetActive(true);
	}

	void exitHelpClicked()
	{
		helpPanel.SetActive(false);
	}

	void delayDisplay()
	{
		StartCoroutine(exitSettingsClicked());
	}

	IEnumerator exitSettingsClicked()
	{
		//time that the scroll waits before it moves on to the next image
		float delay = .001f;

		GameObject[] scrollArray = new GameObject[7];

		//Get the Rectransform of each height in order to get the height 
		scrollArray[1] = scroll1;
		scrollArray[2] = scroll2;
		scrollArray[3] = scroll3;
		scrollArray[4] = scroll4;
		scrollArray[5] = scroll5;
		scrollArray[6] = scroll6;

		//Create a copy of the scrolls in order to reset the heights back to the original
		GameObject[] scrollReset = scrollArray;

		//BEAUTIFUL PIECE OF CODE --- Love Dad
		for (int scrollNum = 1; scrollNum <= 5; scrollNum++)
		{
			scrollArray[scrollNum].SetActive(true);
			float firstScrollRef = scrollArray[scrollNum].GetComponent<RectTransform>().rect.height;
			float tempHeight = firstScrollRef;
			float secondScrollRef = scrollArray[scrollNum + 1].GetComponent<RectTransform>().rect.height;
			for (float i = firstScrollRef; i >= (firstScrollRef - ((firstScrollRef - secondScrollRef))); i -= 8)
			{
				scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollArray[scrollNum].GetComponent<RectTransform>().rect.width, i);
				yield return new WaitForSecondsRealtime(delay);
			}
			scrollArray[scrollNum].SetActive(false);

			//add delay before is turns off
			if (scrollNum == 5)
			{
				scrollArray[6].SetActive(true);
				yield return new WaitForSeconds(.1f);
				scrollArray[6].SetActive(false);
			}
			//reset height value
			scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollReset[scrollNum].GetComponent<RectTransform>().rect.width, tempHeight);
		}
		clearPanel.SetActive(false);

		//Why is this here???
		PlayGame();
	}
}
