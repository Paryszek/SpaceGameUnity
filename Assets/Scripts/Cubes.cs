using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public GameObject cube;
    private float screenHalfWidth;

    void Start () {
        InvokeRepeating("Spawn", 2f, 1.5f);
        InitScreenWidth();        
    }
	
	void Spawn () {
		if (CharacterController2D.alive)
		{
		    var time = CharacterController2D.time;
            if (Mathf.RoundToInt(time) == 10)
            {
                InvokeRepeating("Spawn", 2f, 1.25f);
            }

            if (Mathf.RoundToInt(time) == 250)
            {
                InvokeRepeating("Spawn", 0f, 1f);
            }
           
            if (Mathf.RoundToInt(time) == 500)
            {
                InvokeRepeating("Spawn", 0f, 0.95f);
            }


            cube = Instantiate(cube, new Vector3(Random.Range(-screenHalfWidth - 1, screenHalfWidth + 1), 15, 0), Quaternion.identity);
			cube.name = "Cube";
		}
	}

    void InitScreenWidth()
    {
        float halfCubeWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfCubeWidth;
    }

}
