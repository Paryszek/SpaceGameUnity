using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {
    public static Text gameOver;
    public static Text secondsText;
    public static Text seconds;

    private Text powerLeft;

    private float MAX_POWER = 0.15f;
    private float MIN_POWER = -0.15f;

    private float screenHalfWidth;
    private float powerAmount = 0.6f;
    private float initPower;
    private float time;
    private Vector3 playerSpeed = new Vector3(0, 0);

    void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
        initPower = powerAmount;
        InitLabels();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        { 
            powerAmount += 0.09f;
            Destroy(col.gameObject);
        }

    }

    void Update ()
    {        
        powerLeft.text = powerAmount > 0 ? (powerAmount * 100 / initPower).ToString() + '%' : "0%";

        if (Input.GetKey(KeyCode.LeftArrow) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x -= 0.005f;            
        }

        if (Input.GetKey(KeyCode.RightArrow) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x += 0.005f;
        }
       
        if (transform.position.x < -screenHalfWidth)
        {
            transform.position = new Vector3(screenHalfWidth, transform.position.y);
        }
           
        if (transform.position.x > screenHalfWidth)
        {
            transform.position = new Vector3(-screenHalfWidth, transform.position.y);
        }

        powerAmount = powerAmount > MAX_POWER ? MAX_POWER : powerAmount;
        powerAmount = powerAmount < MIN_POWER ? MIN_POWER : powerAmount;

        transform.position += playerSpeed;

    }

    void InitLabels() {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("TextCanvas");
        Transform textTr = canvasObject.transform.Find("PowerLeft");
        powerLeft = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("GameOver");
        gameOver = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("SecondsText");
        secondsText = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("Seconds");
        seconds = textTr.GetComponent<Text>();
    }


}