using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaCasaHit : MonoBehaviour {
    private Animator casaAnimator;
    public static bool hitaCasa;
    
    // Use this for initialization
    void Start () {
        
        hitaCasa = false; // está sendo alterado pelo segueBomba
        casaAnimator = gameObject.GetComponent<Animator>();
        Mantimentos.Casa = gameObject;
        

}
	
	// Update is called once per frame
	void Update () {
        
            if (hitaCasa)
                casaTomaDano();
            if (!hitaCasa)
                casaParaDeTomarDano();        
    }
    void casaTomaDano()
    {
            casaAnimator.SetBool("tomaDano", true);       
        
    }
    void casaParaDeTomarDano()
    {
        casaAnimator.SetBool("tomaDano", false);

    }
    

}
