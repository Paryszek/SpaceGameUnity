﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


    public void ToGame()
    {
        Application.LoadLevel("SampleScene");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
