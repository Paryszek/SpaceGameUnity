using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {
    public static Text gameOver;
    public static Text secondsText;
    public static Text seconds;
    public static Button restartGameButton;
    public static Vector3 restartGameButtonInit;
    public static float time;

    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;

    private Text powerLeft;

    private float MAX_POWER = 0.12f;
    private float MIN_POWER = -0.12f;

    private Button leftButton;
    private Button rightButton;

    private float screenHalfWidth;
    private float powerAmount = 0.6f;
    private float initPower;
    private Vector3 playerSpeed = new Vector3(0, 0);

    private bool restartGame = false;

    void Start()
    {
        time = Time.time;
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
        initPower = powerAmount;
        InitLabels();
        particleLeft = GameObject.Find("Particle Left").GetComponent<ParticleSystem>();
        particleLeft.Stop();
        particleRight = GameObject.Find("Particle Right").GetComponent<ParticleSystem>();
        particleRight.Stop();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        { 
            powerAmount += 0.09f;
            powerAmount = powerAmount > initPower ? initPower : powerAmount;
            Destroy(col.gameObject);
        }

    }

    void Update ()
    {
        time += Time.deltaTime;
        powerLeft.text = powerAmount > 0 ? Mathf.RoundToInt((powerAmount * 100 / initPower)).ToString() + '%' : "0%";

        if (Input.GetKey(KeyCode.LeftArrow) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x -= 0.005f;
            

        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            particleLeft.Stop();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && powerAmount > 0)
        {
            particleLeft.Play();
        }


        if (Input.GetKey(KeyCode.RightArrow) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x += 0.005f;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            particleRight.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && powerAmount > 0)
        {
            particleRight.Play();
        }

        if (transform.position.x < -screenHalfWidth)
        {
            transform.position = new Vector3(screenHalfWidth, transform.position.y);
        }
           
        if (transform.position.x > screenHalfWidth)
        {
            transform.position = new Vector3(-screenHalfWidth, transform.position.y);
        }

        playerSpeed.x = playerSpeed.x > MAX_POWER ? MAX_POWER : playerSpeed.x;
        playerSpeed.x = playerSpeed.x < MIN_POWER ? MIN_POWER : playerSpeed.x;

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
        textTr = canvasObject.transform.Find("Button");
        restartGameButton = textTr.GetComponent<Button>();
        restartGameButton.onClick.AddListener(HandleClick);
        restartGameButtonInit = new Vector3(restartGameButton.transform.position.x, restartGameButton.transform.position.y, 0);
        restartGameButton.transform.position = new Vector3(-500, -500);
        textTr = canvasObject.transform.Find("LeftButton");
        //leftButton = textTr.GetComponent<Button>();
        //leftButton.onClick.AddListener(LeftClick);
        textTr = canvasObject.transform.Find("RightButton");
        //rightButton = textTr.GetComponent<Button>();
        //rightButton.onClick.AddListener(RightClick);
    }

    public static void HandleClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LeftClick()
    {
        if (powerAmount > 0)
        {
            powerAmount -= 0.01f;
            playerSpeed.x -= 0.01f;
        }
        
    }

    public void RightClick()
    {
        if (powerAmount > 0)
        {
            powerAmount -= 0.01f;
            playerSpeed.x += 0.01f;
        }


    }


}