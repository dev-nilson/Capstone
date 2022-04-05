﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using static GameUtilities;

public class Scroll : MonoBehaviour
{

    public Button settings;
    public Button exitSettings;

    private static bool disabled;

    //This is for the setting scroll (makes it look like it's rolling up)
    public GameObject scroll1;
    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;
    public GameObject scroll6;

    // Start is called before the first frame update
    void Start()
    {
        Button settingsBtn = settings.GetComponent<Button>();
        settingsBtn.onClick.AddListener(settingsClicked);

        Button exitSettingsBtn = exitSettings.GetComponent<Button>();
        exitSettingsBtn.onClick.AddListener(delayDisplay);
    }

    void settingsClicked()
    {
        FindObjectOfType<AudioManager>().Play("goldButtonPress");
        if (!disabled)
        {
            Debug.Log("settings game");
            scroll1.SetActive(true);

            //switch phases to turn off build and place player to create a fake modal pop up box
            RotateMainCamera.DisableRotation();
            PauseGame();
            DisableButtons();
            HelpTimer.TurnOff();
        }
    }

    void delayDisplay()
    {
        StartCoroutine(exitSettingsClicked());
    }

    IEnumerator exitSettingsClicked()
    {
        //time that the scroll waits before it moves on to the next image
        float delay = .001f;

        GameObject[] scrollArray = new GameObject[7];

        //Get the Rectransform of each height in order to get the height 
        scrollArray[1] = scroll1;
        scrollArray[2] = scroll2;
        scrollArray[3] = scroll3;
        scrollArray[4] = scroll4;
        scrollArray[5] = scroll5;
        scrollArray[6] = scroll6;

        //Create a copy of the scrolls in order to reset the heights back to the original
        GameObject[] scrollReset = scrollArray;

        //BEAUTIFUL PIECE OF CODE --- Love Dad
        for (int scrollNum = 1; scrollNum <= 5; scrollNum++)
        {
            scrollArray[scrollNum].SetActive(true);
            float firstScrollRef = scrollArray[scrollNum].GetComponent<RectTransform>().rect.height;
            float tempHeight = firstScrollRef;
            float secondScrollRef = scrollArray[scrollNum + 1].GetComponent<RectTransform>().rect.height;
            for (float i = firstScrollRef; i >= (firstScrollRef - ((firstScrollRef - secondScrollRef))); i -= 8)
            {
                scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollArray[scrollNum].GetComponent<RectTransform>().rect.width, i);
                yield return new WaitForSecondsRealtime(delay);
            }
            scrollArray[scrollNum].SetActive(false);

            //add delay before is turns off
            if (scrollNum == 5)
            {
                scrollArray[6].SetActive(true);
                yield return new WaitForSeconds(.1f);
                scrollArray[6].SetActive(false);
            }
            //reset height value
            scrollArray[scrollNum].GetComponent<RectTransform>().sizeDelta = new Vector2(scrollReset[scrollNum].GetComponent<RectTransform>().rect.width, tempHeight);
        }

        RotateMainCamera.EnableRotation();
        PlayGame();
        EnableButtons();
        HelpTimer.Set();
    }

    public static void DisableButtons()
    {
        disabled = true;
    }

    public static void EnableButtons()
    {
        disabled = false;
    }

}
