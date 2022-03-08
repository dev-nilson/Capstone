using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;

public class GameBoardScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;
    public GameObject settingsPopUp;
    public GameObject confirmExitPopUp;
    public Button ok;
    public Button cancel;


    void Start()
    {
        //settingsPopUp.SetActive(false); // false to hide, true to show

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(exitSettingsClicked);

        Button okBtn = ok.GetComponent<Button>();
        okBtn.onClick.AddListener(okClicked);

        Button cancelBtn = cancel.GetComponent<Button>();
        cancelBtn.onClick.AddListener(cancelClicked);
    }

    void backClicked()
    {
        confirmExitPopUp.SetActive(true);
        //switch phases to turn off build and place player to create a fake modal pop up box
        StorePhases();
        DisablePhases();
    }

    void okClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    void cancelClicked()
    {
        RestorePhases();
        confirmExitPopUp.SetActive(false);
    }

    void settingsClicked()
    {
        Debug.Log("settings game");
        settingsPopUp.SetActive(true);

        //switch phases to turn off build and place player to create a fake modal pop up box
        StorePhases();
        DisablePhases();
    }

    void exitSettingsClicked()
    {
        Debug.Log("exit");
        settingsPopUp.SetActive(false);
        //switch phases to turn off build and place player to create a fake modal pop 
        RestorePhases();
    }
}
