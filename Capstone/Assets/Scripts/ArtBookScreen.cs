using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArtBookScreen : MonoBehaviour
{
    public Button back;

    // Start is called before the first frame update
    void Start()
    {
        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);
    }
    public void backClicked()
    {
        FindObjectOfType<AudioManager>().Play("stoneButtonPress");
        //Debug.Log("I got clicked!!1!");
        SceneManager.LoadScene("Menu");

        FindObjectOfType<AudioManager>().StopCurrentSong(1);


    }
}
