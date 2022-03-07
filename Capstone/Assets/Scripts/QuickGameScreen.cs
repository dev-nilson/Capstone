using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    ToggleGroup difficulty;
    ToggleGroup turn;

    GameObject difficultyToggle;
    GameObject turnToggle;

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
    }

    void startGameClicked()
    {
        Debug.Log("here");
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
    }

    void scribeClicked()
    {
        resetAll();
        scribe_H.SetActive(true);
        scribe_figure.SetActive(true);
    }

    void peasantClicked()
    {
        resetAll();
        peasant_H.SetActive(true);
        peasant_figure.SetActive(true);
    }

    void workerClicked()
    {
        resetAll();
        worker_H.SetActive(true);
        worker_figure.SetActive(true);
    }

}
