using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public Canvas HowToPlayCanvas;

    void Start()
    {
        HowToPlayCanvas.gameObject.SetActive(false);
    }

    public void ToGame()
    {
        Application.LoadLevel("SampleScene");
    }

    public void HowToPlay()
    {
        HowToPlayCanvas.gameObject.SetActive(true);
    }

    public void ExitHowToPlay()
    {
        HowToPlayCanvas.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
