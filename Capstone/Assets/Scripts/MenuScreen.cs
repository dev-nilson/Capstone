using System.Collections;
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

	//Help Menu Items
	public Button quickGameHelp;
	public Button multiplayerHelp;
	public Button tutorialHelp;
	public Button storyModeHelp;
	public Button artBookHelp;

	//Upper lefthand buttons
	public Button exitApp;
	public Button exitSettings;
	public Button settings;

	//These are for the setting scroll (to make it fold)
	public GameObject scroll1;
	public GameObject scroll2;
	public GameObject scroll3;
	public GameObject scroll4;
	public GameObject scroll5;
	public GameObject scroll6;

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

		Button exitSettingsBtn = exitSettings.GetComponent<Button>();
		exitSettingsBtn.onClick.AddListener(delayDisplay);

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
		SceneManager.LoadScene("ArtBook");

		FindObjectOfType<AudioManager>().StopCurrentSong(5);
		FindObjectOfType<AudioManager>().Play("stoneButtonPress");
	}

	void exitAppClicked()
	{
		Application.Quit();
	}

	void settingsClicked()
	{
		scroll1.SetActive(true);

		//switch phases to turn off build and place player to create a fake modal pop up box
		PauseGame();
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

		PlayGame();
	}
}
