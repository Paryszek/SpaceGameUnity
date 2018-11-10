using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {
    public static Text gameOver;

    private float MAX_POWER = 0.06f;
    private float MIN_POWER = -0.06f;

    private float screenHalfWidth;
    private Rigidbody2D player;
    private Vector3 playerSpeed = new Vector3(0, 0);
    private float powerAmount = 0.2f;
    Text powerLeft;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        InitLabels();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        {
            powerAmount += 0.08f;
            Destroy(col.gameObject);
        }

    }

    void Update ()
    {

        powerLeft.text = (powerAmount * 100 / 0.2f).ToString() + '%';

        if (Input.GetKey(KeyCode.G) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x -= 0.005f;            
        }

        if (Input.GetKey(KeyCode.H) && powerAmount > 0)
        {
            powerAmount -= 0.005f;
            playerSpeed.x += 0.005f;
        }
       
        if (player.transform.position.x < -2)
        {
            player.transform.position = new Vector3((screenHalfWidth * 2) - 2, -1.5f);
        }
           
        if (player.transform.position.x > (screenHalfWidth * 2) - 2)
        {
            player.transform.position = new Vector3(-2, -1.5f);
        }

        if (playerSpeed.x >= MAX_POWER)
        {
            playerSpeed.x = MAX_POWER;
        }

        if (playerSpeed.x <= MIN_POWER)
        {
            playerSpeed.x = MIN_POWER;
        }

        player.transform.position += playerSpeed;

    }

    void InitLabels() {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("TextCanvas");
        Transform textTr = canvasObject.transform.Find("PowerLeft");
        powerLeft = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("GameOver");
        gameOver = textTr.GetComponent<Text>();
    }


}