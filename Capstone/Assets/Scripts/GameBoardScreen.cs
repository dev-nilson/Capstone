using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Threading;
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

    public GameObject firstQ;
    public GameObject secondQ;
    public GameObject thirdQ;

    //For the tutorial popup
    public Button tutorial;
    public Button exitTutorial;
    public GameObject tutorialPopup;
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

    HelpArrow GameHelp;

    private static bool disabled;
    private static bool Qsready = true;

    //Thread ChildThread = null;

    void Start()
    {
        p1Username.text = getP1username();
        p2Username.text = getP2username();

        p1Alien = getP1avatar();
        p2Alien = getP2avatar();

        //ChildThread = new Thread(StartAIQs);

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

        GameHelp = gameObject.GetComponent<HelpArrow>();
    }

    void Update()
    {
        if (Qsready && getGameType() == GameType.DIFFICULT && GetPlayerTurn() == PlayerTurn.TWO && CanMove())
            StartAIQs();
        if (!Qsready && getGameType() == GameType.DIFFICULT && GetPlayerTurn() == PlayerTurn.TWO && CanBuild())
            EndAIQs();
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
            Scroll.DisableButtons();
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
            PauseGame();
            DisableButtons();
            Scroll.DisableButtons();
            HelpTimer.TurnOff();

            tutorialPopup.SetActive(true);
            FindObjectOfType<AudioManager>().Play("goldButtonPress");
            GameHelp.TutorialStart();

            if (CanPlacePawn()) placeText.SetActive(true);
            if (CanMove()) moveText.SetActive(true);
            if (CanBuild()) buildText.SetActive(true);
        }
    }

    void exitTutorialClicked()
    {
        //GameHelp.TutorialEnd();
        tutorialPopup.SetActive(false);
        PlayGame();
        EnableButtons();
        Scroll.EnableButtons();
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

    public void StartAIQs()
    {
        Debug.Log("hereeeeeeeeeee");
        StartCoroutine("TurnOnQs");
    }

    IEnumerator TurnOnQs()
    {
        Qsready = false;
        while (true)
        {
            firstQ.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            firstQ.SetActive(false);
            secondQ.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            secondQ.SetActive(false);
            thirdQ.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            thirdQ.SetActive(false);
        }
    }

    public void EndAIQs()
    {
        firstQ.SetActive(false);
        secondQ.SetActive(false);
        thirdQ.SetActive(false);
        StopCoroutine("TurnOnQs");
        Qsready = true;
    }
}