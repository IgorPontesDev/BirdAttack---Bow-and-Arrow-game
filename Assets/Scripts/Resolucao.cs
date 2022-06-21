using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolucao : MonoBehaviour {
    public float A, B;
    public float screenwidth, screenheight;
	// Use this for initialization
	void Start () {
        A = transform.localScale.x;
        screenwidth = Screen.width;
        screenheight = Screen.height;
        transform.localScale = new Vector3((A * screenwidth / screenheight), transform.localScale.y, transform.localScale.z);
    }
	
	// Update is called once per frame
	void Update () {
        
        screenwidth = Screen.width;
        screenheight = Screen.height;
        transform.localScale = new Vector3((screenwidth / screenheight), transform.localScale.y, transform.localScale.z);
	}
}
