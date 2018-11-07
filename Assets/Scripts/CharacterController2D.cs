using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour {
    public static Text gameOver;

    private int LEFT_CHARACTER_BORDER = -5;
    private int RIGHT_CHARACTER_BORDER = 4;
    private float MAX_POWER = 1.5f;
    private Vector3 ROPE_SPEED = new Vector3(0.1f, 0, 0);

    private Rigidbody2D player;
    private bool active = true;
    private float timer = 0.5f;
    private string key = "space";
    private bool move = false;
    private int points = 0;
    Text powerAmount;
    Text pointsAmount;
    Vector3 temp;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        InitLabels();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bonus")
        {
            points += 1;
            pointsAmount.text = points.ToString();
            Destroy(col.gameObject);
        }

    }

    void Update ()
    {
        if (Input.GetKeyDown(key) && !move)
        { 
            timer = 0f;
        }
                
        if (Input.GetKey(key) && !move)
        {
            timer += Time.deltaTime;
            timer = timer % MAX_POWER;
            powerAmount.text = Mathf.RoundToInt(timer * 100 / MAX_POWER).ToString()  + '%';
        }
 
        if (Input.GetKeyUp(key))
        {           
            move = true;            
        }

        if (move) 
        {
            if (active) 
            {
                timer += (Time.deltaTime * 0.4f);
                player.transform.position -= transform.right * Mathf.Lerp(0, 1, Mathf.Sin(timer * 0.1f));
                if (player.transform.position.x <= LEFT_CHARACTER_BORDER)
                {
                    active = false;
                }
            } else
            {
                player.transform.position += ROPE_SPEED;
                if (player.transform.position.x >= RIGHT_CHARACTER_BORDER)
                {
                    active = true;
                    move = false;
                }
            }
        }
    }

    void InitLabels() {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("PowerCanvas");
        Transform textTr = canvasObject.transform.Find("Power");
        powerAmount = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("Points");
        pointsAmount = textTr.GetComponent<Text>();
        textTr = canvasObject.transform.Find("GameOver");
        gameOver = textTr.GetComponent<Text>();
    }


}
