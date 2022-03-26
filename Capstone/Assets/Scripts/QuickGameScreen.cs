﻿using System.Collections;
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

        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        usernameInputField.onEndEdit.AddListener(delegate { LockInput(usernameInputField); });
        if (usernameInputField.text.Length == 0)
            setP1username("You");

        //SAVE USERNAME HERE!!!
        if (getP2avatar() == PlayerAvatar.PEASANT) setP2username("Peasant");
        else if (getP2avatar() == PlayerAvatar.PHAROAH) setP2username("Pharaoh");
        else if (getP2avatar() == PlayerAvatar.SCRIBE) setP2username("Scribe");
        else if (getP2avatar() == PlayerAvatar.WORKER) setP2username("Worker");
    }

    void startGameClicked()
    {
        // GameType and PlayerTurn are set in "ToggleGroup.cs"

        SceneManager.LoadScene("GameBoard");
        FindObjectOfType<AudioManager>().StopCurrentSong(6);
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
    }

    void backClicked()
    {
        SceneManager.LoadScene("Menu");
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
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
        resetAll();
        pharoah_H.SetActive(true);
        pharoah_figure.SetActive(true);

        //Turn of choose player text because a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.PHAROAH);

        // Set a random player avatar for player 2 (the AI) other than the pharoah
        setP2avatar(RandomPlayerAvatar(PlayerAvatar.PHAROAH));
    }

    void scribeClicked()
    {
        resetAll();
        scribe_H.SetActive(true);
        scribe_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.SCRIBE);

        // Set a random player avatar for player 2 (the AI) other than the scribe
        setP2avatar(RandomPlayerAvatar(PlayerAvatar.SCRIBE));
    }

    void peasantClicked()
    {
        resetAll();
        peasant_H.SetActive(true);
        peasant_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.PEASANT);

        // Set a random player avatar for player 2 (the AI) other than the peasant
        setP2avatar(RandomPlayerAvatar(PlayerAvatar.PEASANT));
    }

    void workerClicked()
    {
        resetAll();
        worker_H.SetActive(true);
        worker_figure.SetActive(true);

        //Turn of choose player text becasue a player was selected
        choosePlayer.SetActive(false);

        setP1avatar(PlayerAvatar.WORKER);

        // Set a random player avatar for player 2 (the AI) other than the worker
        setP2avatar(RandomPlayerAvatar(PlayerAvatar.WORKER));
    }
}
