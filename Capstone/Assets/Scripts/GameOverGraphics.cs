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

    ScreenShake screenShake = new ScreenShake();

    bool canShake = false;


    // Start is called before the first frame update
    void Start()
    {
        resetPopupBoxes();
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
        if (IsGameOver() && canShake == false)
        {
            canShake = true;
            GameOverPopup();
            shakeTheScreen();
        }
    }

    public void GameOverPopup()
    {
        // Network disconnect
        if (getGameType() == GameType.NETWORK && IsLocalDisconnect())
        {
            disconnectedPopup.SetActive(true);
            youDisconnected.SetActive(true);
        }

        else if (getGameType() == GameType.NETWORK && IsOpponentDisconnect())
        {
            disconnectedPopup.SetActive(true);
            opponectDisconnected.SetActive(true);
        }

        // TO DO: WHAT IF SOMEONE LEAVES THE GAME????????????

        // Local player wins in story mode
        else if (PlayingStoryMode && GetWinningPlayer() == PlayerTurn.ONE)
        {
            // Local player wins in story mode!
            if (canShake)
                shakeTheScreen();
            SceneManager.LoadScene("GameOver");
        }

        // Local player loses in story mode
        else if (PlayingStoryMode)
        {
            if (canShake)
                shakeTheScreen();
            // Local player loses in story mode :(
            SceneManager.LoadScene("GameOver");
        }

        // Local player wins in other game type
        else if (GetWinningPlayer() == PlayerTurn.ONE)
        {
            //winPopup.SetActive(true);
            //Debug.Log("Local player loses: no available moves");
            shakeTheScreen();
            canShake = true;
            SceneManager.LoadScene("GameOver");

        }

        // Local player loses in other game type
        else
        {
            //Debug.Log("Opposing player loses: no available moves"); 
            //losePopup.SetActive(true);
            //shakeTheScreen();
            //SceneManager.LoadScene("GameOver");
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
        FindObjectOfType<AudioManager>().StopCurrentSong(1);
    }

    void shakeTheScreen()
    {
        //ScreenShake.instance.StartShake(5f, 5f);
        //ScreenShake.instance.StartShake(1f, 1f);
        //canShake = true;
        StartCoroutine(shakeScreen());
    }

    IEnumerator shakeScreen()
    {
        ScreenShake.instance.StartShake(1f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(1f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(1f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(1f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(1f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
    }
}
