using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoBehavior : MonoBehaviour {
    public static GameObject casa;
    public GameObject passaro;
    public float posicaoParaSoltar;
    public bool flag;
    public static bool PodeSoltarBomba;
    public Rigidbody2D rb;
    public Passaro PomboScript;
	// Use this for initialization
	void Start () {
        
        flag = false;
        rb.gravityScale = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (PodeSoltarBomba)
        {
                
                transform.SetParent(casa.transform);
                flag = true;
                rb.gravityScale = 1;            
        }
    }
}
