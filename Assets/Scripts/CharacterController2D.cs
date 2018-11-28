using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {
    public static Text gameOver;
    public static Text secondsText;
    public static Text seconds;    
    public static float time;
    public static float powerAmount = 5.6f;     

    public string horizontalAxis = "Horizontal";

    public GameObject shield;
    public float shieldLifeTime;

    public Image powerBar;
    public Image shieldBar;
    public Button RestartGame;
    public Button BackToMenu;
    public Button Left;
    public Button Right;

    private ParticleSystem particleLeft;
    private ParticleSystem particleRight;

    private bool stopParticlePlay = false;

    private float MAX_POWER = 0.12f;
    private float MIN_POWER = -0.12f;

    private Button leftButton;
    private Button rightButton;

    private float screenHalfWidth;
    private float initPower;
    private Vector3 playerSpeed = new Vector3(0, 0);

    private bool restartGame = false;

    void Start()
    {
        time = 0f;        
        initPower = powerAmount;
        InitScreenWidth();
        InitLabels();
        InitParticles();
        shield.SetActive(false);
        RestartGame.gameObject.SetActive(false);
        RestartGame.onClick.AddListener(() => RestartClick());
        BackToMenu.gameObject.SetActive(false);
        BackToMenu.onClick.AddListener(() => BackToMenuClick());
        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);


    }    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        { 
            powerAmount += 0.09f;
            powerAmount = powerAmount > initPower ? initPower : powerAmount;            
            Destroy(col.gameObject);
        } else if (col.gameObject.tag == "Shield Bonus")
        {
            shield.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            tag = "Shield";
            Destroy(col.gameObject);
            shieldBar.fillAmount = 1;
            StartCoroutine(StopShield(shieldLifeTime));            
        }

    }

    IEnumerator StopShield(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shield.SetActive(false);
        shield.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        tag = "Player";
    }


    void Update ()
    {
        time += Time.deltaTime;
        HandlePowerBar();
        HandleShieldBar();

        if (SimpleInput.GetAxis(horizontalAxis) < 0 && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x -= 0.005f;            
            particleLeft.Play();
        }

        if (SimpleInput.GetAxis(horizontalAxis) > 0 && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x += 0.005f;
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

    private void HandlePowerBar()
    {
        var powerProcent = powerAmount * 100 / initPower;
        powerBar.fillAmount = powerProcent / 100;
    }

    private void HandleShieldBar()
    {
        if (shieldBar.fillAmount > 0)
        {
            shieldBar.fillAmount -= Time.deltaTime / shieldLifeTime;
        }
    }

    private void InitLabels() {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("TextCanvas");
        Transform transform = canvasObject.transform.Find("GameOver");
        gameOver = transform.GetComponent<Text>();
        transform = canvasObject.transform.Find("SecondsText");
        secondsText = transform.GetComponent<Text>();
        transform = canvasObject.transform.Find("Seconds");
        seconds = transform.GetComponent<Text>();        
        
    }

    private void InitParticles()
    {
        particleLeft = GameObject.Find("Particle Left").GetComponent<ParticleSystem>();
        particleLeft.Stop();
        particleRight = GameObject.Find("Particle Right").GetComponent<ParticleSystem>();
        particleRight.Stop();
    }

    private void InitScreenWidth()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    public static void RestartClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public static void BackToMenuClick()
    {
        Application.LoadLevel("Menu");
    }

}