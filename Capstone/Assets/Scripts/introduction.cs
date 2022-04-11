using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class introduction : MonoBehaviour, IPointerDownHandler
{
    float screenDelay = 5f;

    public GameObject teamIntro;
    public GameObject gameIntro;
    public GameObject introductionPanel;

    int count = 0;

    void Start()
    {
        startTeamIntroVideo();
    }
   
    public void goToMenuClicked()
    {
        teamIntro.SetActive(false);
        gameIntro.SetActive(false);
    }

    void startTeamIntroVideo()
    {
        StartCoroutine("startTeamVideo");
    }

    IEnumerator startTeamVideo()
    {
        yield return new WaitForSeconds(screenDelay);
        teamIntro.SetActive(false);
        startGameIntroVideo();
    }

    void startGameIntroVideo()
    {
        gameIntro.SetActive(true);
        StartCoroutine("startGameVideo");
    }

    IEnumerator startGameVideo()
    {
        yield return new WaitForSeconds(screenDelay);
        gameIntro.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        count++;
        if(count == 1)
        {
            Debug.Log("first click");
            StopCoroutine("startTeamVideo");
            startGameIntroVideo();
        }
        else
        {
            StopCoroutine("startGameVideo");
            introductionPanel.SetActive(false);
        }
    }
}
