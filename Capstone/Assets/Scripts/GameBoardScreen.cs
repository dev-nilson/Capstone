using UnityEngine.SceneManagement;
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

    // add text for tutorial popup depending on the game phase
    public GameObject placeText;
    public GameObject moveText;
    public GameObject buildText;

    //Normal icons in the bottom of the screen
    public GameObject p1ScribeIcon;
    public GameObject p1WorkerIcon;
    public GameObject p1PeasantIcon;
    public GameObject p1PharoahIcon;

    public GameObject p2ScribeIcon;
    public GameObject p2WorkerIcon;
    public GameObject p2PeasantIcon;
    public GameObject p2PharoahIcon;

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

        Button okBtn = ok.GetComponent<Button>();
        okBtn.onClick.AddListener(okClicked);

        Button cancelBtn = cancel.GetComponent<Button>();
        cancelBtn.onClick.AddListener(cancelClicked);

        Button tutorialBtn = tutorial.GetComponent<Button>();
        tutorialBtn.onClick.AddListener(tutorialClicked);

        Button exitTutorialBtn = exitTutorial.GetComponent<Button>();
        exitTutorialBtn.onClick.AddListener(exitTutorialClicked);
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
            HelpTimer.TurnOff();
        }
    }

    void okClicked()
    {
        NetworkController.playerIntentionallyLeftRoom = true;
        NetworkController.SendPlayerLeft();
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
        HelpTimer.Set();
        confirmExitPopUp.SetActive(false);
    }

    void tutorialClicked()
    {
        if (!disabled)
        {
            FindObjectOfType<AudioManager>().Play("goldButtonPress");
            tutorialPopup.SetActive(true);
            if (CanPlacePawn()) placeText.SetActive(true);
            else if (CanMove()) moveText.SetActive(true);
            else if (CanBuild()) buildText.SetActive(true);

            PauseGame();
            DisableButtons();
            HelpTimer.TurnOff();
        }
    }

    void exitTutorialClicked()
    {
        placeText.SetActive(false);
        moveText.SetActive(false);
        buildText.SetActive(false);
        tutorialPopup.SetActive(false);
        PlayGame();
        EnableButtons();
        HelpTimer.Set();
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