using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour {

    Image foregroundImage;    

	// Use this for initialization
	void Start () {
	    foregroundImage = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    foregroundImage.fillAmount -= 0.0005f;
	}
}
