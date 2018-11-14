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
        initPower = powerAmount;
        InitScreenWidth();
        InitLabels();
        InitButtons();
        InitParticles();        
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
        Transform transform = canvasObject.transform.Find("PowerLeft");
        powerLeft = transform.GetComponent<Text>();
        transform = canvasObject.transform.Find("GameOver");
        gameOver = transform.GetComponent<Text>();
        transform = canvasObject.transform.Find("SecondsText");
        secondsText = transform.GetComponent<Text>();
        transform = canvasObject.transform.Find("Seconds");
        seconds = transform.GetComponent<Text>();        
        
    }

    void InitButtons()
    {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("TextCanvas");
        Transform transform = canvasObject.transform.Find("RestartButton");
        restartGameButton = transform.GetComponent<Button>();
        restartGameButton.onClick.AddListener(RestartClick);
        restartGameButtonInit = new Vector3(restartGameButton.transform.position.x, restartGameButton.transform.position.y, 0);
        restartGameButton.transform.position = new Vector3(-500, -500);
        transform = canvasObject.transform.Find("LeftButton");
        //leftButton = textTr.GetComponent<Button>();
        //leftButton.onClick.AddListener(LeftClick);
        transform = canvasObject.transform.Find("RightButton");
        //rightButton = textTr.GetComponent<Button>();
        //rightButton.onClick.AddListener(RightClick);
    }

    void InitParticles()
    {
        particleLeft = GameObject.Find("Particle Left").GetComponent<ParticleSystem>();
        particleLeft.Stop();
        particleRight = GameObject.Find("Particle Right").GetComponent<ParticleSystem>();
        particleRight.Stop();
    }

    void InitScreenWidth()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    public static void RestartClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //Android build
    //public void LeftClick()
    //{
    //    if (powerAmount > 0)
    //    {
    //        powerAmount -= 0.01f;
    //        playerSpeed.x -= 0.01f;
    //    }
        
    //}

    //public void RightClick()
    //{
    //    if (powerAmount > 0)
    //    {
    //        powerAmount -= 0.01f;
    //        playerSpeed.x += 0.01f;
    //    }
    //}


}