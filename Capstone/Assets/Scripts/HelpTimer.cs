﻿using System;
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

    private static bool scarabOn;
    private float onScreen_x;
    private float offScreen_x;

    void Start()
    {
        helptimer = this;
        helptimer._turnOff();

        // set x positions for on and off screen scarab
        onScreen_x = hintBanner.transform.position.x;
        offScreen_x = 1100.0f;

        hintBanner.transform.position = new Vector3(1100.0f, hintBanner.transform.position.y, hintBanner.transform.position.z);

        if (ScrollSettings.HintsOn())
            scarabOn = true;
        else
            scarabOn = false;
    }

    public static void Set()
    {
        if (scarabOn)
        {
            helptimer.startDelay();
        }
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
            if (!GamePaused())
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

                StartCoroutine("SlideIn");

                // eventually turn them off
                yield return new WaitForSecondsRealtime(delay);
                TurnOff();
            }
            StartCoroutine("HelpPopup");
        }
    }

    IEnumerator SlideIn()
    {
        float x_shift = 1.0f;
        Debug.Log("slide in called");
        while (hintBanner.transform.position.x > onScreen_x)
        {
            if (Math.Abs(hintBanner.transform.position.x - onScreen_x) < 6.0f)
                hintBanner.transform.position = new Vector3(onScreen_x, hintBanner.transform.position.y, hintBanner.transform.position.z);
            else
                hintBanner.transform.position -= new Vector3(x_shift, 0.0f, 0.0f);
            Debug.Log("slide on is running");
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator SlideOff()
    {
        Debug.Log("Starting slide off");
        float x_shift = 1.0f;

        while (hintBanner.transform.position.x < offScreen_x)
        {
            if (Math.Abs(hintBanner.transform.position.x - offScreen_x) < 6.0f)
                hintBanner.transform.position = new Vector3(offScreen_x, hintBanner.transform.position.y, hintBanner.transform.position.z);
            else
            {
                hintBanner.transform.position += new Vector3(x_shift, 0.0f, 0.0f);
                Debug.Log("slide off is being called");
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }

        // turn off popups
        build.SetActive(false);
        move.SetActive(false);
        place.SetActive(false);

        peasant.SetActive(false);
        pharoah.SetActive(false);
        scribe.SetActive(false);
        worker.SetActive(false);

        hintBanner.SetActive(false);
    }

    public static void TurnOff()
    {
        helptimer._turnOff();
    }

    // Non static function called by TurnOff()
    void _turnOff()
    {
        StopCoroutine("HelpPopup");

        StartCoroutine("SlideOff");
    }

    public static void DisableScarab()
    {
        scarabOn = false;
    }
    public static void EnableScarab()
    {
        scarabOn = true;
    }
}