using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class QuickGameScreen : MonoBehaviour
{
    public Button startGame;
    public Button back;

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

    public GameObject choosePlayer;

    public InputField usernameInputField;


    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            setP1username(input.text);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button pharoahBtn = pharoah.GetComponent<Button>();
        pharoahBtn.onClick.AddListener(pharoahClicked);

        Button scribeBtn = scribe.GetComponent<Button>();
        scribeBtn.onClick.AddListener(scribeClicked);

        Button peasantBtn = peasant.GetComponent<Button>();
        peasantBtn.onClick.AddListener(peasantClicked);

        Button workerBtn = worker.GetComponent<Button>();
        workerBtn.onClick.AddListener(workerClicked);

        usernameInputField.characterLimit = 17;

        setP1avatar(RandomPlayerAvatar());

        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        usernameInputField.onEndEdit.AddListener(delegate { LockInput(usernameInputField); });
        if (usernameInputField.text.Length == 0)
            setP1username("You");
    }

    void startGameClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");

        // GameType and PlayerTurn are set in "ToggleGroup.cs"
        SceneManager.LoadScene("GameBoard");
        FindObjectOfType<AudioManager>().StopCurrentSong(6);



        // Set a random player avatar for player 2 (the AI) other than p1's avatar
        setP2avatar(RandomPlayerAvatar(getP1avatar()));

        //SAVE P2 USERNAME HERE!!! (if you try to do this earlier, p2 avatar is still set from the previous game)
        if (getP2avatar() == PlayerAvatar.PEASANT) setP2username("Peasant");
        else if (getP2avatar() == PlayerAvatar.PHAROAH) setP2username("Pharaoh");
        else if (getP2avatar() == PlayerAvatar.SCRIBE) setP2username("Scribe");
        else if (getP2avatar() == PlayerAvatar.WORKER) setP2username("Worker");
    }

    void backClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        SceneManager.LoadScene("Menu");
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

        choosePlayer.SetActive(true);
    }

    void pharoahClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        resetAll();
        pharoah_H.SetActive(true);
        pharoah_figure.SetActive(true);

        //Turn of choose player text because a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.PHAROAH);
    }

    void scribeClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        resetAll();
        scribe_H.SetActive(true);
        scribe_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.SCRIBE);
    }

    void peasantClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        resetAll();
        peasant_H.SetActive(true);
        peasant_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.PEASANT);
    }

    void workerClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        resetAll();
        worker_H.SetActive(true);
        worker_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.WORKER);
    }
}
