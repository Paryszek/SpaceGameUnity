using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {   
    public static float time;
    public static bool alive;

    public float PowerAmount;
    public float MaxPower;
    public float MinPower;
    public float PowerUse;

    public Image powerBar;
    public Image shieldBar;
    public Button RestartGame;
    public Button BackToMenu;
    public Button Left;
    public Button Right;
    public Text GameOver;
    public Text SecondsText;
    public Text Seconds;    
    public GameObject shield;
    public GameObject Astronaut;

    public float shieldLifeTime;
    public string horizontalAxis = "Horizontal";

    private bool gameOverEmited;
    private ParticleSystem particleLeft;
    private ParticleSystem particleRight;
    private bool playParticleLeft = false;
    private bool playParticleRight = false;
    private Button leftButton;
    private Button rightButton;
    private float screenHalfWidth;
    private float initPower;
    private Vector3 playerSpeed = new Vector3(0, 0);
    private bool restartGame = false;

    void Start()
    {
        time = 0f;
        alive = true;
        initPower = PowerAmount;
        InitScreenWidth();
        InitParticles();
        shield.SetActive(false);
        RestartGame.gameObject.SetActive(false);
        RestartGame.onClick.AddListener(() => RestartClick());
        BackToMenu.gameObject.SetActive(false);
        BackToMenu.onClick.AddListener(() => BackToMenuClick());
        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);
        Astronaut.SetActive(true);
        gameOverEmited = false;
    }    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        { 
            PowerAmount += 0.09f;
            PowerAmount = PowerAmount > initPower ? initPower : PowerAmount;            
            Destroy(col.gameObject);
        } 
        if (col.gameObject.tag == "Shield Bonus")
        {
            if (!shield.active) 
            {
                shieldBar.fillAmount = 1;
            }
            shield.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            tag = "Shield";
            Destroy(col.gameObject);
            StartCoroutine(StopShield(shieldLifeTime));            
        } 


    }

    IEnumerator StopShield(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shield.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        tag = "Player";
    }


    void Update ()
    {
        time += Time.deltaTime;
        HandlePowerBar();
        HandleShield();
        HandleGameOver();

        if (SimpleInput.GetAxis(horizontalAxis) < 0 && PowerAmount > 0)
        {
            PowerAmount -= PowerUse;
            playerSpeed.x -= PowerUse;
            if (playParticleLeft == false)
            {
                particleLeft.Play();
                playParticleLeft = true;
            }
        }
        else if (SimpleInput.GetAxis(horizontalAxis) == 0)
        {
            playParticleLeft = false;
            playParticleRight = false;
            particleRight.Stop();
            particleLeft.Stop();
        }
        else if (SimpleInput.GetAxis(horizontalAxis) > 0 && PowerAmount > 0)
        {
            PowerAmount -= PowerUse;
            playerSpeed.x += PowerUse;
            if (playParticleRight == false)
            {
                particleRight.Play();
                playParticleRight = true;
            }
        }

        if (transform.position.x < -screenHalfWidth)
        {
            transform.position = new Vector3(screenHalfWidth, transform.position.y);
        }
           
        if (transform.position.x > screenHalfWidth)
        {
            transform.position = new Vector3(-screenHalfWidth, transform.position.y);
        }

        playerSpeed.x = playerSpeed.x > MaxPower ? MaxPower : playerSpeed.x;
        playerSpeed.x = playerSpeed.x < MinPower ? MinPower : playerSpeed.x;

        transform.position += playerSpeed;

    }

    private void HandleGameOver() {
        if (!alive && !gameOverEmited)
        {
            GameOver.text = "Game Over";
            SecondsText.text = "Seconds survived";
            Seconds.text = Mathf.RoundToInt(CharacterController2D.time).ToString();
            RestartGame.gameObject.SetActive(true);
            BackToMenu.gameObject.SetActive(true);
            Left.gameObject.SetActive(false);
            Right.gameObject.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
            Astronaut.SetActive(false);
            gameOverEmited = true;
            particleLeft.Stop();
            particleRight.Stop();
        }
    }

    private void HandlePowerBar()
    {
        var powerProcent = PowerAmount * 100 / initPower;
        powerBar.fillAmount = powerProcent / 100;
    }

    private void HandleShield()
    {
        if (shieldBar.fillAmount > 0 && alive)
        {
            shieldBar.fillAmount -= Time.deltaTime / shieldLifeTime;
        }
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