using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GameUtilities;

public class HelpTimer : MonoBehaviour
{
    public GameObject hintBanner;
    public GameObject pharoah;
    public GameObject scribe;
    public GameObject peasant;
    public GameObject worker;

    public GameObject move;
    public GameObject build;
    public GameObject place;

    static float delay = 10.0f;

    static HelpTimer helptimer;

    void Start()
    {
        helptimer = this;
    }

    public static void Set()
    {
        helptimer.startDelay();
    }

    // Non static function called by Set()
    void startDelay()
    {
        // turn off current popups
        helptimer._turnOff();

        StartCoroutine("HelpPopup");
    }

    IEnumerator HelpPopup()
    {
        yield return new WaitForSecondsRealtime(delay);

        if (GetPlayerTurn() == PlayerTurn.ONE)
        {
            hintBanner.SetActive(true);

            if (getP1avatar() == PlayerAvatar.PEASANT) peasant.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.PHAROAH) pharoah.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.SCRIBE) scribe.SetActive(true);
            else if (getP1avatar() == PlayerAvatar.WORKER) worker.SetActive(true);

            // display popup
            if (CanPlacePawn()) place.SetActive(true);
            else if (CanBuild()) build.SetActive(true);
            else if (CanMove()) move.SetActive(true);
        }
    }

    public static void TurnOff()
    {
        helptimer._turnOff();
    }

    // Non static function called by TurnOff()
    void _turnOff()
    {
        StopCoroutine("HelpPopup");

        // turn off popups

        // TO DO: ADD THING FOR PLACING PAWN!!!!!!!!!!!!!!!
        build.SetActive(false);
        move.SetActive(false);
        place.SetActive(false);

        peasant.SetActive(false);
        pharoah.SetActive(false);
        scribe.SetActive(false);
        worker.SetActive(false);

        hintBanner.SetActive(false);
    }
}