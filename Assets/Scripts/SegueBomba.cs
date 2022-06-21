using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegueBomba : MonoBehaviour {
    public static GameObject BombaEstatica;

    public GameObject Bomba;
    public static bool possoSeguir;
    public Animator animatorBomba;
    // Use this for initialzation
    void Start () {

        animatorBomba = this.gameObject.GetComponent<Animator>();
        possoSeguir = false;
        Bomba = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if(Bomba!=null && possoSeguir==false)
        {
            animatorBomba.SetBool("Explode", false);
            AnimaCasaHit.hitaCasa = false;
        }
        if(possoSeguir)
        {
            Bomba = BombaEstatica;            
            if (Bomba != null)
            {
                seguirBomba(Bomba);
            }
        }
        if(Bomba == null)
        {
            ativaExplosao();
        }
	}
    public void seguirBomba(GameObject alvoBomba)
    {
        transform.position = alvoBomba.transform.position;
    }
    public void ativaExplosao()
    {
        animatorBomba.SetBool("Explode", true);
        if(BarraVida.vidaAtualEstatica >0)
            AnimaCasaHit.hitaCasa = true;
        
        Bomba = this.gameObject;//bomba recebe esse gameobject para nao chamar novamente essa função
        possoSeguir = false;
        
    }
}
