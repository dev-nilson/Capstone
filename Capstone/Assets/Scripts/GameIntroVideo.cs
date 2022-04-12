using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameIntroVideo : MonoBehaviour, IPointerDownHandler
{
    float screenDelay = 5f;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("game intro was clicked");
        StopCoroutine("startVideo");
        SceneManager.LoadScene("Menu");
    }
}
