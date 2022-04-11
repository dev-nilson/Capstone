using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class teamIntroScreen : MonoBehaviour, IPointerDownHandler
{
    public GameObject teamIntro;
    int delay = 5;
    // Start is called before the first frame update
    void Start()
    {
        startTeamIntroVideo();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("GameIntro");
        }
    }

    void startTeamIntroVideo()
    {
        StartCoroutine("startVideo");
    }

    IEnumerator startVideo()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameIntro");
    }
}
