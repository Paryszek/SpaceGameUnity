using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    GameObject player;
    bool active = true;
    Vector3 temp;
    int counter = 1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        temp = new Vector3(0.1f, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter = 0;
        }
        temp = transform.right* Mathf.Sin(Time.time * 0.01f);
        if (active && counter != 1) 
        {
            player.transform.position += temp;
            if (player.transform.position.x >= 5)
            {
                active = false;
                
            }
        } else if (counter != 1)
        {
            player.transform.position -= temp;
            if (player.transform.position.x <= 0)
            {
                active = true;
                counter++;
            }
        }
    }
}
