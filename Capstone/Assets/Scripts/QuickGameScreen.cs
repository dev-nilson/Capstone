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

    public Button scribe;
    public GameObject scribe_H;
    public GameObject scribe_figure;

    public Button peasant;
    public GameObject peasant_H;
    public GameObject peasant_figure;

    public Button worker;
    public GameObject worker_H;
    public GameObject worker_figure;

    public InputField usernameInputField;

    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log(input.text);
            
        }
        else if (input.text.Length == 0)
        {
            input.text = "Player 1";
            Debug.Log(input.text);
        }

        //SAVE USERNAME HERE!!!!!!!!!
    }

    // Start is called before the first frame update
    void Start()
    {
        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

        Button pharoahBtn = pharoah.GetComponent<Button>();
        pharoahBtn.onClick.AddListener(pharoahClicked);

        Button scribeBtn = scribe.GetComponent<Button>();
        scribeBtn.onClick.AddListener(scribeClicked);

        Button peasantBtn = peasant.GetComponent<Button>();
        peasantBtn.onClick.AddListener(peasantClicked);

        Button workerBtn = worker.GetComponent<Button>();
        workerBtn.onClick.AddListener(workerClicked);

        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        usernameInputField.onEndEdit.AddListener(delegate { LockInput(usernameInputField); });
    }

    void startGameClicked()
    {
        Debug.Log("here");

        //  INITIALIZE GAME: AI HARD, AI EASY, NETWORK?
        //setGameType(GameType.EASY);

        // STARTING PLAYER?
        //SetPlayerTurn(PlayerTurn.ONE);

        //// GUI: GET A USERNAME FROM USER
        //P1username = "Player one";

        //// GET USERNAME FROM OPPONENT
        //P2username = "CPU";


        SceneManager.LoadScene("GameBoard");
    }

    void resetAll()
    {
        pharoah_H.SetActive(false);
        pharoah_figure.SetActive(false);

        scribe_H.SetActive(false);
        scribe_figure.SetActive(false);

        peasant_H.SetActive(false);
        peasant_figure.SetActive(false);

        worker_H.SetActive(false);
        worker_figure.SetActive(false);
    }

    void pharoahClicked()
    {
        resetAll();
        pharoah_H.SetActive(true);
        pharoah_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
    }

    void scribeClicked()
    {
        resetAll();
        scribe_H.SetActive(true);
        scribe_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
    }

    void peasantClicked()
    {
        resetAll();
        peasant_H.SetActive(true);
        peasant_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
    }

    void workerClicked()
    {
        resetAll();
        worker_H.SetActive(true);
        worker_figure.SetActive(true);

        //save the alien name LAURA GRACE lol
    }

}
