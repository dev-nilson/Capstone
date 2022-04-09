using System.Collections;
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
    public Button rematch_WM;
    public Button rematch_LM;

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

    public GameObject scribePrefab;
    public GameObject workerPrefab;
    public GameObject pharoahPrefab;
    public GameObject peasantPrefab;

    //This is used to determine whether it is time to to dispay the final screen saying you beat storymode
    int count = 0;

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

        GameOverPopup();
        displayAlien();

        //Multiplayer buttons
        //Back to Menu
        Button backToMenuBtn_WM = backToMenu_WM.GetComponent<Button>();
        backToMenuBtn_WM.onClick.AddListener(backToMenuClicked);
        Button backToMenuBtn_LM = backToMenu_LM.GetComponent<Button>();
        backToMenuBtn_LM.onClick.AddListener(backToMenuClicked);
        //Rematch
        Button rematchBtn_WM = rematch_WM.GetComponent<Button>();
        rematchBtn_WM.onClick.AddListener(rematchClicked);
        Button rematchBtn_LM = rematch_LM.GetComponent<Button>();
        rematchBtn_LM.onClick.AddListener(rematchClicked);

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

    public void GameOverPopup()
    {
        // Local player wins in story mode
        if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE)
        {
            if(beatStoryMode)
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
                winQuickGameScreen.SetActive(true);
        }

        // Local player loses in other game type
        else
        {
            //Debug.Log("Opposing player loses: no available moves");
            if (getGameType() == GameType.NETWORK)
                loseMultiScreen.SetActive(true);
            else
                loseQuickGameScreen.SetActive(true);
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
        FindObjectOfType<AudioManager>().StopCurrentSong(1);
        beatStoryMode = false;
    }

    void rematchClicked()
    {
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

        beatStoryMode = true;
    }

}
