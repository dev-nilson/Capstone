using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StoryModeScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;
    public GameObject settingsPopUp;

    void Start()
    {
        settingsPopUp.SetActive(false);

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(exitSettingsClicked);
    }

    void backClicked()
    {
        Debug.Log("back clicked");

        SceneManager.LoadScene("Menu");
    }

    void settingsClicked()
    {
        Debug.Log("settings game");
        settingsPopUp.SetActive(true); // false to hide, true to show
    }

    void exitSettingsClicked()
    {
        Debug.Log("exit");
        settingsPopUp.SetActive(false); // false to hide, true to show
    }
}
