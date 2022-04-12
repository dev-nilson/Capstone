﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class GameOverScreen : MonoBehaviour
{
    //Multiplayer
    public GameObject winMultiScreen;
    public GameObject loseMultiScreen;
    public Button backToMenu_WM;
    public Button backToMenu_LM;

    //Quick game
    public GameObject winQuickGameScreen;
    public GameObject loseQuickGameScreen;
    public Button backToMenu_WQ;
    public Button backToMenu_LQ;
    public Button rematch_WQ;
    public Button rematch_LQ;

    //Story mode
    public GameObject winStoryModeScreen;
    public GameObject loseStoryModeScreen;
    public GameObject beatStoryModeScreen;
    public Button backToMenu_WS;
    public Button backToMenu_LS;
    public Button continue_WS;
    public Button rematch_LS;
    public Button backToMenu_BS;

    public static bool readyForStoryModeSetThree = false;
    public static bool beatStoryMode = false;

    //Aliens
    public GameObject scribePrefab;
    public GameObject workerPrefab;
    public GameObject pharoahPrefab;
    public GameObject peasantPrefab;

    //Ships
    public GameObject scribeShipPrefab;
    public GameObject workerShipPrefab;
    public GameObject pharoahShipPrefab;
    public GameObject peasantShipPrefab;

    static bool readyToDisplayText = false;

    // Start is called before the first frame update
    void Start()
    {
        winStoryModeScreen.SetActive(false);
        loseStoryModeScreen.SetActive(false);
        beatStoryModeScreen.SetActive(false);

        winQuickGameScreen.SetActive(false);
        loseQuickGameScreen.SetActive(false);

        winMultiScreen.SetActive(false);
        loseMultiScreen.SetActive(false);

        scribePrefab.SetActive(false);
        workerPrefab.SetActive(false);
        pharoahPrefab.SetActive(false);
        peasantPrefab.SetActive(false);

        scribeShipPrefab.SetActive(false);
        workerShipPrefab.SetActive(false);
        pharoahShipPrefab.SetActive(false);
        peasantShipPrefab.SetActive(false);

        displayAlien();
        displayShip();

        //Multiplayer buttons
        //Back to Menu
        Button backToMenuBtn_WM = backToMenu_WM.GetComponent<Button>();
        backToMenuBtn_WM.onClick.AddListener(backToMenuClicked);
        Button backToMenuBtn_LM = backToMenu_LM.GetComponent<Button>();
        backToMenuBtn_LM.onClick.AddListener(backToMenuClicked);

        //Quick Game buttons
        //Back to Menu
        Button backToMenuBtn_WQ = backToMenu_WQ.GetComponent<Button>();
        backToMenuBtn_WQ.onClick.AddListener(backToMenuClicked);
        Button backToMenuBtn_LQ = backToMenu_LQ.GetComponent<Button>();
        backToMenuBtn_LQ.onClick.AddListener(backToMenuClicked);
        //Rematch
        Button rematchBtn_WQ = rematch_WQ.GetComponent<Button>();
        rematchBtn_WQ.onClick.AddListener(rematchClicked);
        Button rematchBtn_LQ = rematch_LQ.GetComponent<Button>();
        rematchBtn_LQ.onClick.AddListener(rematchClicked);

        //Story mode buttons
        //Back to Menu
        Button backToMenuBtn_WS = backToMenu_WS.GetComponent<Button>();
        backToMenuBtn_WS.onClick.AddListener(backToMenuClicked);
        Button backToMenuBtn_LS = backToMenu_LS.GetComponent<Button>();
        backToMenuBtn_LS.onClick.AddListener(backToMenuClicked);
        Button backToMenuBtn_BS = backToMenu_BS.GetComponent<Button>();
        backToMenuBtn_BS.onClick.AddListener(backToMenuClicked);
        //Retry
        Button rematchBtn_LS = rematch_LS.GetComponent<Button>();
        rematchBtn_LS.onClick.AddListener(rematchClicked);
        //Continue
        Button continueBtn_WS = continue_WS.GetComponent<Button>();
        continueBtn_WS.onClick.AddListener(continueStoryClicked);

    }

    void Update()
    {
        waitForShipToLeave();
        if (readyToDisplayText)
        {
            GameOverPopup();
        }
    }

    public void GameOverPopup()
    {
        // Local player wins in story mode
        if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE)
        {
            waitForShipToLeave();
            if (beatStoryMode)
            {
                beatStoryModeScreen.SetActive(true);
            }
            else
            {
                // Local player wins in story mode!
                winStoryModeScreen.SetActive(true);
            }
        }

        // Local player loses in story mode
        else if (PlayingStoryMode)
        {
            // Local player loses in story mode :(
            loseStoryModeScreen.SetActive(true);
        }

        // Local player wins in other game type
        else if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            //Debug.Log("Local player loses: no available moves");
            if (getGameType() == GameType.NETWORK)
                winMultiScreen.SetActive(true);
            else
            {
                winQuickGameScreen.SetActive(true);
            }
        }

        // Local player loses in other game type
        else
        {
            //Debug.Log("Opposing player loses: no available moves");
            if (getGameType() == GameType.NETWORK)
                loseMultiScreen.SetActive(true);
            else
            {
                loseQuickGameScreen.SetActive(true);
            }
        }
    }

    void displayAlien()
    {
        //If player 1 won then display the other alien
        if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            if (getP2avatar() == PlayerAvatar.PEASANT) peasantPrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.PHAROAH) pharoahPrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.SCRIBE) scribePrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.WORKER) workerPrefab.SetActive(true);
        }

        else
        {
            if (getP1avatar() == PlayerAvatar.PEASANT) peasantPrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.PHAROAH) pharoahPrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.SCRIBE) scribePrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.WORKER) workerPrefab.SetActive(true);
        }
    }

    void displayShip()
    {
        //If player 1 won then display their ship
        if(GetWinningPlayer() == PlayerTurn.ONE)
        {
            if (getP1avatar() == PlayerAvatar.PEASANT) peasantShipPrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.PHAROAH) pharoahShipPrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.SCRIBE) scribeShipPrefab.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.WORKER) workerShipPrefab.SetActive(true);
        }
        //else display the opponents ship
        else
        {
            if (getP2avatar() == PlayerAvatar.PEASANT) peasantShipPrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.PHAROAH) pharoahShipPrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.SCRIBE) scribeShipPrefab.SetActive(true);
            else if (getP2avatar() == PlayerAvatar.WORKER) workerShipPrefab.SetActive(true);
        }
    }

    void backToMenuClicked()
    {
        Scroll.EnableButtons();
        GameBoardScreen.EnableButtons();
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().StopCurrentSong(1);
        beatStoryMode = false;
    }

    void rematchClicked()
    {
        Scroll.EnableButtons();
        GameBoardScreen.EnableButtons();
        ResetStartingPlayer();
        SceneManager.LoadScene("GameBoard");
        FindObjectOfType<AudioManager>().StopCurrentSong(6);
    }

    void continueStoryClicked()
    {
        Debug.Log("Continue clicked");
        readyForStoryModeSetThree = true;
        SceneManager.LoadScene("StoryMode");
        FindObjectOfType<AudioManager>().StopCurrentSong(3);

        //beatStoryMode = true;
    }

    void waitForShipToLeave()
    {
        StartCoroutine("shipDelay");
    }
    IEnumerator shipDelay()
    {
        yield return new WaitForSeconds(2f);
        readyToDisplayText = true;
    }
}
