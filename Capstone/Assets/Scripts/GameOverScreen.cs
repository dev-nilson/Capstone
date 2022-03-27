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

    public Button backToMenu;
    public Button rematch;

    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        GameOverPopup();

        Button backToMenuBtn = backToMenu.GetComponent<Button>();
        backToMenuBtn.onClick.AddListener(backToMenuClicked);

        Button rematchBtn = rematch.GetComponent<Button>();
        rematchBtn.onClick.AddListener(rematchClicked);
    }

    public void GameOverPopup()
    {
        // Local player wins in story mode
        if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE)
        {
            // Local player wins in story mode!
            winText.SetActive(true);
        }

        // Local player loses in story mode
        else if (PlayingStoryMode)
        {
            // Local player loses in story mode :(
            loseText.SetActive(true);
        }

        // Local player wins in other game type
        else if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            //Debug.Log("Local player loses: no available moves");
            winText.SetActive(true);
        }

        // Local player loses in other game type
        else
        {
            //Debug.Log("Opposing player loses: no available moves"); 
            loseText.SetActive(true);
        }
    }

    void backToMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    void rematchClicked()
    {
        //Laura Grace!! this needs to play a game with all the saved info from the previous game
        SceneManager.LoadScene("Menu");
    }

}
