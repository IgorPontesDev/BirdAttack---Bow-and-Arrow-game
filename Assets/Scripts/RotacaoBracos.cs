using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoBracos : MonoBehaviour {
    public GameObject PrefabArco;
    public Transform arco;
    public static bool atirando, largueiSemAtirar;

    public Transform conectorBracoFlecha;
    public GameObject antebraco;
    public Vector3 posInicial;
    Vector3 direcao;
    // Use this for initialization
    void Start() {
        largueiSemAtirar = false;
        atirando = false; //usado para o braco flecha e freezar rotacao do indio
        antebraco = GameObject.Find("AntebracoFlecha");
        posInicial = transform.localPosition;
      
        if(gameObject.name == "bracoArco")
            
        instanciaArcoAtual();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "bracoFlecha")
        {
            PrefabArco = GameObject.Find("bracoArco").transform.GetChild(0).gameObject;
            if (atirando)
            {
                transform.position = antebraco.transform.position;
            }
            if(!atirando)
            {
                if(!largueiSemAtirar)
                transform.localPosition = posInicial;
            }
        }
        if (gameObject.name == "bracoFlecha")
        {
            PrefabArco = GameObject.Find("bracoArco").transform.GetChild(0).gameObject;
            //  AjeitaBracoFlecha();
        }
        if (gameObject.name == "indio")
        {
            PrefabArco = GameObject.Find("bracoArco").transform.GetChild(0).gameObject;
            if (atirando)
            {
                
                this.transform.eulerAngles = PrefabArco.transform.eulerAngles;
                if (transform.eulerAngles.z < 360 && transform.eulerAngles.z > 80)
                {
                    if (transform.eulerAngles.z < 180 && transform.eulerAngles.z > 80)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 80);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }

                }
            }
        }

    }
    private void AjeitaBracoFlecha()
    {
        this.transform.eulerAngles = PrefabArco.transform.eulerAngles;
    }
    private void instanciaArcoAtual()
    {
        switch (PlayerPrefs.GetString("ArcoAtual"))
        {
            case "Arco de Bambu":
                PrefabArco = Resources.Load(path: "Arcos/ArcosInGame/ArcodeBambu") as GameObject;
                PrefabArco = Instantiate(PrefabArco, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                PrefabArco.transform.SetParent(transform);
                break;
            case "Arco Explosivo":                
                PrefabArco = Resources.Load(path: "Arcos/ArcosInGame/ArcoExplosivo") as GameObject;
                PrefabArco = Instantiate(PrefabArco, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                PrefabArco.transform.SetParent(transform);
                break;
            case "Arco Vampirico":
                PrefabArco = Resources.Load(path: "Arcos/ArcosInGame/ArcoVampirico") as GameObject;
                PrefabArco = Instantiate(PrefabArco, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                PrefabArco.transform.SetParent(transform);
                break;
            case "Arco Divisor":
                PrefabArco = Resources.Load(path: "Arcos/ArcosInGame/ArcoDivisor") as GameObject;
                PrefabArco = Instantiate(PrefabArco, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                PrefabArco.transform.SetParent(transform);
                break;
        }
        
        
    }
}
