using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassaroBehavior : MonoBehaviour {
    
    public static bool cair; // está sendo acessado no Passaro.cs
    public bool passaroBateuChao;
    public float speed;
    public static float speedStatic;
    public GameObject bloco;
    public GameObject canvasComScript;
    public Pontuacao scriptPontos;
	// Use this for initialization
	void Start () {
        passaroBateuChao = false;
        canvasComScript = GameObject.Find("CanvasGame");
        scriptPontos = canvasComScript.GetComponent<Pontuacao>();
        speed = 3;
        speedStatic = speed;
        
        cair = false;
        bloco = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (gameObject.name == "Clouds_H(Clone)")
            this.transform.position += new Vector3(-speed, 0, 0)*Time.deltaTime;

        if (gameObject.name == "passaro(Clone)" && PlayerPrefs.GetInt("TutoInGame") == 1)
        {
            speed = scriptPontos.speed;
           if(!passaroBateuChao)
                this.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;            
            
        }

        if (gameObject.name == "coruja(Clone)")
        {
            speed = scriptPontos.speed;
            if (!passaroBateuChao)
                this.transform.position += new Vector3(-speed-1, 0, 0) * Time.deltaTime;

        }

        if (transform.position.x < -26.63f)
        {
            Destroy(this.gameObject);
        }
	}
    public void aumentaVel()
    {
        speed += 1;
    }
   
}
