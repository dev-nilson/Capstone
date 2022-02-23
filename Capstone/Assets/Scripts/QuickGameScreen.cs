using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuickGameScreen : MonoBehaviour
{
    public Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        Button startGameBtn = startGame.GetComponent<Button>();
        startGameBtn.onClick.AddListener(startGameClicked);
    }

    void startGameClicked()
    {
        Debug.Log("here");
        SceneManager.LoadScene("GameBoard");
    }
}
