using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;
    private float screenHalfWidth;

    void Start () {
        InvokeRepeating("Spawn", 1f, 0.7f);
        float halfCubeWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfCubeWidth;
    }
	
	void Spawn () {
		if (CharacterController2D.gameOver.text != "Game Over") 
		{
			Instantiate(cube, new Vector3(Random.Range(-screenHalfWidth, screenHalfWidth), 15, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

}
