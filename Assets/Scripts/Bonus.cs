using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public float delay = 1f;
    public GameObject bonus;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", delay, 1);
    }

    // Update is called once per frame
    void Spawn()
    {
        bonus = Instantiate(bonus, new Vector3(Random.Range(-6, 6), 10, 0), Quaternion.identity);
        bonus.name = "Bonus";
    }

}
