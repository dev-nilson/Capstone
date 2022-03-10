using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using static GameUtilities;
using System.Collections;
using UnityEngine.Events;
using System;
using static GameUtilities;

public class GameBoardScreen : MonoBehaviour
{
    public Button back;
    public Button settings;
    public Button exitSettings;
    public GameObject confirmExitPopUp;
    public Button ok;
    public Button cancel;

    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;
    public GameObject scroll6;




    void Start()
    {
        //settingsPopUp.SetActive(false); // false to hide, true to show

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);

        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(delayDisplay);

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
        ClearGame();
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
        scroll1.SetActive(true);

        //switch phases to turn off build and place player to create a fake modal pop up box
        StorePhases();
        DisablePhases();
    }

    IEnumerator wait()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(4f);
    }

    void delayDisplay()
    {
        StartCoroutine(exitSettingsClicked());
    }

    IEnumerator exitSettingsClicked()
    {
        //When you close the scroll you cycle through the scroll images
        scroll1.SetActive(false);

        scroll2.SetActive(true);
        yield return new WaitForSeconds(.2f);
        scroll2.SetActive(false);
        Debug.Log("closed");

        scroll3.SetActive(true);
        yield return new WaitForSeconds(.2f);
        scroll3.SetActive(false);
        Debug.Log("closed");

        scroll4.SetActive(true);
        yield return new WaitForSeconds(.2f);
        scroll4.SetActive(false);
        Debug.Log("closed");

        scroll5.SetActive(true);
        yield return new WaitForSeconds(.2f);
        scroll5.SetActive(false);
        Debug.Log("closed");

        scroll6.SetActive(true);
        yield return new WaitForSeconds(.3f);
        scroll6.SetActive(false);
        Debug.Log("closed");

        //switch phases to turn off build and place player to create a fake modal pop 
        RestorePhases();
    }
}