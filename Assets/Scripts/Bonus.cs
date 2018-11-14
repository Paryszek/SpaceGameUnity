using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public GameObject bonus;
    private float screenHalfWidth;

    void Start()
    {
        InvokeRepeating("Spawn", 0.01f, 2f);
        InitScreenWidth();
    }

    void Spawn()
    {
        if (CharacterController2D.gameOver.text != "Game Over") 
		{
            bonus = Instantiate(bonus, new Vector3(Random.Range(-screenHalfWidth, screenHalfWidth), 15, 0), Quaternion.identity);
            bonus.name = "Bonus";
        }
    }

    void InitScreenWidth()
    {
        float halfCubeWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfCubeWidth;
    }

}
