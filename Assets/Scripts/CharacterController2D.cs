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
    public float shieldLifeTime;
    public string horizontalAxis = "Horizontal";

    private ParticleSystem particleLeft;
    private ParticleSystem particleRight;
    private bool stopParticlePlay = false;
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
    }    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        { 
            PowerAmount += 0.09f;
            PowerAmount = PowerAmount > initPower ? initPower : PowerAmount;            
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
        } else if (col.gameObject.tag == "Enemy")
        {
            GameOver.text = "Game Over";
            SecondsText.text = "Seconds survived";
            Seconds.text = Mathf.RoundToInt(CharacterController2D.time).ToString();
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

        if (SimpleInput.GetAxis(horizontalAxis) < 0 && PowerAmount > 0)
        {
            PowerAmount -= PowerUse;
            playerSpeed.x -= PowerUse;            
            particleLeft.Play();
        }

        if (SimpleInput.GetAxis(horizontalAxis) > 0 && PowerAmount > 0)
        {
            PowerAmount -= PowerUse;
            playerSpeed.x += PowerUse;
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

        playerSpeed.x = playerSpeed.x > MaxPower ? MaxPower : playerSpeed.x;
        playerSpeed.x = playerSpeed.x < MinPower ? MinPower : playerSpeed.x;

        transform.position += playerSpeed;

    }

    private void HandlePowerBar()
    {
        var powerProcent = PowerAmount * 100 / initPower;
        powerBar.fillAmount = powerProcent / 100;
    }

    private void HandleShieldBar()
    {
        if (shieldBar.fillAmount > 0)
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