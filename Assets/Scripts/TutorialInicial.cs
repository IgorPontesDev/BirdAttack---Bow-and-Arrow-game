using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInicial : MonoBehaviour {
    [SerializeField] GameObject seta, LojaButton, UpgradeButton;
    public static bool estouNoMenu, estouNaLoja, estouNosUpgrades, concluiuTutorial;
    public static Vector3 positionBtnForca;
    // botoesParaDesativar
    [SerializeField] GameObject btnPlay, btnOptions ,btnCasas, btnVoltar, btnStatus ,btnProx,btnAnterior;
    public static GameObject btnUpDelay;
    //GameObject PanelMenu;
    // Use this for initialization
    void Start()
    {
        
     /*   PlayerPrefs.DeleteKey("FezTutoInicial");
        //PanelMenu = GameObject.Find("PanelMenu");        
        estouNoMenu = true;
        estouNaLoja = false;
        estouNosUpgrades = false;
        if(PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0) { 
        if (LojaButton.activeSelf == true && estouNoMenu)
        {
                btnPlay.GetComponent<Button>().enabled = false;
                btnPlay.GetComponent<Image>().enabled = false;
                btnOptions.GetComponent<Button>().enabled = false;
                btnOptions.GetComponent<Image>().enabled = false;
                btnProx.GetComponent<Button>().enabled = false;
                btnProx.GetComponent<Image>().enabled = false;
                btnAnterior.GetComponent<Button>().enabled = false;
                btnAnterior.GetComponent<Image>().enabled = false;
                seta = Instantiate(seta, new Vector3(LojaButton.transform.position.x - 190, LojaButton.transform.position.y, LojaButton.transform.position.z), Quaternion.identity);
            seta.transform.SetParent(GameObject.Find("CanvasStartMenu").transform);

        }
        }*/
	}
	
	// Update is called once per frame
	void Update () {
        
/*
        if (PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0) {
            if (estouNaLoja)
            {
                btnCasas.GetComponent<Button>().enabled = false;
                btnCasas.GetComponent<Image>().enabled = false;
                btnVoltar.GetComponent<Button>().enabled = false;
                btnVoltar.GetComponent<Image>().enabled = false;
                seta.transform.position = new Vector3(UpgradeButton.transform.position.x - 210, UpgradeButton.transform.position.y + 8, UpgradeButton.transform.position.z);
                estouNaLoja = false;
            }
            if (estouNosUpgrades)
            {
                
                btnStatus.GetComponent<Button>().enabled = false;
                btnStatus.GetComponent<Image>().enabled = false;
                seta.transform.position = new Vector3(positionBtnForca.x - 160, positionBtnForca.y, positionBtnForca.z);
                estouNosUpgrades = false;
            }
            if (concluiuTutorial)
            {
                btnPlay.GetComponent<Button>().enabled = true;
                btnPlay.GetComponent<Image>().enabled = true;
                btnOptions.GetComponent<Button>().enabled = true;
                btnOptions.GetComponent<Image>().enabled = true;
                btnProx.GetComponent<Button>().enabled = true;
                btnProx.GetComponent<Image>().enabled = true;
                btnAnterior.GetComponent<Button>().enabled = true;
                btnAnterior.GetComponent<Image>().enabled = true;
                btnCasas.GetComponent<Button>().enabled = true;
                btnCasas.GetComponent<Image>().enabled = true;
                btnVoltar.GetComponent<Button>().enabled = true;
                btnVoltar.GetComponent<Image>().enabled = true;
                btnStatus.GetComponent<Button>().enabled = true;
                btnStatus.GetComponent<Image>().enabled = true;
                Destroy(seta.gameObject);
                concluiuTutorial = false;
                PlayerPrefs.SetInt("FezTutoInicial", 1);
                
            }
            
           
        }
        */
	}
}
