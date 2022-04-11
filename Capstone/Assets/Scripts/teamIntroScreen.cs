using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class teamIntroScreen : MonoBehaviour, IPointerDownHandler
{
    int delay = 3;
    // Start is called before the first frame update
    void Start()
    {
        startTeamIntroVideo();
    }

    void startTeamIntroVideo()
    {
        StartCoroutine("startTeamVideo");
    }

    IEnumerator startTeamVideo()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameIntro");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("this was clicked");
        StopCoroutine("startTeamVideo");
        SceneManager.LoadScene("GameIntro");
    }
}


