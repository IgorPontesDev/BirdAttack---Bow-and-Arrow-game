using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lancador : MonoBehaviour {
    public static bool PossoAtirar;
    //indio
    public static GameObject MaozinhaTutorial;
    public static bool SouIniciante;
    

    public GameObject indio;
    //facilitador de tiro
    public GameObject Origem;
    public GameObject Destino;
    bool permiteBolaTiroAndar;
    //
    public float forca, distanciaMax;
    public GameObject bala;
    Vector3 posicMouse;
    GameObject instanciaTemp;
    bool instanciou = false;
    public float delayTiro;
    private Image indicadorDelay;
    public float tempo;
    //Variaveis rotacao arco
    private Ray mouseRay1;
    private RaycastHit rayhit;
    private float posX;
    private float posY;
   
    // Corda do arco
    public GameObject bowString;
    private List<Vector3> bowStringPosition;
    LineRenderer bowStringLinerenderer;
    // position of the line renderers middle part 
    Vector3 stringPullout;
    Vector3 stringRestPosition = new Vector3(-0.44f, -0.06f, 2f);
    // to determine the string pullout
    public float length;
    float rotacaoZ;
    //rotacao da flecha variaveis
    public GameObject gameManager;
    public string ArcoAtual;
    bool puxarSom;
    bool precisoTestarArcoAtual;
    
    void Start () {
        if (PlayerPrefs.GetInt("TutoInGame") == 0 || PlayerPrefs.HasKey("TutoInGame") == false)
        {
            SouIniciante = true;
        } else
        {
            SouIniciante = false;
        }
        puxarSom = true;
        indio = GameObject.Find("indio");
        gameManager = GameObject.Find("gameManager");
        indicadorDelay = GameObject.Find("IndicadorDelay").GetComponent<Image>();
        ArcoAtual = getArcoAtual();        
        forca = 20 + (getForcaAtual() * 10);
        Habilidades.forca = forca;
        permiteBolaTiroAndar = true;
        delayTiro = getDelay();
        tempo = delayTiro;
        distanciaMax=  0.03f;


        // setup the line renderer representing the bowstring
        bowStringLinerenderer = bowString.AddComponent<LineRenderer>();
        bowStringLinerenderer.positionCount = 3;
        bowStringLinerenderer.startWidth=0.05f;
        bowStringLinerenderer.useWorldSpace = false;
        bowStringLinerenderer.material = Resources.Load("Materials/bowStringMaterial") as Material;
        bowStringPosition = new List<Vector3>();
        bowStringPosition.Add(new Vector3(-0.44f, 1.43f, 2f));
        bowStringPosition.Add(new Vector3(-0.44f, -0.06f, 2f));
        bowStringPosition.Add(new Vector3(-0.43f, -1.32f, 2f));
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, bowStringPosition[1]);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);        

        stringPullout = stringRestPosition;
        Origem = GameObject.Find("Origem");
        Destino = GameObject.Find("Destino");
        Origem.SetActive(false);
        Destino.SetActive(false);
        pegaFlechaAtual();
        
    }
	
	// Update is called once per frame
	void Update () {
        tempo += Time.deltaTime;
        indicadorDelay.fillAmount = (tempo/delayTiro);
        if (SouIniciante)
            TiroInicialTutorial();
        if (Input.GetMouseButton(0) && Habilidades.mouseEmCimaDoBotao == false && !SouIniciante)
        {
            RotacaoBracos.largueiSemAtirar = false;
            if (puxarSom)
            {
                GameObject.Find("indio").GetComponent<AudioSource>().Play();
                puxarSom = false;
            }
            RotacaoBracos.atirando = true;
            rotacionarArco();
            posicMouse = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);//pega as coordenadas do mouse no mundo
            //deixa visivel o circulo indicador de tiro
            if (permiteBolaTiroAndar)
            {
                Origem.SetActive(true);
                Origem.transform.position = posicMouse;
                
                permiteBolaTiroAndar = false;
            }
            Destino.SetActive(true);
                Destino.transform.position = posicMouse;
            
            //
            posicMouse.z = transform.position.z;
            
            if (instanciou == false)
            {
                instanciou = true;
                instanciaTemp = Instantiate(bala, posicMouse,transform.rotation) as GameObject;
                instanciaTemp.transform.position = new Vector3(instanciaTemp.transform.position.x, instanciaTemp.transform.position.y, -2);
                instanciaTemp.GetComponent<Rigidbody2D>().isKinematic = true;
                instanciaTemp.GetComponent<RotacaoDaFlecha>().setBow(gameObject);
                Habilidades.FlechaAtual = instanciaTemp;
                Habilidades.possoBifurcar = false;
            }
            if (Vector3.Distance(transform.position, posicMouse) < distanciaMax)
            {
                instanciaTemp.transform.position = posicMouse;
            } else
            {
                Vector3 lugarCorreto = transform.position + (posicMouse - transform.position).normalized * distanciaMax;
                instanciaTemp.transform.position = lugarCorreto;
            }

            // determine the arrow pullout
            length = posicMouse.magnitude / 3f;
            length = Mathf.Clamp(length, 0, 1);
            //set the bowstrings line renderer
            stringPullout = new Vector3(-(0.44f + length), -0.06f, 2f);
            instanciaTemp.transform.eulerAngles = transform.eulerAngles;
            
        }

        if(Input.GetMouseButtonUp(0) && tempo < delayTiro)
        {
            Origem.SetActive(false);
            Destino.SetActive(false);
            RotacaoBracos.atirando = false;
            RotacaoBracos.largueiSemAtirar = true;
            permiteBolaTiroAndar = true;
        }

        if(Input.GetMouseButtonUp(0) && tempo > delayTiro && instanciou == true && !SouIniciante)
        {
            Habilidades.possoBifurcar = true;
            if (PlayerPrefs.HasKey("NuncaAtirouArcoExplosivo1") == false && ArcoAtual == "Arco Explosivo")
            {
                GameObject.Find("Tutorial").GetComponent<Image>().enabled = true;
                StartCoroutine(esperaSeg());
                PlayerPrefs.SetInt("NuncaAtirouArcoExplosivo1",1);
            }
            GameObject.Find("bracoArco").GetComponent<AudioSource>().Play();
            puxarSom = true;
            permiteBolaTiroAndar = true;
            indicadorDelay.fillAmount = 1;
            RotacaoBracos.atirando = false;
            Vector3 direcao = Origem.transform.position - Destino.transform.position;
            
            //setando para a flecha nao ir mt forte
            direcao = direcao.normalized;
            //
            //Debug.Log("x: " + direcao.x + " y: " + direcao.y + " z: " + direcao.z);
            instanciaTemp.GetComponent<Rigidbody2D>().isKinematic = false;
            instanciaTemp.transform.parent = gameManager.transform;
            //80 graus: new Vector3(0.09053572f, 0.9958932f,0)
            // 0 graus: new Vector3(0.9993806f, 0.03518943f, 0)
            //Adicionando tiro da flecha na direcao correta na brute force
             if (direcao.y < 0 || direcao.x < 0)
             {
                 if (direcao.x < 0) //se o angulo for maior q 90 graus direcao.x fica negativo
                 {//
                     instanciaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.09053572f, 0.9958932f, 0)* forca, ForceMode2D.Impulse);
                 }
                 else
                 {
                     if (direcao.y < 0) //se o angulo for menor que 0 graus direcao.y fica negativo                        
                         instanciaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.9993806f, 0.03518943f, 0) * forca, ForceMode2D.Impulse);

                 }
             }
             if(direcao.x > 0 && direcao.y > 0)                
            instanciaTemp.GetComponent<Rigidbody2D>().AddForce(direcao* forca, ForceMode2D.Impulse);
            Habilidades.Direcao = direcao;
            
            
            instanciou = false;
            instanciaTemp = null;
            stringPullout = stringRestPosition;
            tempo = 0; // zera o tempo pro delay contar
            
            Flecha.PodeDestruir = true;
            Origem.SetActive(false);
            Destino.SetActive(false);
        }
        drawBowString();

        
    }

    public void rotacionarArco()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Origem.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (180 + angle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        if (Origem != null)
        {
            
            if (transform.eulerAngles.z < 360 && transform.eulerAngles.z > 80)
            {
                
                if (transform.eulerAngles.z < 180 && transform.eulerAngles.z > 80)
                {
                    transform.eulerAngles = new Vector3(0, 0, 80);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

            }
            
            
        }
        Origem.transform.eulerAngles = transform.eulerAngles;

    }
    public void rotacionarArcoTutorialInGame()
    {
        Vector2 direction = MaozinhaTutorial.transform.position - Origem.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = (180 + angle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        if (Origem != null)
        {

            if (transform.eulerAngles.z < 360 && transform.eulerAngles.z > 80)
            {

                if (transform.eulerAngles.z < 180 && transform.eulerAngles.z > 80)
                {
                    transform.eulerAngles = new Vector3(0, 0, 80);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

            }


        }
        Origem.transform.eulerAngles = transform.eulerAngles;

    }
    public void drawBowString()
    {
        bowStringLinerenderer = bowString.GetComponent<LineRenderer>();
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, stringPullout);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
    }
   
    //Getters e setters
    public string getArcoAtual()
    {
        return PlayerPrefs.GetString("ArcoAtual");
    }
    public float getForcaAtual()
    {
        return PlayerPrefs.GetFloat(ArcoAtual + "UpForca");
    }

    public float getDelay()
    {
        return PlayerPrefs.GetFloat(ArcoAtual + "Delay");
    }
    public void pegaFlechaAtual()
    {
        switch (ArcoAtual) {
            case "Arco de Bambu":
            bala = Resources.Load(path: "Arcos/Flechas/Flecha") as GameObject;
                break;
            case "Arco Explosivo":
                bala = Resources.Load(path: "Arcos/Flechas/FlechaExplosiva") as GameObject;
                break;
            case "Arco Vampirico":
                bala = Resources.Load(path: "Arcos/Flechas/Flecha") as GameObject;
                break;
            case "Arco Divisor":
                bala = Resources.Load(path: "Arcos/Flechas/Flecha") as GameObject;
                break;

        }
    }
    //
    private IEnumerator esperaSeg()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
    }

    public void TiroInicialTutorial()
    {        
        Vector3 posicaoMao = new Vector3(MaozinhaTutorial.transform.position.x, MaozinhaTutorial.transform.position.y, MaozinhaTutorial.transform.position.z);
        Debug.Log(posicaoMao);
        if (posicaoMao.z <= 0.2f)
        {
            if (puxarSom)
            {
                GameObject.Find("indio").GetComponent<AudioSource>().Play();
                puxarSom = false;
            }
            RotacaoBracos.atirando = true;
            rotacionarArcoTutorialInGame();

            //deixa visivel o circulo indicador de tiro
            if (permiteBolaTiroAndar)
            {
                Origem.SetActive(true);
                //Origem.transform.position = posicaoMao;

                permiteBolaTiroAndar = false;
            }
            Destino.SetActive(true);
            Destino.transform.position = MaozinhaTutorial.transform.position;

            //

            posicaoMao.z = transform.position.z;

            if (instanciou == false)
            {
                instanciou = true;
                instanciaTemp = Instantiate(bala, MaozinhaTutorial.transform.position, transform.rotation) as GameObject;
                instanciaTemp.transform.position = new Vector3(instanciaTemp.transform.position.x, instanciaTemp.transform.position.y, -2);
                instanciaTemp.GetComponent<Rigidbody2D>().isKinematic = true;
                instanciaTemp.GetComponent<RotacaoDaFlecha>().setBow(gameObject);
                Habilidades.FlechaAtual = instanciaTemp;
                Habilidades.possoBifurcar = false;
            }
            if (Vector3.Distance(transform.position, MaozinhaTutorial.transform.position) < distanciaMax)
            {
                instanciaTemp.transform.position = MaozinhaTutorial.transform.position;
            }
            else
            {
                Vector3 lugarCorreto = transform.position + (MaozinhaTutorial.transform.position - transform.position).normalized * distanciaMax;
                instanciaTemp.transform.position = lugarCorreto;
            }

            // determine the arrow pullout
            length = MaozinhaTutorial.transform.position.magnitude / 3f;
            length = Mathf.Clamp(length, 0, 1);
            //set the bowstrings line renderer
            stringPullout = new Vector3(-(0.44f + length), -0.06f, 2f);
            instanciaTemp.transform.eulerAngles = transform.eulerAngles;
        }

        if (posicaoMao.z > 0.2f)
        {
            Habilidades.possoBifurcar = true;
           
            GameObject.Find("bracoArco").GetComponent<AudioSource>().Play();
            puxarSom = true;
            permiteBolaTiroAndar = true;
            indicadorDelay.fillAmount = 1;
            RotacaoBracos.atirando = false;
            Vector3 direcao = Origem.transform.position - Destino.transform.position;

            //setando para a flecha nao ir mt forte
            direcao = direcao.normalized;
            //
            //Debug.Log("x: " + direcao.x + " y: " + direcao.y + " z: " + direcao.z);
            instanciaTemp.GetComponent<Rigidbody2D>().isKinematic = false;
            instanciaTemp.transform.parent = gameManager.transform;
            //80 graus: new Vector3(0.09053572f, 0.9958932f,0)
            // 0 graus: new Vector3(0.9993806f, 0.03518943f, 0)
            //Adicionando tiro da flecha na direcao correta na brute force
            if (direcao.y < 0 || direcao.x < 0)
            {
                if (direcao.x < 0) //se o angulo for maior q 90 graus direcao.x fica negativo
                {//
                    instanciaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.09053572f, 0.9958932f, 0) * forca, ForceMode2D.Impulse);
                }
                else
                {
                    if (direcao.y < 0) //se o angulo for menor que 0 graus direcao.y fica negativo                        
                        instanciaTemp.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.9993806f, 0.03518943f, 0) * forca, ForceMode2D.Impulse);

                }
            }
            if (direcao.x > 0 && direcao.y > 0)
                instanciaTemp.GetComponent<Rigidbody2D>().AddForce(direcao * forca, ForceMode2D.Impulse);
            Habilidades.Direcao = direcao;


            instanciou = false;
            instanciaTemp = null;
            stringPullout = stringRestPosition;
            tempo = 0; // zera o tempo pro delay contar

            Flecha.PodeDestruir = true;
            Origem.SetActive(false);
            Destino.SetActive(false);
        }
    }
    
}
