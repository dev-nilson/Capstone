using UnityEngine.SceneManagement;
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
    public Button step1Continue;
    public Button step2;
    public Button step3;
    public Button step4;
    public Button step5;
    public Button step6;
    public Button step7;
    public Button step8;
    public Button step9;
    public Button FinalButton;
    public Button StartButton;
    public Button BackToMenu;
    public GameObject Stone;
    private int _index;


    private void Start()
    {
        Button _rightArrow = RightArrow.GetComponent<Button>();
        _rightArrow.onClick.AddListener(RightArrowSelect);

        Button _leftArrow = LeftArrow.GetComponent<Button>();
        _leftArrow.onClick.AddListener(LeftArrowSelect);

        Button backToMenu = BackToMenu.GetComponent<Button>();
        backToMenu.onClick.AddListener(ChangeScene);

        Button startButton = StartButton.GetComponent<Button>();
        startButton.onClick.AddListener(RightArrowSelect2);

        _index = 2;
        //video1.SetActive(true);
        // script1.SetActive(true);
    }

    public void RightArrowSelect2()
    {
        _index = 2;
        swapVideo();
    }


    public void RightArrowSelect()
    {
        
        _index++;
     
        if (_index > 10)
            _index = 1;

        Debug.Log(_index);
        swapVideo();
        //  swapScript();
    }

    public void LeftArrowSelect()
    {
        
        _index--;

        if (_index < 1)
            _index = 10;
        Debug.Log(_index);
        swapVideo();
        // swapScript();
    }

    private void swapVideo()
    {

        if (_index == 1)
        {
            //video1.SetActive(true);
           step1.onClick.Invoke();
        }
        else if (_index == 2)
        {
          
            step2.onClick.Invoke();
        }
        else if (_index == 3)
        {
           
            step3.onClick.Invoke();
        }
        else if (_index == 4)
        {
            
            step4.onClick.Invoke();
        }
        else if (_index == 5)
        {
            
            step5.onClick.Invoke();
        }
        else if (_index == 6)
        {
          
            step6.onClick.Invoke();
        }
        else if (_index == 7)
        {
          
            step7.onClick.Invoke();
        }
        else if (_index == 8)
        {
            
            step8.onClick.Invoke();
        }
        else if (_index == 9)
        {
            
            step9.onClick.Invoke();
        }
        else
        {
            FinalButton.onClick.Invoke();
            Stone.SetActive(true);
            _index = 2;
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }
}

