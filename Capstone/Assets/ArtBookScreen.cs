﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArtBookScreen : MonoBehaviour
{
    public Button back;

    // Start is called before the first frame update
    void Start()
    {
        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backClicked);
    }
    void backClicked()
    {
        SceneManager.LoadScene(0);
    }
}
