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
        else if (getGameType() == GameType.NETWORK && IsOpponentLeft())
        {
            opponentLeft.SetActive(true);
        }
        else if (IsGameOver() && canShake == false)
        {
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
