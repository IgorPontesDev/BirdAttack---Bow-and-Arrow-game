using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour {
    public bool mouseOver = false;
	
    void OnMouseOver()
    {
        mouseOver = true;
        
    }
    void OnMouseExit()
    {
        mouseOver = false;        
    }
}
