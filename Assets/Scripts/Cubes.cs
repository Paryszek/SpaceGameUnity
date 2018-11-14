using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;
    private float screenHalfWidth;

    void Start () {
        InvokeRepeating("Spawn", 2f, 1f);
        InitScreenWidth();        
    }
	
	void Spawn () {
		if (CharacterController2D.gameOver.text != "Game Over")
		{
		    var time = CharacterController2D.time;
            if (Mathf.RoundToInt(time) == 10)
            {
                InvokeRepeating("Spawn", 2f, 0.99f);
            }

            if (Mathf.RoundToInt(time) == 30)
            {
                InvokeRepeating("Spawn", 2f, 0.97f);
            }

            if (Mathf.RoundToInt(time) == 70)
            {
                InvokeRepeating("Spawn", 2f, 0.95f);
            }

            if (Mathf.RoundToInt(time) == 150)
            {
                InvokeRepeating("Spawn", 2f, 0.92f);
            }

            if (Mathf.RoundToInt(time) == 310)
            {
                InvokeRepeating("Spawn", 2f, 0.9f);
            }

            if (Mathf.RoundToInt(time) == 610)
            {
                InvokeRepeating("Spawn", 2f, 0.87f);
            }

            if (Mathf.RoundToInt(time) == 1210)
            {
                InvokeRepeating("Spawn", 2f, 0.85f);
            }

            if (Mathf.RoundToInt(time) == 2410)
            {
                InvokeRepeating("Spawn", 2f, 0.83f);
            }

            Instantiate(cube, new Vector3(Random.Range(-screenHalfWidth - 1, screenHalfWidth + 1), 15, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

    void InitScreenWidth()
    {
        float halfCubeWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfCubeWidth;
    }

}
