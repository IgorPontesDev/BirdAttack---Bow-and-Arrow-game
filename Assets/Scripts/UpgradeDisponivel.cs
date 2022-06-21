using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisponivel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("TemUpgradeArco") == 1 && PlayerPrefs.GetInt("ClicouEvoluir") == 1)
        {
            GetComponent<MenuInicial>().Shop();
            GetComponent<Loja>().OnClickUpgrade();
            Debug.Log("AQUI");
            PlayerPrefs.SetInt("TemUpgradeArco",0);
            PlayerPrefs.SetInt("ClicouEvoluir", 0);
        } else
        {
            if(PlayerPrefs.GetInt("TemUpgradeCasa") == 1 && PlayerPrefs.GetInt("ClicouEvoluir") == 1)
            {
                Debug.Log("AQUI 2");
                GetComponent<MenuInicial>().Shop();
                GetComponent<Loja>().OnClickCasa();
                GetComponent<Loja>().OnClickUpgrade();
                PlayerPrefs.SetInt("TemUpgradeCasa", 0);
                PlayerPrefs.SetInt("ClicouEvoluir", 0);
            }
        }
    }
    
}
