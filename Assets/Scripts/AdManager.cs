using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    private bool adShowed;

	// Use this for initialization
	void Start () {
        //Android ads
        Advertisement.Initialize("2949874", false);
    }
	
	// Update is called once per frame
	void Update () {
	    if (!CharacterController2D.alive && !adShowed)
	    {
	        int random = Random.Range(1, 4);
            if (random == 2)
            {
                if (Advertisement.IsReady("banner"))
                {
                    Advertisement.Show("banner");
                }
            }                
                    
	        adShowed = true;
	    }

	    if (CharacterController2D.alive)
	    {
            adShowed = false;
        }
	}
}
