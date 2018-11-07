using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cubes : MonoBehaviour {
    public float delay = 1f;
    public GameObject cube;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", delay, 1);
	}
	
	// Update is called once per frame
	void Spawn () {
        Instantiate(cube, new Vector3(Random.Range(-6, 6), 10, 0),Quaternion.identity);
        cube.name = "Cube";
	}

}
