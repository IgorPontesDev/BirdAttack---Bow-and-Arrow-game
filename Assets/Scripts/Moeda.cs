using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Moeda : MonoBehaviour {
    public GameObject UpgradeDisponivel;
    public TextMeshProUGUI TxtUpgradeDisponivel;
    public static float recebeCarnes;
    public static bool fimDeJogo;
    private float carneTotal;
    public GameObject panelLoja;
    public GameObject carneGO;
    public TextMeshProUGUI carneTxt;
	// Use this for initialization
	void Start () {
        UpgradeDisponivel.SetActive(false);
        carneGO = GameObject.Find("CarnesTxt");

        carneTxt = carneGO.GetComponent<TextMeshProUGUI>();        
        
        carneTotal = getTotalCarnes();

        recebeCarnes = 0;        

        fimDeJogo = false;
    }
	
	// Update is called once per frame
	void Update () {
        mexeNoCarneTxt();
        if (fimDeJogo)
        {
            setTotalCarnes(recebeCarnes);
            ExibeMsgSeTiverUpgradeDisponivel();
            fimDeJogo = false;
        }
	}
    void ExibeMsgSeTiverUpgradeDisponivel()
    {
        float carnes = PlayerPrefs.GetFloat("carnes");
        float precoUpVida, precoUpForca, precoUpDelay;
        precoUpForca = PlayerPrefs.GetFloat(PlayerPrefs.GetString("ArcoAtual") + "PrecoForca");
        precoUpDelay = PlayerPrefs.GetFloat(PlayerPrefs.GetString("ArcoAtual") + "PrecoDelay");
        precoUpVida = PlayerPrefs.GetFloat(PlayerPrefs.GetString("CasaAtual") + "PrecoVida");
        Debug.Log("UpForca: " + PlayerPrefs.GetFloat(PlayerPrefs.GetString("ArcoAtual") + "UpForca"));

        if (carnes >= precoUpForca && PlayerPrefs.GetFloat(PlayerPrefs.GetString("ArcoAtual") + "UpForca") < 1)
        {
            TxtUpgradeDisponivel.text = "Evoluir Arco";
                UpgradeDisponivel.SetActive(true);
                PlayerPrefs.SetInt("TemUpgradeArco", 1);            
        }
        else
        {
            PlayerPrefs.SetInt("TemUpgradeArco", 0);
            if (carnes >= precoUpVida && PlayerPrefs.GetFloat(PlayerPrefs.GetString("CasaAtual") + "UpCasaVida") < 1)
            {
                TxtUpgradeDisponivel.text = "Evoluir Casa";
                UpgradeDisponivel.SetActive(true);
                PlayerPrefs.SetInt("TemUpgradeCasa", 1);
            }
            else
            {
                PlayerPrefs.SetInt("TemUpgradeCasa", 0);
            }
        }
        if(carnes >= precoUpDelay && PlayerPrefs.GetFloat(PlayerPrefs.GetString("ArcoAtual") + "UpDelay") < 1)
        {
            TxtUpgradeDisponivel.text = "Evoluir Arco";
            UpgradeDisponivel.SetActive(true);
            PlayerPrefs.SetInt("TemUpgradeArco", 1);
        } else
        {
            PlayerPrefs.SetInt("TemUpgradeArco", 0);
            if (carnes >= precoUpVida && PlayerPrefs.GetFloat(PlayerPrefs.GetString("CasaAtual") + "UpCasaVida") < 1)
            {
                TxtUpgradeDisponivel.text = "Evoluir Casa";
                UpgradeDisponivel.SetActive(true);
                PlayerPrefs.SetInt("TemUpgradeCasa", 1);
            }
            else
            {
                PlayerPrefs.SetInt("TemUpgradeCasa", 0);
            }
        }
            


    }
    private float getTotalCarnes()
    {
        carneTotal = PlayerPrefs.GetFloat("carnes");
        return carneTotal;
    }

    private void setTotalCarnes(float carneFarmadaInGame)
    {
        float carneTotal = getTotalCarnes();
        carneTotal += carneFarmadaInGame;
        PlayerPrefs.SetFloat("carnes", carneTotal);
        
    }
    private void mexeNoCarneTxt()
    {
            carneTxt.text = recebeCarnes.ToString();
             
    }
}
