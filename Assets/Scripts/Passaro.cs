using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passaro : MonoBehaviour {
    
    public static GameObject casa;
    // Use this for initialization
    public GameObject Bomba;
    //public static bool PossoLiberarBomba;    
    bool podeCair;
    public static bool podeCairStatic;
    private Animator animatorPombo;
    private Rigidbody2D rbPombo;
    bool bateuChao;
    public float speedCair;
    private string ArcoAtual;
    void Start () {
        ArcoAtual = PlayerPrefs.GetString("ArcoAtual");
        bateuChao = false;
        speedCair = 3;
        
        podeCair = false;
        podeCairStatic = false;
        animatorPombo = this.gameObject.GetComponent<Animator>();
        //PossoLiberarBomba = true;        
        Bomba = this.gameObject.transform.GetChild(0).gameObject;
        
    }
    private void Update()
    {
        
        if (this.transform.position.x <= casa.transform.position.x &&  !podeCair && casa!=null)
        {
                    if (casa != null && Bomba != null)
                    {
                Bomba.SetActive(true);
                if(gameObject.name == "passaro(Clone)")
                        animatorPombo.SetBool("PomboNormal", true);
                if (gameObject.name == "coruja(Clone)")
                    animatorPombo.SetBool("CorujaNormal", true);
                CocoBehavior.PodeSoltarBomba = true;
                       // PossoLiberarBomba = false;

                        SegueBomba.BombaEstatica = Bomba;
                        SegueBomba.possoSeguir = true;
                    }
                
            

        }

        if (podeCair)
        {
           // PossoLiberarBomba = false;
            Caindo();
           
        }
        

    }
    //1.622337 valo explosao Flecha scale
    // Update is called once per frame
    private void OnCollisionEnter2D (Collision2D collision)
    {
        float speed = gameObject.GetComponent<PassaroBehavior>().speed;
        
        if (collision.gameObject.CompareTag("tiro"))
        {
            
            
            GameObject.Find("SpawnEnemy").GetComponent<AudioSource>().Play();
            //atribui gold
            //PossoLiberarBomba = false;
            if (gameObject.name == "passaro(Clone)")
            {
                float qtdMoedas = 2f * speed;
                Moeda.recebeCarnes += 2f * speed;
                gameObject.GetComponent<PopUpText>().ProduceText(qtdMoedas);
            }
            if (gameObject.name == "coruja(Clone)")
            {
                float qtdMoedas = 3f * speed;
                Moeda.recebeCarnes += 3f * speed;
                gameObject.GetComponent<PopUpText>().ProduceText(qtdMoedas);
            }

            //


            // seta o trigger para não atingir a flecha dps de morto
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            //
            Pontuacao.score++;           
            
            Destroy(collision.gameObject);
            if (gameObject.name == "passaro(Clone)")
            {
                animatorPombo.SetBool("PomboGiraMorrer", true);
                animatorPombo.SetBool("PomboGirando", true);
            }
            else
            {
                if (gameObject.name == "coruja(Clone)")
                {
                    animatorPombo.SetBool("CorujaGiraMorrer", true);
                    animatorPombo.SetBool("CorujaGirando", true);
                }
            }
            podeCair = true;
            podeCairStatic = podeCair;
            if(ArcoAtual == "Arco Vampirico")
            {
                //SkillEncherVida
                float vidaCasaMax = BarraVida.vidaMaximaEstatica;
                float vidaCasaAtual = BarraVida.vidaAtualEstatica;
                float vidaCasaFinal;
                vidaCasaFinal = (vidaCasaMax * 0.05f) + vidaCasaAtual;
                if (vidaCasaFinal > BarraVida.vidaMaximaEstatica)
                    vidaCasaFinal = BarraVida.vidaMaximaEstatica;
                BarraVida.vidaAtualEstatica = vidaCasaFinal;
                Debug.Log("Vida enchida: " + vidaCasaFinal);
                vidaCasaFinal = 0;
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("cabecaFlecha") && Flecha.podeExplodirFlecha)
        {
            GameObject.Find("SpawnEnemy").GetComponent<AudioSource>().Play();
            // PossoLiberarBomba = false;
            float speed = gameObject.GetComponent<PassaroBehavior>().speed;
            Destroy(collision.gameObject);
            animatorPombo.SetBool("FlechaExplodiu", true);
            
            StartCoroutine(KillOnAnimationEnd());
            Pontuacao.score++;
            Moeda.recebeCarnes += 2f * speed;
        }
        if (collision.gameObject.CompareTag("chao"))
        {
            this.gameObject.GetComponent<PassaroBehavior>().passaroBateuChao = true;
            bateuChao = true;
            animatorPombo.SetBool("BateuChao", true);
            StartCoroutine(KillOnAnimationEnd());
        }
    }

    public void Caindo()
    {
        if (!bateuChao)
        {
            this.transform.position += new Vector3(speedCair, 0, 0) * Time.deltaTime;
            this.transform.position += new Vector3(0, -speedCair - 2, 0) * Time.deltaTime;
        }
    }
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
   
}
