using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntroVideo : MonoBehaviour
{
    public GameObject gameIntro;
    int screenDelay = 1;
    // Start is called before the first frame update
    void Start()
    {
        startGameIntroVideo();
    }

    void startGameIntroVideo()
    {
        StartCoroutine("startVideo");
    }

    IEnumerator startVideo()
    {
        yield return new WaitForSeconds(screenDelay);
        SceneManager.LoadScene("Menu");
    }
}
