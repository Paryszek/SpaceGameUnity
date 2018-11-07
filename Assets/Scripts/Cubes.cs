using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;

	void Start () {
        	InvokeRepeating("Spawn", 1f, 3f);
	}
	
	void Spawn () {
		if (CharacterController2D.gameOver.text != "Game Over") 
		{
			Instantiate(cube, new Vector3(Random.Range(-6, 6), 10, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

}
