﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using static GameUtilities;
using UnityEngine.Events;
using System;

public class GameBoardScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;
    public GameObject confirmExitPopUp;
    public Button ok;
    public Button cancel;

    //For the tutorial popup
    public Button tutorial;
    public Button exitTutorial;
    public GameObject tutorialPopup;

    //This is for the setting scroll (makes it look like it's rolling up)
    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;
    public GameObject scroll6;

    //Normal icons in the bottom of the screen
    public GameObject p1ScribeIcon;
    public GameObject p1WorkerIcon;
    public GameObject p1PeasantIcon;
    public GameObject p1PharoahIcon;

    public GameObject p2ScribeIcon;
    public GameObject p2WorkerIcon;
    public GameObject p2PeasantIcon;
    public GameObject p2PharoahIcon;

    //Highlighted icons in the bottom of the screen
    public GameObject p1ScribeIcon_H;
    public GameObject p1WorkerIcon_H;
    public GameObject p1PeasantIcon_H;
    public GameObject p1PharoahIcon_H;

    public GameObject p2ScribeIcon_H;
    public GameObject p2WorkerIcon_H;
    public GameObject p2PeasantIcon_H;
    public GameObject p2PharoahIcon_H;

    public Text p1Username;
    public Text p2Username;

    public PlayerAvatar p1Alien;
    public PlayerAvatar p2Alien;

    private static bool disabled;

    void Start()
    {
        p1Username.text = getP1username();
        p2Username.text = getP2username();

        p1Alien = getP1avatar();
        p2Alien = getP2avatar();


        if (getP1avatar() == PlayerAvatar.PEASANT) p1PeasantIcon.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.PHAROAH) p1PharoahIcon.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.SCRIBE) p1ScribeIcon.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.WORKER) p1WorkerIcon.SetActive(true);

        if (getP2avatar() == PlayerAvatar.PEASANT) p2PeasantIcon.SetActive(true);
        else if (getP2avatar() == PlayerAvatar.PHAROAH) p2PharoahIcon.SetActive(true);
        else if (getP2avatar() == PlayerAvatar.SCRIBE) p2ScribeIcon.SetActive(true);
        else if (getP2avatar() == PlayerAvatar.WORKER) p2WorkerIcon.SetActive(true);


        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(delayDisplay);

        Button okBtn = ok.GetComponent<Button>();
        okBtn.onClick.AddListener(okClicked);

        Button cancelBtn = cancel.GetComponent<Button>();
        cancelBtn.onClick.AddListener(cancelClicked);

        Button tutorialBtn = tutorial.GetComponent<Button>();
        tutorialBtn.onClick.AddListener(tutorialClicked);

        Button exitTutorialBtn = exitTutorial.GetComponent<Button>();
        exitTutorialBtn.onClick.AddListener(exitTutorialClicked);
    }

    void Update()
    {
        if (GetPlayerTurn() == PlayerTurn.ONE && (CanPlacePawn() || CanMove()))
        {
            //Turn of player 2 highlighted icon
            if (getP2avatar() == PlayerAvatar.PEASANT) p2PeasantIcon_H.SetActive(false);
            else if (getP2avatar() == PlayerAvatar.PHAROAH) p2PharoahIcon_H.SetActive(false);
            else if (getP2avatar() == PlayerAvatar.SCRIBE) p2ScribeIcon_H.SetActive(false);
            else if (getP2avatar() == PlayerAvatar.WORKER) p2WorkerIcon_H.SetActive(false);

            //Turn on the highlighted icon 
            if (getP1avatar() == PlayerAvatar.PEASANT) p1PeasantIcon_H.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.PHAROAH) p1PharoahIcon_H.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.SCRIBE) p1ScribeIcon_H.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.WORKER) p1WorkerIcon_H.SetActive(true);
        }
        else if (GetPlayerTurn() == PlayerTurn.TWO && (CanPlacePawn() || CanMove()))
        {
            //Turn off player 1 highlighted icon
            if (getP1avatar() == PlayerAvatar.PEASANT) p1PeasantIcon_H.SetActive(false);
            else if (getP1avatar() == PlayerAvatar.PHAROAH) p1PharoahIcon_H.SetActive(false);
            else if (getP1avatar() == PlayerAvatar.SCRIBE) p1ScribeIcon_H.SetActive(false);
            else if (getP1avatar() == PlayerAvatar.WORKER) p1WorkerIcon_H.SetActive(false);

            //Turn on the highlighted icon 
            if (getP2avatar() == PlayerAvatar.PEASANT) p2PeasantIcon_H.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.PHAROAH) p2PharoahIcon_H.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.SCRIBE) p2ScribeIcon_H.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.WORKER) p2WorkerIcon_H.SetActive(true);
        }
    }

    void backClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (!disabled)
        {
            confirmExitPopUp.SetActive(true);
            //switch phases to turn off build and place player to create a fake modal pop up box
            RotateMainCamera.DisableRotation();
            PauseGame();
            DisableButtons();
        }
    }

    void okClicked()
    {
        ClearGame();
        EnableButtons();
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().StopCurrentSong(1);
    }

    void cancelClicked()
    {
        RotateMainCamera.EnableRotation();
        PlayGame();
        EnableButtons();
        confirmExitPopUp.SetActive(false);
    }

    void tutorialClicked()
    {
        if (!disabled)
        {
            FindObjectOfType<AudioManager>().Play("goldButtonPress");
            tutorialPopup.SetActive(true);
            PauseGame();
            DisableButtons();
        }
    }

    void exitTutorialClicked()
    {
        tutorialPopup.SetActive(false);
        PlayGame();
        EnableButtons();
    }

    void settingsClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (!disabled)
        {
            Debug.Log("settings game");
            scroll1.SetActive(true);

            //switch phases to turn off build and place player to create a fake modal pop up box
            RotateMainCamera.DisableRotation();
            PauseGame();
            DisableButtons();
        }
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
            for (float i = firstScrollRef; i >= (firstScrollRef - ((firstScrollRef - secondScrollRef))); i-=8)
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

        RotateMainCamera.EnableRotation();
        PlayGame();
        EnableButtons();
    }

    public static void DisableButtons()
    {
        disabled = true;
    }

    public static void EnableButtons()
    {
        disabled = false;
    }
}