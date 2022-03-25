using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class GameOverGraphics : MonoBehaviour
{
    //Network
    public GameObject disconnectedPopup;
    public GameObject youDisconnected;
    public GameObject opponectDisconnected;
    public GameObject backToMenu;

    //Normal popups
    public GameObject winPopup;
    public GameObject losePopup;


    // Start is called before the first frame update
    void Start()
    {
        Button backToMenuBtn = backToMenu.GetComponent<Button>();
        backToMenuBtn.onClick.AddListener(backToMenuClicked);
    }

    void Awake()
    {
        resetPopupBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver())
            GameOverPopup();
    }

    public void GameOverPopup()
    {
        // Network disconnect
        if (IsLocalDisconnect())
        {
            // MAGGIE: some pop up that says you are disconnected with button to return to main menu
            //then clear the game when you hit "okay" or whatever!
            disconnectedPopup.SetActive(true);
            youDisconnected.SetActive(true);
        }

        else if (IsOpponentDisconnect())
        {
            // MAGGIE: tell player that their opponent disconnected -- button to return to main menu
            //then clear the game when you hit "okay" or whatever!
            disconnectedPopup.SetActive(true);
            opponectDisconnected.SetActive(true);
        }

        //WHAT IF SOMEONE LEAVES THE GAME????????????

        // Local player wins in story mode
        else if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE) //CurrentPlayer.Type() == Player.Tag.LOCAL)
        {
            // Local player wins in story mode!
            // Change screens ??
        }

        // Local player loses in story mode
        else if (PlayingStoryMode)
        {
            // Local player loses in story mode :(
            // Change screens ??
        }

        // Local player wins in other game type
        else if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            winPopup.SetActive(true);
            Debug.Log("Local player loses: no available moves");
        }

        // Local player loses in other game type
        else
        {
            Debug.Log("Opposing player loses: no available moves"); 
            losePopup.SetActive(true);
        }
    }

    void resetPopupBoxes()
    {
        losePopup.SetActive(false);
        disconnectedPopup.SetActive(false);
        opponectDisconnected.SetActive(false);
        youDisconnected.SetActive(false);
        winPopup.SetActive(false);
    }

    void backToMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
