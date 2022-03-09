using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static GameUtilities;

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

            setGameType(GameType.EASY);
        }
        else if (isHard.isOn)
        {
            Debug.Log("HARD");

            setGameType(GameType.DIFFICULT);
        }

        if (isFirst.isOn)
        {
            Debug.Log("FIRST");

            // In GameController, P1 is local player
            SetPlayerTurn(PlayerTurn.ONE);
        }
        else if (isSecond.isOn)
        {
            Debug.Log("SECOND");

            SetPlayerTurn(PlayerTurn.TWO);
        }
    }
    public void onStartGame()
    {
        Debug.Log("Starting quick game....");

        ActiveToggle();
    }
}
