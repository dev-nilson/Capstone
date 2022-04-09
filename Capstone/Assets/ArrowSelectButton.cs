﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class ArrowSelectButton : MonoBehaviour
{
    public Button RightArrow;
    public Button LeftArrow;
    public Button step1;
    public Button step2;
    public Button step3;
    public Button step4;
    public Button step5;
    public Button step6;
    public Button step7;
    public Button step8;
    public Button step9;
    public Button FinalButton;
    public int _index;

    //public GameObject video1;
    //public GameObject video2;
    //public GameObject video3;
    //public GameObject video4;
    //public GameObject video5;
    //public GameObject video6;

    //public GameObject script1;
    //public GameObject script2;
    //public GameObject script3;
    //public GameObject script4;
    //public GameObject script5;
    //public GameObject script6;

    private void Start()
    {
        Button _rightArrow = RightArrow.GetComponent<Button>();
        _rightArrow.onClick.AddListener(RightArrowSelect);

        Button _leftArrow = LeftArrow.GetComponent<Button>();
        _leftArrow.onClick.AddListener(LeftArrowSelect);

        _index = 0;
        //video1.SetActive(true);
        // script1.SetActive(true);
    }

    public void RightArrowSelect()
    {
        _index++;
        if (_index > 9)
            _index = 0;

        Debug.Log(_index);
        swapVideo();
        //  swapScript();
    }

    public void LeftArrowSelect()
    {
        _index--;
        if (_index < 1)
            _index = 9;
        swapVideo();
        // swapScript();
    }

    private void swapVideo()
    {

        //video1.SetActive(false);
        //video2.SetActive(false);
        //video3.SetActive(false);
        //video4.SetActive(false);
        //video5.SetActive(false);
        //video6.SetActive(false);

        if (_index == 1)
        {
            //video1.SetActive(true);
            step1.onClick.Invoke();

        }
        else if (_index == 2)
        {
            //video1.SetActive(false);
            //video2.SetActive(true);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(false);
            //video6.SetActive(false);
            step2.onClick.Invoke();
        }
        else if (_index == 3)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(true);
            //video4.SetActive(false);
            //video5.SetActive(false);
            //video6.SetActive(false);
            step3.onClick.Invoke();
        }
        else if (_index == 4)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(true);
            //video5.SetActive(false);
            //video6.SetActive(false);
            step4.onClick.Invoke();
        }
        else if (_index == 5)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(true);
            //video6.SetActive(false);
            step5.onClick.Invoke();
        }
        else if (_index == 6)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(true);
            //video6.SetActive(false);
            step6.onClick.Invoke();
        }
        else if (_index == 7)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(true);
            //video6.SetActive(false);
            step7.onClick.Invoke();
        }
        else if (_index == 8)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(true);
            //video6.SetActive(false);
            step8.onClick.Invoke();
        }
        else if (_index == 9)
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(false);
            //video4.SetActive(false);
            //video5.SetActive(false);
            //video6.SetActive(true);
            step9.onClick.Invoke();
        }
        else
        {
            FinalButton.onClick.Invoke();
        }
    }


    //private void swapScript()
    //{
    //    script1.SetActive(false);
    //    script2.SetActive(false);
    //    script3.SetActive(false);
    //    script4.SetActive(false);
    //    script5.SetActive(false);
    //    script6.SetActive(false);

    //    if (_index == 1)
    //        script1.SetActive(true);
    //    else if (_index == 2)
    //        script2.SetActive(true);
    //    else if (_index == 3)
    //        script3.SetActive(true);
    //    else if (_index == 4)
    //        script4.SetActive(true);
    //    else if (_index == 5)
    //        script5.SetActive(true);
    //    else
    //        script6.SetActive(true);
    //}
}

