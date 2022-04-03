using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class GameOverScreen : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;

    //Quick game buttons
    public Button backToMenu;
    public Button rematch;

    //Story mode buttons
    public Button retry;
    public Button leaveStoryMode;
    public Button menuButton;

    public GameObject quickGameScreen;
    public GameObject multiplayerScreen;
    public GameObject storyModeScreen;
    public GameObject beatStoryModeScreen;


    public GameObject scribePrefab;
    public GameObject workerPrefab;
    public GameObject pharoahPrefab;
    public GameObject peasantPrefab;

    //This is used to determine whether it is time to to dispay the final screen saying you beat storymode
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);

        quickGameScreen.SetActive(false);
        multiplayerScreen.SetActive(false);
        storyModeScreen.SetActive(false);
        beatStoryModeScreen.SetActive(false);

        scribePrefab.SetActive(false);
        workerPrefab.SetActive(false);
        pharoahPrefab.SetActive(false);
        peasantPrefab.SetActive(false);

        GameOverPopup();
        displayAlien();

        Button backToMenuBtn = backToMenu.GetComponent<Button>();
        backToMenuBtn.onClick.AddListener(backToMenuClicked);

        Button rematchBtn = rematch.GetComponent<Button>();
        rematchBtn.onClick.AddListener(rematchClicked);

        Button retryBtn = retry.GetComponent<Button>();
        retryBtn.onClick.AddListener(rematchClicked);

        Button leaveStoryModeBtn = leaveStoryMode.GetComponent<Button>();
        leaveStoryModeBtn.onClick.AddListener(backToMenuClicked);

        Button menuBtn = menuButton.GetComponent<Button>();
        menuBtn.onClick.AddListener(backToMenuClicked);
    }

    public void GameOverPopup()
    {
        // Local player wins in story mode
        if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE)
        {
            if(count == 1)
            {
                beatStoryModeScreen.SetActive(true);
                count = 0;
            }
            else
            {
                storyModeScreen.SetActive(true);
                // Local player wins in story mode!
                winText.SetActive(true);
                //increment this so you know you have been here once
                count++;
            }
        }

        // Local player loses in story mode
        else if (PlayingStoryMode)
        {
            storyModeScreen.SetActive(true);
            // Local player loses in story mode :(
            loseText.SetActive(true);
        }

        // Local player wins in other game type
        else if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            quickGameScreen.SetActive(true);
            //Debug.Log("Local player loses: no available moves");
            winText.SetActive(true);
        }

        // Local player loses in other game type
        else
        {
            quickGameScreen.SetActive(true);
            //Debug.Log("Opposing player loses: no available moves"); 
            loseText.SetActive(true);
        }
    }

    void displayAlien()
    {
        if (getP1avatar() == PlayerAvatar.PEASANT) peasantPrefab.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.PHAROAH)pharoahPrefab.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.SCRIBE)scribePrefab.SetActive(true);
        else if (getP1avatar() == PlayerAvatar.WORKER)workerPrefab.SetActive(true);
    }

    void backToMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    void rematchClicked()
    {
        //Laura Grace!! this needs to play a game with all the saved info from the previous game
        ResetStartingPlayer();
        SceneManager.LoadScene("GameBoard");
    }

}
