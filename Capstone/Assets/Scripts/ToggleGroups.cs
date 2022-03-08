using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleGroups : MonoBehaviour
{
    public Toggle isEasy;
    public Toggle isHard;

    public Toggle isFirst;
    public Toggle isSecond;

    public void ActiveToggle()
    {
        if (isEasy.isOn)
        {
            Debug.Log("EASY");
        }
        else if (isHard.isOn)
        {
            Debug.Log("HARD");
        }

        if (isFirst.isOn)
        {
            Debug.Log("FIRST");
        }
        else if (isSecond.isOn)
        {
            Debug.Log("SECOND");
        }
    }
    public void onStartGame()
    {
        Debug.Log("Starting quick game....");

        ActiveToggle();
    }
}
