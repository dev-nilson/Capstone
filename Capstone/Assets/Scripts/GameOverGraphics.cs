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
    public GameObject opponentLeft;
    public GameObject backToMenu;

    ScreenShake screenShake = new ScreenShake();

    bool canShake = false;


    // Start is called before the first frame update
    void Start()
    {
        Button backToMenuBtn = backToMenu.GetComponent<Button>();
        backToMenuBtn.onClick.AddListener(backToMenuClicked);
    }

    // Update is called once per frame
    void Update()
    {
        // Network disconnect
        if (getGameType() == GameType.NETWORK && IsLocalDisconnect())
        {
            RotateMainCamera.DisableRotation();
            PauseGame();
            GameBoardScreen.DisableButtons();
            Scroll.DisableButtons();
            HelpTimer.TurnOff();

            disconnectedPopup.SetActive(true);
            youDisconnected.SetActive(true);
        }
        else if (getGameType() == GameType.NETWORK && IsOpponentDisconnect())
        {
            RotateMainCamera.DisableRotation();
            PauseGame();
            GameBoardScreen.DisableButtons();
            Scroll.DisableButtons();
            HelpTimer.TurnOff();

            disconnectedPopup.SetActive(true);
            opponectDisconnected.SetActive(true);
        }
        else if (getGameType() == GameType.NETWORK && IsOpponentLeft())
        {
            RotateMainCamera.DisableRotation();
            PauseGame();
            GameBoardScreen.DisableButtons();
            Scroll.DisableButtons();
            HelpTimer.TurnOff();

            opponentLeft.SetActive(true);
        }
        else if (IsGameOver() && canShake == false)
        {
            RotateMainCamera.DisableRotation();
            PauseGame();
            GameBoardScreen.DisableButtons();
            Scroll.DisableButtons();
            HelpTimer.TurnOff();

            canShake = true;
            //GameOverPopup();
            shakeTheScreen();
            //SceneManager.LoadScene("GameOver");
        }
    }

    public void resetPopupBoxes()
    {
        disconnectedPopup.SetActive(false);
        opponectDisconnected.SetActive(false);
        youDisconnected.SetActive(false);
        opponentLeft.SetActive(false);

        ClearGame();
        GameBoardScreen.EnableButtons();
        Scroll.EnableButtons();
    }

    void backToMenuClicked()
    {
        resetPopupBoxes();
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().StopCurrentSong(1);
    }

    void shakeTheScreen()
    {
        StartCoroutine(shakeScreen());
    }

    IEnumerator shakeScreen()
    {
        FindObjectOfType<AudioManager>().StopCurrentSong(7);
        yield return new WaitForSeconds(1f);
        ScreenShake.instance.StartShake(.5f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(.5f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(.5f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(.5f, 1f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);
        ScreenShake.instance.StartShake(1f, 2f);
        Debug.Log("Wait");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");
    }
}
