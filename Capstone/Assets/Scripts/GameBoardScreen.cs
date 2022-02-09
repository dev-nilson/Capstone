using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameBoardScreen : MonoBehaviour
{
    public Button back;

    void Start()
    {
        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);
    }

    void backClicked()
    {
        Debug.Log("qback clicked");

        SceneManager.LoadScene("Menu");
    }
}
