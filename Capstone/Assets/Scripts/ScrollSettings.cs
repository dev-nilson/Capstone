using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class ScrollSettings : MonoBehaviour
{
    //public static ScrollSettings instance;
    public Toggle musicOn;
    public Toggle musicOff;
    public Slider slider;
    public Toggle hints;

    private static bool hintsOn = true;

        //void Awake()
        //{
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //    else
        //    {
        //        Destroy(gameObject);
        //        Debug.Log("DESTROYED");
        //        return;
        //    }
        //
        //    DontDestroyOnLoad(gameObject);
        //}

        // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SCROLL STARTED");
        //SceneManager.activeSceneChanged += setSoundSettings;
        //SceneManager.activeSceneChanged += ToggleSound;

        //musicOn = GetComponent<Toggle>();

        hints.isOn = hintsOn;
        //Debug.Log("scroll volume value is: " + AudioManager.musicVolume);
        Debug.Log("scroll toggle value is: " + AudioManager.musicOn);
        //slider.value = AudioManager.musicVolume;
        //musicOn.isOn = AudioManager.musicOn;
        //musicOff.isOn = !AudioManager.musicOn;
        if (AudioManager.musicOn == true)
        {
            musicOn.isOn = true;
            musicOff.isOn = false;
        }
        else if (AudioManager.musicOn == false)
        {
            musicOff.isOn = true;
            musicOn.isOn = false;
        }
    }

    void Update()
    {
        GameHints();

        slider.value = AudioManager.musicVolume;
        ////Debug.Log(musicOn.isOn);
        //if (musicOn.isOn != AudioManager.musicOn)
        //{
        //    musicOn.isOn = AudioManager.musicOn;
        //    Debug.Log("the scroll value should get changed");
        //    musicOff.isOn = !AudioManager.musicOn;
        //}

        if (AudioManager.musicOn == true)
        {
            Debug.Log("AudioManager.musicOn is currently: " + AudioManager.musicOn);
            musicOn.isOn = true;
            musicOff.isOn = false;
        }
        else if (AudioManager.musicOn == false)
        {
            Debug.Log("AudioManager.musicOn is currently: " + AudioManager.musicOn);
            musicOff.isOn = true;
            musicOn.isOn = false;
        }
    }

    //public void ToggleSound(Scene current, Scene next)
    //{
    //    musicOn.isOn = AudioManager.musicOn;
    //    musicOff.isOn = !AudioManager.musicOn;
    //}

    //public void setSoundSettings(Scene current, Scene next)
    //{
    //    //Debug.Log("toggle value is: " + AudioManager.musicOn);

    //   Debug.Log(AudioManager.musicVolume);
    //    slider.value = AudioManager.musicVolume;
    //    //Debug.Log("slider value is: " + slider.value);
    //    musicOn.isOn = AudioManager.musicOn;
    //    musicOff.isOn = !AudioManager.musicOn;
    //}

    public void GameHints()
    {
        if (hints.isOn != hintsOn)
        {
            hintsOn = hints.isOn;

            if (!IsGameOver())
            {
                if (hints.isOn == true)
                {
                    HelpTimer.EnableScarab();
                    HelpTimer.Set();
                }
                else
                    HelpTimer.DisableScarab();
            }
        }
    }

    public static bool HintsOn()
    {
        return hintsOn;
    }
}