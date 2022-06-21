using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour {
    public GameObject MenuInicio;
    public GameObject PanelMenu, PanelOptions;
    public Toggle CheckMusica, CheckEfeitos;
    public Slider SliderEfeitos, SliderMusica;
    public GameObject Loja;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	   
	}
    public void IniciaGame()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Loja>().CriaItens();
        SceneManager.LoadScene("Jogo");
        TelaFim.gameover = false;
    }
    public void Sair()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public void Shop()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        if (PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0)        
            TutorialInicial.estouNaLoja = true;

        Loja.SetActive(true);
        gameObject.GetComponent<Loja>().PegaPrefabArcoECasa();
        gameObject.GetComponent<Loja>().CriaItens();        
        gameObject.GetComponent<Loja>().OnClickLoja();
        gameObject.GetComponent<Loja>().AtualizaSetaLoja();

        MenuInicio.SetActive(false);
    }
    public void voltarOptions()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        PanelMenu.SetActive(true);
        PanelOptions.SetActive(false);
    }
    public void Options()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        PanelMenu.SetActive(false);
        PanelOptions.SetActive(true);
        if (PlayerPrefs.GetFloat("VolumeMusica") == 0)
            CheckMusica.isOn = false;
        else
            CheckMusica.isOn = true;
        if (PlayerPrefs.GetFloat("VolumeEfeitos") == 0)
            CheckEfeitos.isOn = false;
        else
            CheckEfeitos.isOn = true;

        SliderEfeitos.value = PlayerPrefs.GetFloat("VolumeEfeitos");
        SliderMusica.value = PlayerPrefs.GetFloat("VolumeMusica");
    }
    public void Salvar()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        if (CheckMusica.isOn == true)
        {
            PlayerPrefs.SetFloat("VolumeMusica", SliderMusica.value);            
        }
        else
        {
            PlayerPrefs.SetFloat("VolumeMusica", 0);
        }
        if (CheckEfeitos.isOn == true)
        {
            PlayerPrefs.SetFloat("VolumeEfeitos", SliderEfeitos.value);
        }
        else
        {
            PlayerPrefs.SetFloat("VolumeEfeitos", 0);
        }

        Sons.AtSom = true;
        SonsEfeitos.AtSom = true;

    }
}
