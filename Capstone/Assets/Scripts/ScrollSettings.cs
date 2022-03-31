using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUtilities;

public class ScrollSettings : MonoBehaviour
{
    public Toggle musicOn;
    public Toggle musicOff;
    public Slider slider;
    public Toggle hints;

    private static bool hintsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        musicOn = GetComponent<Toggle>();

        hints.isOn = hintsOn;
    }

    void Update()
    {
        GameHints();
    }

    public void ToggleSound()
    {
        if (musicOn.isOn)
        {
            //idk
        }
        else
        {
            //idk
        }
    }

    public void GameVolume()
    {
        //idk
    }

    public void GameHints()
    {
        if (hints.isOn != hintsOn)
        {
            Debug.Log("Changing whether hints are on");
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
