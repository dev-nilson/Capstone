using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntroVideo : MonoBehaviour
{
    public GameObject gameIntro;
    float screenDelay = .5f;
    // Start is called before the first frame update
    void Start()
    {
        startGameIntroVideo();
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("cicked");
    //        SceneManager.LoadScene("Menu");
    //    }
    //}

    void startGameIntroVideo()
    {
        StartCoroutine("startVideo");
    }

    IEnumerator startVideo()
    {
        yield return new WaitForSeconds(screenDelay);
        SceneManager.LoadScene("Menu");
    }

    void OnMouseDown()
    {
        Debug.Log("cicked");
        SceneManager.LoadScene("Menu");
    }
}
