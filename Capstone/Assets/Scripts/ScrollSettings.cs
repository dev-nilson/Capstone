using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUtilities;

public class ScrollSettings : MonoBehaviour
{
    public static ScrollSettings instance;
    public Toggle musicOn;
    public Toggle musicOff;
    public Slider slider;
    public Toggle hints;

    private static bool hintsOn = true;

        // Start is called before the first frame update
        void Start()
    {
        SceneManager.activeSceneChanged += setSoundSettings;

        //musicOn = GetComponent<Toggle>();

        hints.isOn = hintsOn;

        slider.value = AudioManager.musicVolume;
        musicOn.isOn = AudioManager.musicOn;
        musicOff.isOn = !AudioManager.musicOn;
    }

    void Update()
    {
        GameHints();
    }

    public void ToggleSound()
    {
        musicOn.isOn = AudioManager.musicOn;
        musicOff.isOn = !AudioManager.musicOn;
    }

    public void setSoundSettings(Scene current, Scene next)
    {

        Debug.Log(AudioManager.musicVolume);
        slider.value = AudioManager.musicVolume;
        Debug.Log("slider value is: " + slider.value);
        musicOn.isOn = AudioManager.musicOn;
        musicOff.isOn = !AudioManager.musicOn;
    }

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
