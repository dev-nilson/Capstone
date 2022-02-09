using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public Button back;

    void Start()
    {
        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);
    }

    void backClicked()
    {
        Debug.Log("back clicked");

        SceneManager.LoadScene("Menu");
    }
}
