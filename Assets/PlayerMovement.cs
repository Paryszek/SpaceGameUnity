using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private GameObject player;
    private bool active = true;
    private bool move = false;
    private float timer = 0f;
    private string key = "space";
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        }
 
        if (Input.GetKeyUp(key))
        {           
            move = true;
        }

        if (move) 
        {                  
            if (active) 
            {                
                Vector3 temp = transform.right * Mathf.Sin(timer * 0.1f);   
                player.transform.position -= temp;
                if (player.transform.position.x <= -5)
                {
                    active = false;
                    
                }
            } else
            {
                Vector3 temp = transform.right * Mathf.Sin(timer * 0.1f);   
                player.transform.position += temp;
                if (player.transform.position.x >= 0)
                {
                    active = true;
                    move = false;
                }
            }
        }
    }
}
