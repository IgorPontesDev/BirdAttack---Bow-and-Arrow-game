using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour {
    
    public static float vidaAtualEstatica, vidaMaximaEstatica;
    public Image barraVidaUI;

    public float vidaMaxima;
    public float vidaAtual;
    public GameObject fimMsg;
    public bool flag;
    public Animator animaCasaExplode;
    private Vector3 posCasaInicial;
    public GameObject Casa, Vida;
    public GameObject [] ListaDeCasas;
    private GameObject BarraVidaPos;
	// Use this for initialization
	void Start () {
        
        BarraVidaPos = GameObject.Find("fundoVida");
        Vector3 posCasa = Camera.main.ScreenToWorldPoint(BarraVidaPos.transform.position);
        posCasaInicial = new Vector3(posCasa.x, posCasa.y-3, -1);
        pegaCasaAtual();
        Vida.SetActive(true);
        Casa.SetActive(true);
        animaCasaExplode = Casa.GetComponent<Animator>();
        flag = true;
        vidaMaxima = getCasaVidaAtual();
        vidaMaximaEstatica = vidaMaxima;
        vidaAtual = vidaMaxima;
        vidaAtualEstatica = vidaAtual;
	}
    
    // Update is called once per frame
    void Update () {        
        barraVidaUI.fillAmount = vidaAtual / vidaMaxima;
        vidaAtual = vidaAtualEstatica; //vidaatual estatica ta sendo modificada no script mantimentos pelo hit da bomba
        FimDeJogo();
        
	}
    private void FimDeJogo()
    {
        if (vidaAtual <= 0 && flag == true)
        {
            
            if (PlayerPrefs.GetInt("CompartilhouFb") == 1)
                PlayerPrefs.SetInt("PartidasJogadas", PlayerPrefs.GetInt("PartidasJogadas") + 1);
            GameObject.Find("PauseButton").GetComponent<Button>().enabled = false;
            GameObject.Find("CasaSomExplode").GetComponent<AudioSource>().Play();
            animaCasaExplode.SetTrigger("VidaZerada");
            Debug.Log("Explodindo");
            animaCasaExplode.SetBool("tomaDano", false);
                     
            Moeda.fimDeJogo = true;
            TelaFim.ativa = true;
            flag = false;
            Pontuacao.guardaRecord = true;
            StartCoroutine(KillOnAnimationEnd());
            
            int valorMorte = PlayerPrefs.GetInt("ContaMorte");
            valorMorte += 1;
            PlayerPrefs.SetInt("ContaMorte", valorMorte);
            Debug.Log(PlayerPrefs.GetInt("ContaMorte"));
            if (PlayerPrefs.GetInt("ContaMorte") >= 4)
            {
                PlayerPrefs.SetInt("ContaMorte", 0);
                StartCoroutine(ativaAnuncio());               
                
            }
             //ExibeMsgSeTiverUpgradeDisponivel(); está no Moeda.FimdeJogo

        }
    }
    
    private IEnumerator ativaAnuncio()
    {
        yield return new WaitForSeconds(0.4f);
        AdmobInterst.Morri = true;
    }
    
    
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.4f);
        Casa.SetActive(false);
        Vida.SetActive(false);
    }

    public string getCasaAtual()
    {
        return PlayerPrefs.GetString("CasaAtual");
    }
    public float getCasaVidaAtual()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "CasaVidaAtual");
    }

    public void pegaCasaAtual()
    {
        switch (PlayerPrefs.GetString("CasaAtual"))
        {
            case "Oca Simples":
                Casa = Instantiate(ListaDeCasas[0], posCasaInicial, Quaternion.identity);
                Passaro.casa = Casa;
                CocoBehavior.casa = Casa;
                break;
            case "Oca Artesanal":
                Casa = Instantiate(ListaDeCasas[1], posCasaInicial, Quaternion.identity);
                Passaro.casa = Casa;
                CocoBehavior.casa = Casa;
                break;
            case "Oca Fortificada":
                Casa = Instantiate(ListaDeCasas[2], posCasaInicial, Quaternion.identity);
                Passaro.casa = Casa;
                CocoBehavior.casa = Casa;
                break;
        }
    }
}
