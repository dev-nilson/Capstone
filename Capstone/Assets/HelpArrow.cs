using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using static GameUtilities;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class HelpArrow : MonoBehaviour
{
    public Button RightButton;
    public Button LeftButton;
    public Button step1;
    public Button step2;
    public Button step3;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public GameObject placeText;
   // public GameObject testText;
    public GameObject moveText;
    public GameObject buildText;

    public static int _index;

    private void Start()
    {
        Button _rightArrow = RightButton.GetComponent<Button>();
        _rightArrow.onClick.AddListener(RightButtonSelect);

        Button _leftArrow = LeftButton.GetComponent<Button>();
        _leftArrow.onClick.AddListener(LeftButtonSelect);

        _index = 1;
        //video1.SetActive(true);
        // script1.SetActive(true);
    }

    public void TutorialStart()
    {
        if (CanPlacePawn())
        {
            HelpArrow._index = 1;
            Debug.Log(_index);
            //step1.onClick.Invoke();
        }
        else if (CanMove())
        {
            HelpArrow._index = 2;
            Debug.Log(_index);
        }
        else if (CanBuild())
        {
            HelpArrow._index = 3;
            Debug.Log(_index);
        }
        // ++_index;
        //LeftButton.onClick.Invoke();
        swapVideo();
    }

    //public void TutorialEnd()
    //{
    //    placeText.SetActive(false);
    //    moveText.SetActive(false);
    //    buildText.SetActive(false);
    //}

    public void RightButtonSelect()
    {
        _index++;
        if (_index > 3)
            _index = 1;

        Debug.Log(_index);
        swapVideo();
        //  swapScript();
    }

    public void LeftButtonSelect()
    {
        _index--;
        if (_index < 1)
            _index = 3;
        swapVideo();
        // swapScript();
    }

    private void swapVideo()
    {
        placeText.SetActive(false);
        moveText.SetActive(false);
        buildText.SetActive(false);
        if (_index == 1)
        {
           // testText.SetActive(true);
            //step1.OnPointerClick(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));
            //video1.SetActive(true);
            //step1.onClick.Invoke();
            videoPlayer1.Play();
            videoPlayer2.Stop();
            videoPlayer3.Stop();

            //Debug.Log("")
            placeText.SetActive(true);
        }
        else if (_index == 2)
        {
            //videoPlayer1.SetActive(false);
            videoPlayer2.Play();
            videoPlayer1.Stop();
            videoPlayer3.Stop();
            // step2.onClick.Invoke();
            moveText.SetActive(true);
        }
        else 
        {
            //video1.SetActive(false);
            //video2.SetActive(false);
            //video3.SetActive(true);
            //video4.SetActive(false);
            //video5.SetActive(false);
            //video6.SetActive(false);
            videoPlayer3.Play();
            videoPlayer2.Stop();
            videoPlayer1.Stop();
            // step3.onClick.Invoke();
            buildText.SetActive(true);
        }
    }


}

