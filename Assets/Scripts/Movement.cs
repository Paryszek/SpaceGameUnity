using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private float screenHalfWidth;
    private float targetPosition;
    public Button RestartGame;
    public Button BackToMenu;
    public Button Left;
    public Button Right;

    // Use this for initialization
    void Start()
    {
        InitScreenWidth();
        targetPosition = Random.Range(-screenHalfWidth, screenHalfWidth);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < targetPosition) 
        {
            transform.position = new Vector3(transform.position.x + 0.009f, transform.position.y);
        } else if (transform.position.x >= targetPosition)
        {
            transform.position = new Vector3(transform.position.x - 0.009f, transform.position.y);
        }                    

        if (transform.localPosition.y < -15)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);    
            RestartGame.gameObject.SetActive(true);
            BackToMenu.gameObject.SetActive(true);
            Left.gameObject.SetActive(false);
            Right.gameObject.SetActive(false);
            CharacterController2D.alive = false;
        }
        else if (col.gameObject.tag == "Shield")
        {
            Destroy(this);
        }
    }

    void InitScreenWidth()
    {
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
    }
}

