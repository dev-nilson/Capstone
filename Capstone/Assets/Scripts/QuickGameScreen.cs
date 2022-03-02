using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class QuickGameScreen : MonoBehaviour
{
    public Button startGame;
    public Button pharoah;
    public GameObject pharoah_H;
    public GameObject pharoah_figure;


    // Start is called before the first frame update
    void Start()
    {
        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

        Button pharoahBtn = pharoah.GetComponent<Button>();
        pharoahBtn.onClick.AddListener(pharoahClicked);
    }

    void startGameClicked()
    {
        Debug.Log("here");

        ////INITIALIZE GAME: AI HARD, AI EASY, NETWORK?
        //setGameType(GameType.EASY);
        //// OR setGameType(GameType.DIFFICULT);

        ////  STARTING PLAYER?
        //SetPlayerTurn(PlayerTurn.ONE);

        //// GUI: GET A USERNAME FROM USER
        //P1username = "Player one";

        //// GET USERNAME FROM OPPONENT
        //P2username = "CPU";


        SceneManager.LoadScene("GameBoard");
    }

    void pharoahClicked()
    {
        pharoah_H.SetActive(true);
        pharoah_figure.SetActive(true);

    }
}
