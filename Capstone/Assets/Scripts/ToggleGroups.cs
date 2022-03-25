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
            setGameType(GameType.EASY);
        }
        else if (isHard.isOn)
        {
            setGameType(GameType.DIFFICULT);
        }

        if (isFirst.isOn)
        {
            // In GameController, P1 is local player
            SetPlayerTurn(PlayerTurn.ONE);
        }
        else if (isSecond.isOn)
        {
            SetPlayerTurn(PlayerTurn.TWO);
        }
    }
    public void onStartGame()
    {
        ActiveToggle();
    }
}
