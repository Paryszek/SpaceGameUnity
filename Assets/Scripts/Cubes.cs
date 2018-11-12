using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;

	void Start () {
        InvokeRepeating("Spawn", 1f, 0.7f);
    }
	
	void Spawn () {
		if (CharacterController2D.gameOver.text != "Game Over") 
		{
			Instantiate(cube, new Vector3(Random.Range(-5, 3), 15, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

}
