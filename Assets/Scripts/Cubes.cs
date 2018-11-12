using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;
    private float screenHalfWidth;

    void Start () {
        InvokeRepeating("Spawn", 2f, 1f);
        float halfCubeWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfCubeWidth;
    }
	
	void Spawn () {
		if (CharacterController2D.gameOver.text != "Game Over") 
		{
            if (Mathf.RoundToInt(Time.time) == 10)
            {
                InvokeRepeating("Spawn", 2f, 0.95f);
            }

            if (Mathf.RoundToInt(Time.time) == 20)
            {
                InvokeRepeating("Spawn", 2f, 0.9f);
            }

            if (Mathf.RoundToInt(Time.time) == 40)
            {
                InvokeRepeating("Spawn", 2f, 0.85f);
            }

            if (Mathf.RoundToInt(Time.time) == 60)
            {
                InvokeRepeating("Spawn", 2f, 0.80f);
            }

            if (Mathf.RoundToInt(Time.time) == 80)
            {
                InvokeRepeating("Spawn", 2f, 0.75f);
            }

            if (Mathf.RoundToInt(Time.time) == 100)
            {
                InvokeRepeating("Spawn", 2f, 0.70f);
            }

            if (Mathf.RoundToInt(Time.time) == 120)
            {
                InvokeRepeating("Spawn", 2f, 0.65f);
            }

            if (Mathf.RoundToInt(Time.time) == 140)
            {
                InvokeRepeating("Spawn", 2f, 0.6f);
            }

            Instantiate(cube, new Vector3(Random.Range(-screenHalfWidth, screenHalfWidth), 15, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

}
