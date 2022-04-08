using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teamIntroScreen : MonoBehaviour
{
    public GameObject teamIntro;
    public GameObject gameIntro;

    int clickCount = 0;
    int delay = 3;
    // Start is called before the first frame update
    void Start()
    {
        startTeamIntroVideo();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            teamIntro.SetActive(false);
            StopCoroutine("startTeamVideo");
            startGameIntroVideo();

            clickCount++;

            if (clickCount == 2)
            {
                Debug.Log("second click");
                SceneManager.LoadScene("Menu");
            }
        }
        
    }

    void startTeamIntroVideo()
    {
        StartCoroutine("startTeamVideo");
    }

    IEnumerator startTeamVideo()
    {
        yield return new WaitForSeconds(delay);
        teamIntro.SetActive(false);
        startGameIntroVideo();
    }

    void startGameIntroVideo()
    {
        StartCoroutine("starGameVideo");
    }

    IEnumerator starGameVideo()
    {
        gameIntro.SetActive(true);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menu");
    }
}


