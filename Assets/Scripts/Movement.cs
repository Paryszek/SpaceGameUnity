using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private float screenHalfWidth;
    private float targetPosition;

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
            CharacterController2D.alive = false;
        }
    }

    void InitScreenWidth()
    {
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
    }
}

