using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public GameObject bonus;

    void Start()
    {
        InvokeRepeating("Spawn", 1f, 4f);
    }

    void Spawn()
    {
        if (CharacterController2D.gameOver.text != "Game Over") 
		{
            bonus = Instantiate(bonus, new Vector3(Random.Range(-6, 6), 10, 0), Quaternion.identity);
            bonus.name = "Bonus";
        }
    }

}
