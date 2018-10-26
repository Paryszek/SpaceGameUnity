using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {
    private int LEFT_CHARACTER_BORDER = -5;
    private int RIGHT_CHARACTER_BORDER = 4;
    private float MAX_POWER = 1.5f;

    private Rigidbody2D player;
    private bool active = true;
    private bool move = false;
    private float timer = 0.5f;
    private string key = "space";
    
   
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(key) && !move)
        { 
            timer = 0.5f;
        }
                
        if (Input.GetKey(key) && !move)
        {
            timer += Time.deltaTime;
        }
 
        if (Input.GetKeyUp(key))
        {           
            move = true;
            timer = timer % MAX_POWER;
        }

        if (move) 
        {                  
            if (active) 
            {                                
                Vector3 temp = transform.right * Mathf.Sin(timer * 0.1f);   

                player.transform.position -= temp;
                if (player.transform.position.x <= LEFT_CHARACTER_BORDER)
                {
                    active = false;
                    
                }
            } else
            {
                Vector3 temp = transform.right * Mathf.Sin(timer * 0.1f);   
                player.transform.position += temp;
                if (player.transform.position.x >= RIGHT_CHARACTER_BORDER)
                {
                    active = true;
                    move = false;
                }
            }
        }
    }
}
