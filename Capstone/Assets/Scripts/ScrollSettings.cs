using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSettings : MonoBehaviour
{
    public Toggle musicOn;
    public Toggle musicOff;
    public Slider slider;
    public Toggle hints;

    // Start is called before the first frame update
    void Start()
    {
        musicOn = GetComponent<Toggle>();
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
        if (hints.isOn)
        {
            //turn on hints
            Debug.Log("Turning on hints");
            HelpTimer.Set();
        }
        else
        {
            //turn off hints
            Debug.Log("Turning off hints");
            HelpTimer.TurnOff();
        }
    }
}
