using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesPrefab : MonoBehaviour {
    public Loja LojaDeItensScript;
	// Use this for initialization
	void Start () {
        LojaDeItensScript = GameObject.Find("CanvasStartMenu").GetComponent<Loja>();
        
    }
    // Update is called once per frame
    public void OnClickUpForca()
    {
        LojaDeItensScript.upgradeForca();
        
    }
    public void OnClickUpDelay()
    {
        LojaDeItensScript.upgradeDelay();
        
    }
    public void OnClickUpVida()
    {
        LojaDeItensScript.upgradeVida();
        
    }
}
