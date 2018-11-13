using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
            CharacterController2D.gameOver.text = "Game Over";
            CharacterController2D.secondsText.text = "Seconds survived";
            CharacterController2D.seconds.text = Mathf.RoundToInt(Time.time).ToString();
            CharacterController2D.restartGameButton.transform.position = new Vector3(CharacterController2D.restartGameButtonInit.x, CharacterController2D.restartGameButtonInit.y, 0);
            CharacterController2D.time = 0f;
        }
    }
}

