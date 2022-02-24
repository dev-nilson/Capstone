using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameBoardScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;
    public GameObject settingsPopUp;


    void Start()
    {
        //settingsPopUp.SetActive(false); // false to hide, true to show

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
        settingsPopUp.SetActive(true); 

        //switch phases to turn off build and place player to create a fake modal pop up box
    }

    void exitSettingsClicked()
    {
        Debug.Log("exit");
        settingsPopUp.SetActive(false);

        //switch phases to turn off build and place player to create a fake modal pop 
    }
}
