using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sons : MonoBehaviour {
    public static bool AtSom;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("VolumeMusica") == false)
        {
            PlayerPrefs.SetFloat("VolumeMusica", 1);
        }
        if (PlayerPrefs.HasKey("VolumeEfeitos") == false)
        {
            PlayerPrefs.SetFloat("VolumeEfeitos", 1);
        }
        if (gameObject.GetComponent<AudioSource>().volume != PlayerPrefs.GetFloat("VolumeMusica"))
        {
            //está com musica do menu
            float volume = PlayerPrefs.GetFloat("VolumeMusica");
            gameObject.GetComponent<AudioSource>().volume = volume;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (AtSom)
            AttSom();
        
    }
    public void SomBotao()
    {
        GetComponent<AudioSource>().Play();
    }
    public void AttSom()
    {
        if (gameObject.GetComponent<AudioSource>().volume != PlayerPrefs.GetFloat("VolumeMusica"))
        {
            //está com musica do menu
            float volume = PlayerPrefs.GetFloat("VolumeMusica");
            gameObject.GetComponent<AudioSource>().volume = volume;
        }
        AtSom = false;
    }
}
