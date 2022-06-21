using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInGame : MonoBehaviour {
    [SerializeField] GameObject prefabMaozinha, maoInstanciada, Origem, Destino, EntendiBtn;
    
    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteKey("TutoInGame");
        if(PlayerPrefs.GetInt("TutoInGame") == 0 || PlayerPrefs.HasKey("TutoInGame") == false)
        {
            maoInstanciada= Instantiate(prefabMaozinha, GameObject.Find("pedra1").transform.position, Quaternion.identity);
            maoInstanciada.transform.SetParent(GameObject.Find("Cenario").transform);
            Lancador.MaozinhaTutorial = maoInstanciada;
            EntendiBtn.SetActive(true);
        } else
        {
            EntendiBtn.SetActive(false);
        }
	}
	
	public void ClickEntendiBtn()
    {
        PlayerPrefs.SetInt("TutoInGame", 1);
        EntendiBtn.SetActive(false);
        Lancador.SouIniciante = false;
        Destroy(maoInstanciada.gameObject);
    }
}
