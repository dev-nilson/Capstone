using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;
using System.Collections;

public class TutorialScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;

    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;
    public GameObject scroll6;

    void Start()
    {
        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);
    }

    void backClicked()
    {
        //Debug.Log("back clicked");
        //FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        SceneManager.LoadScene("Menu");
    }
}
