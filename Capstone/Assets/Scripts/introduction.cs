using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class introduction : MonoBehaviour
{
    float screenDelay = 5f;

    public GameObject teamIntro;
    public GameObject gameIntro;

    public Button goToGameIntro;
    public Button goToMenu;

    void Start()
    {
        Debug.Log("Here");
        teamIntro.SetActive(true);

        Button goToGameIntroBtn = goToGameIntro.GetComponent<Button>();
        goToGameIntroBtn.onClick.AddListener(goToGameIntroClicked);

        Button goToMenuBtn = goToMenu.GetComponent<Button>();
        goToMenuBtn.onClick.AddListener(goToMenuClicked);
    }
    public void goToGameIntroClicked()
    {
        teamIntro.SetActive(false);
        gameIntro.SetActive(true);
    }
    public void goToMenuClicked()
    {
        teamIntro.SetActive(false);
        gameIntro.SetActive(false);
    }
}
