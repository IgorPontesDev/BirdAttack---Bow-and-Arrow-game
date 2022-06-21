using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Loja : MonoBehaviour
{
    public GameObject btnAntes, btnDepois;
    public GameObject PanelMsgErro;
    public GameObject [] ListaPrefabArco;
    public GameObject[] ListaPrefabCasa;
    public GameObject MenuInicial;
    public GameObject LojaItens;
    public GameObject ItensArcos;
    public GameObject ItensCasas;
    private float carneTotal;
    public TextMeshProUGUI qtdCarneTxt;
    public TextMeshProUGUI nomeItemAtual;
    public Image EstrelasForca;
    public Image EstrelasDelay;
    public Image EstrelasVida;
    private float upgradeAtualForca, upgradeAtualDelay;
    private float upgradeAtualCasa;
    private float precoAtualUpgradeForca, precoAtualUpgradeDelay;
    private float precoAtualUpgradeCasaVida;
    public GameObject UpgradePanelArcos, DescricaoItemPanelArcos, PanelComprarArcos;
    public GameObject UpgradePanelCasas, DescricaoItemPanelCasas, PanelComprarCasas;
    public GameObject btnUpgrade, btnStatus,botaoComprar, botaoEquipar;
    public TextMeshProUGUI txtVidaCasaAtual;
    public bool reseta;
    public float multiplicadorStatus;
    public GameObject IndicaAtualLugarCasas, IndicaAtualLugarArcos;
    // Use this for initialization
    private int posAtualListaArcos, posAtualListaCasas;
    private int TamanhoMaxListaArcos, TamanhoMaxListaCasas;
    private string nomeDoArcoQueEstouVendo, nomeDaCasaQueEstouVendo;
    bool EstouNoUpgrade, EstouNaDescricao;
    private float PrecoUpgradeArcoFinalForca, PrecoUpgradeArcoFinalDelay, PrecoUpgradeCasaFinalVida;
    void Start()
    {
        EstouNaDescricao = true;
        EstouNoUpgrade = false;
        
        TamanhoMaxListaArcos = 3;
        TamanhoMaxListaCasas = 2;
        posAtualListaArcos = 0;
        posAtualListaCasas = 0;
        if(PlayerPrefs.HasKey("UBAADDDDDSS") == false)
        {
            ResetaEdaCarne();
            PlayerPrefs.SetInt("UBAADDDDDSS", 1);
        }
        //PlayerPrefs.DeleteKey("Att02BirdAttack");
       Atualizacoes();
        

    }
    // Update is called once per frame
    void Update()
    {
        
        if (LojaItens.activeSelf == true)
        {
            StartaItensLoja();
            if (reseta)
            {
                ResetaEdaCarne();
            }            
            
            
            AtualizaListaDeArcos();
            AtualizaListaDeCasas();
            atualizaItem();
        }


    }
    void ResetaEdaCarne()
    {
        setTotalCarnes(35);

        PlayerPrefs.SetInt("TemUpgradeCasa", 0);
        PlayerPrefs.SetInt("TemUpgradeArco", 0);
        PlayerPrefs.SetInt("ClicouEvoluir", 0);

        PlayerPrefs.DeleteKey("CasaAtual");
        PlayerPrefs.DeleteKey("ArcoAtual");

        PlayerPrefs.SetInt("Arco Vampirico", 0);
        PlayerPrefs.SetInt("Arco Explosivo", 0);
        PlayerPrefs.SetInt("Arco Divisor", 0);

        PlayerPrefs.SetInt("Oca Artesanal", 0);
        PlayerPrefs.SetInt("Oca Fortificada", 0);

        setArcoAtual("Arco Explosivo");
        setPrecoUpgradeDelay(0);
        setTotalUpgradeDelay(0f);
        setPrecoUpgradeForca(0);
        setTotalUpgradeForca(0f);
        setaArcoExplosivo();

        setArcoAtual("Arco Vampirico");
        setPrecoUpgradeDelay(0);
        setTotalUpgradeDelay(0f);
        setPrecoUpgradeForca(0);
        setTotalUpgradeForca(0f);
        setaArcoVampirico();

        setArcoAtual("Arco Divisor");
        setPrecoUpgradeDelay(0);
        setTotalUpgradeDelay(0f);
        setPrecoUpgradeForca(0);
        setTotalUpgradeForca(0f);
        setaArcoDivisor();

        setArcoAtual("Arco de Bambu");
        setPrecoUpgradeDelay(0);
        setTotalUpgradeDelay(0f);
        setPrecoUpgradeForca(0);
        setTotalUpgradeForca(0f);
        setaArcoDeBambu();

        setCasaAtual("Oca Artesanal");
        setPrecoUpgradeCasaVida(0);
        setTotalUpgradeCasaVida(0);
        setCasaVidaBase(0);
        setCasaVidaAtual(0);
        setPrecoCasaVidaInicial(125);
        setPrecoUpgradeCasaVida(125);
        setaOcaArtesanal();

        setCasaAtual("Oca Fortificada");
        setPrecoUpgradeCasaVida(0);
        setTotalUpgradeCasaVida(0);
        setCasaVidaBase(0);
        setCasaVidaAtual(0);
        setPrecoCasaVidaInicial(125);
        setPrecoUpgradeCasaVida(125);
        setaOcaFortificada();

        setCasaAtual("Oca Simples");
        setPrecoUpgradeCasaVida(0);
        setTotalUpgradeCasaVida(0);

        setCasaVidaBase(0);
        setCasaVidaAtual(0);
        setPrecoCasaVidaInicial(125);
        setPrecoUpgradeCasaVida(125);
        setaOcaSimples();
        
        //ResetandoArco Explosivo

    }
    //FuncaoStart
    public void StartaItensLoja()
    {
        //Pega os valores atuais dos upgrade e carnes já salvos
        upgradeAtualForca = getTotalUpgradeForca();
        upgradeAtualDelay = getTotalUpgradeDelay();
        upgradeAtualCasa = getTotalUpgradeCasaVida();
        qtdCarneTxt.text = getTotalCarnes().ToString();
        //        
    }
    //
    public void PegaPrefabArcoECasa()
    {
        // funcao so e chamada quando abre loja
        if (LojaItens.activeSelf == true)
        {
            nomeDaCasaQueEstouVendo = getCasaAtual();
            nomeDoArcoQueEstouVendo = getArcoAtual();
            GameObject fundoItens = GameObject.Find("FundoItens"); ;
            if (IndicaAtualLugarArcos.activeSelf == true)
            {
                GameObject prefabArco;
                switch (getArcoAtual())
                {

                    case "Arco de Bambu":                        
                        prefabArco = ListaPrefabArco[0]; //posicao dele na lista
                        if(ItensArcos!=null)
                        Destroy(ItensArcos.gameObject);
                        ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                        ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                        ItensArcos.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                        UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
                        break;
                    case "Arco Explosivo":
                        posAtualListaArcos = 1;
                        prefabArco = ListaPrefabArco[1]; //posicao dele na lista
                        if (ItensArcos != null)
                            Destroy(ItensArcos.gameObject);
                        ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                        ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                        ItensArcos.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                        UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
                        PanelComprarArcos = ItensArcos.transform.GetChild(4).transform.gameObject;                        
                        break;
                    case "Arco Vampirico":
                        posAtualListaArcos = 2;
                        prefabArco = ListaPrefabArco[2]; //posicao dele na lista
                        if (ItensArcos != null)
                            Destroy(ItensArcos.gameObject);
                        ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                        ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                        ItensArcos.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                        UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
                        PanelComprarArcos = ItensArcos.transform.GetChild(4).transform.gameObject;
                        break;
                    case "Arco Divisor":
                        posAtualListaArcos = 3;
                        prefabArco = ListaPrefabArco[3]; //posicao dele na lista
                        if (ItensArcos != null)
                            Destroy(ItensArcos.gameObject);
                        ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                        ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                        ItensArcos.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                        UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
                        PanelComprarArcos = ItensArcos.transform.GetChild(4).transform.gameObject;
                        break;
                }
            }

            if (IndicaAtualLugarCasas.activeSelf == true)
            {
             //   Debug.Log("Casa Atual: " + getCasaAtual());
                GameObject prefabCasa;
                switch (getCasaAtual())
                {
                    case "Oca Simples":
                       // Debug.Log("Cheguei na ocaSimples");
                        prefabCasa = ListaPrefabCasa[0]; //posicao dela na lista
                        if (ItensCasas != null)
                            Destroy(ItensCasas.gameObject);
                        ItensCasas = Instantiate(prefabCasa, new Vector3(0, 40, 0), transform.rotation);
                        ItensCasas.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        ItensCasas.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                        UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
                        DescricaoItemPanelCasas.SetActive(true);
                        UpgradePanelCasas.SetActive(false);
                        break;
                    case "Oca Artesanal":
                       // Debug.Log("Cheguei na oca artesanal");
                        prefabCasa = ListaPrefabCasa[1]; //posicao dela na lista
                        if (ItensCasas != null)
                            Destroy(ItensCasas.gameObject);
                        ItensCasas = Instantiate(prefabCasa, new Vector3(0, 40, 0), transform.rotation);
                        ItensCasas.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        ItensCasas.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                        UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
                        PanelComprarCasas = ItensCasas.transform.GetChild(4).transform.gameObject;
                        DescricaoItemPanelCasas.SetActive(true);
                        UpgradePanelCasas.SetActive(false);
                        break;
                    case "Oca Fortificada":
                        // Debug.Log("Cheguei na oca artesanal");
                        prefabCasa = ListaPrefabCasa[2]; //posicao dela na lista
                        if (ItensCasas != null)
                            Destroy(ItensCasas.gameObject);
                        ItensCasas = Instantiate(prefabCasa, new Vector3(0, 40, 0), transform.rotation);
                        ItensCasas.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                        ItensCasas.transform.SetParent(fundoItens.transform, false);
                        DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                        UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
                        PanelComprarCasas = ItensCasas.transform.GetChild(4).transform.gameObject;
                        DescricaoItemPanelCasas.SetActive(true);
                        UpgradePanelCasas.SetActive(false);
                        break;
                }
            }
        }
    }
    //Funcoes Update

    public void atualizaItem()
    {        
        if (LojaItens.activeSelf == true)
        {
            if (IndicaAtualLugarArcos.activeSelf == true)
            {
                switch (posAtualListaArcos)
                {
                    case 0:
                        nomeDoArcoQueEstouVendo = "Arco de Bambu";
                        //Debug.Log("Nome estou vendo: Arco de bambu" + nomeDoArcoQueEstouVendo);

                        break;
                    case 1:
                        nomeDoArcoQueEstouVendo = "Arco Explosivo";
                        //Debug.Log("Nome estou vendo: Arco Explosivo" + nomeDoArcoQueEstouVendo);
                        break;
                    case 2:
                        nomeDoArcoQueEstouVendo = "Arco Vampirico";
                        //Debug.Log("Nome estou vendo: Arco Explosivo" + nomeDoArcoQueEstouVendo);
                        break;
                    case 3:
                        nomeDoArcoQueEstouVendo = "Arco Divisor";
                        //Debug.Log("Nome estou vendo: Arco Explosivo" + nomeDoArcoQueEstouVendo);
                        break;
                }
                
                if (DescricaoItemPanelArcos.activeSelf == true && DescricaoItemPanelArcos !=null && UpgradePanelArcos!=null && UpgradePanelArcos.activeSelf == false)
                {                    
                    TextMeshProUGUI txtPoderTiro = GameObject.Find("txtPoderTiro").GetComponent<TextMeshProUGUI>();
                    //Debug.Log("PoderTiro: " + txtPoderTiro.text);
                    txtPoderTiro.text = PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo+ "PoderTiro").ToString();
                    TextMeshProUGUI txtDelay = GameObject.Find("txtDelay").GetComponent<TextMeshProUGUI>();
                    txtDelay.text = PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo + "Delay").ToString();
                    //Debug.Log("getDelay: " + txtDelay.text);
                    TextMeshProUGUI txtHabilidade = GameObject.Find("txtHabilidade").GetComponent<TextMeshProUGUI>();
                    txtHabilidade.text = PlayerPrefs.GetString(nomeDoArcoQueEstouVendo + "Habilidade");
                }
            }
            if (IndicaAtualLugarCasas.activeSelf == true)
            {
                switch (posAtualListaCasas)
                {
                    case 0:
                        nomeDaCasaQueEstouVendo = "Oca Simples";
                        break;
                    case 1:
                        nomeDaCasaQueEstouVendo = "Oca Artesanal";
                        break;
                    case 2:
                        nomeDaCasaQueEstouVendo = "Oca Fortificada";
                        break;
                }
                if (DescricaoItemPanelCasas.activeSelf == true && DescricaoItemPanelCasas != null && ItensCasas != null)
                {
                       
                    txtVidaCasaAtual = GameObject.Find("txtVida").GetComponent<TextMeshProUGUI>();
                    txtVidaCasaAtual.text = PlayerPrefs.GetFloat(nomeDaCasaQueEstouVendo+ "CasaVidaAtual").ToString();

                }
            }
        }
    }

    public void AtualizaListaDeArcos()
    {        
        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            GameObject btnUpForca = GameObject.Find("btnUpForca");
            GameObject btnUpDelay = GameObject.Find("btnUpDelay");
            setEstrelasTotaisArco((PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo + "UpForca") + PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo + "UpDelay")) / 2);
            Image estrelasTotalArco = GameObject.Find("estrelaTotalArco").GetComponent<Image>();
            Image bordaEstrelasTotalArco = GameObject.Find("bordaEstrelaTotalArco").GetComponent<Image>();
            if (ItensArcos != null)
            {
                DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
            }
            if (posAtualListaArcos != 0 && nomeDoArcoQueEstouVendo != "Arco de Bambu")
                PanelComprarArcos = ItensArcos.transform.GetChild(4).transform.gameObject;
            if (PlayerPrefs.GetInt(nomeDoArcoQueEstouVendo) == 0)
            {
                estrelasTotalArco.enabled = false;
                bordaEstrelasTotalArco.enabled = false;
                botaoComprar.SetActive(true);
                btnUpgrade.SetActive(false);
                botaoEquipar.SetActive(false);
                btnStatus.SetActive(false);
                DescricaoItemPanelArcos.SetActive(false);
                UpgradePanelArcos.SetActive(false);
                if (posAtualListaArcos != 0 && PanelComprarArcos != null)
                   PanelComprarArcos.SetActive(true);
                
            }
            if (PlayerPrefs.GetInt(nomeDoArcoQueEstouVendo) == 1)
            {
                estrelasTotalArco.enabled = true;
                bordaEstrelasTotalArco.enabled = true;
                estrelasTotalArco.fillAmount = getEstrelasTotaisArco();
                
                if (nomeDoArcoQueEstouVendo == getArcoAtual())
                {
                    //Botoes
                    if(EstouNaDescricao)
                    {
                        DescricaoItemPanelArcos.SetActive(true);
                        UpgradePanelArcos.SetActive(false);
                        btnStatus.SetActive(false);
                        botaoEquipar.SetActive(false);
                        btnUpgrade.SetActive(true);
                         PlayerPrefs.GetFloat(getArcoAtual() + "UpForca");
                    }
                    if (EstouNoUpgrade)
                    {
                        DescricaoItemPanelArcos.SetActive(false);
                        UpgradePanelArcos.SetActive(true);
                        btnStatus.SetActive(true);
                        botaoEquipar.SetActive(false);
                        btnUpgrade.SetActive(false);
                        EstrelasForca = GameObject.Find("estrelasForca").GetComponent<Image>();
                        EstrelasDelay = GameObject.Find("estrelasDelay").GetComponent<Image>();
                        PegaPrecoAtualUpgradeForcaEPassaParaOTxt();
                        PegaPrecoAtualUpgradeDelayEPassaParaOTxt();

                        if (getTotalUpgradeForca() >= 1)
                        {
                            if (btnUpForca != null)
                            {
                                btnUpForca.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
                                btnUpForca.GetComponent<Button>().enabled = false;
                                btnUpForca.GetComponent<Image>().enabled = false;
                            }
                        }
                        else
                        {
                            if (btnUpForca != null)
                            {
                                btnUpForca.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
                                btnUpForca.GetComponent<Button>().enabled = true;
                                btnUpForca.GetComponent<Image>().enabled = true;
                            }
                        }
                        if (getTotalUpgradeDelay() >= 1)
                        {
                            if (btnUpDelay != null)
                            {
                                btnUpDelay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
                                btnUpDelay.GetComponent<Button>().enabled = false;
                                btnUpDelay.GetComponent<Image>().enabled = false;
                            }
                        }
                        else
                        {
                            if (btnUpDelay != null)
                            {
                                btnUpDelay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
                                btnUpDelay.GetComponent<Button>().enabled = true;
                                btnUpDelay.GetComponent<Image>().enabled = true;
                            }
                        }
                    }
                    
                    //
                }
                if (nomeDoArcoQueEstouVendo != getArcoAtual())
                {
                    EstouNaDescricao = true;
                    if (EstouNaDescricao)
                    {
                        DescricaoItemPanelArcos.SetActive(true);
                        UpgradePanelArcos.SetActive(false);
                        btnStatus.SetActive(false);
                        botaoEquipar.SetActive(true);
                        btnUpgrade.SetActive(false);
                    }
                    
                }
                botaoComprar.SetActive(false);                   
                    
                    if (posAtualListaArcos != 0 && PanelComprarArcos != null)
                        PanelComprarArcos.SetActive(false);              

            }
        }       
    }
    public void AtualizaListaDeCasas()
    {
        
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            GameObject btnUpVida = GameObject.Find("btnUpVida");
            setEstrelasTotaisCasa(PlayerPrefs.GetFloat(nomeDaCasaQueEstouVendo + "UpCasaVida"));
            Image estrelasTotalCasa = GameObject.Find("estrelaTotalArco").GetComponent<Image>();
            Image bordaEstrelasTotalCasa = GameObject.Find("bordaEstrelaTotalArco").GetComponent<Image>();
           

            if (ItensCasas != null)
            {
                DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
            }
            if (posAtualListaCasas != 0 && nomeDaCasaQueEstouVendo != "Oca de Bambu")
                PanelComprarCasas = ItensCasas.transform.GetChild(4).transform.gameObject;
            
            if (PlayerPrefs.GetInt(nomeDaCasaQueEstouVendo) == 0 )
            {
                estrelasTotalCasa.enabled = false;
                bordaEstrelasTotalCasa.enabled = false;
                btnStatus.SetActive(false);
                botaoComprar.SetActive(true);
                    btnUpgrade.SetActive(false);
                botaoEquipar.SetActive(false);
                   DescricaoItemPanelCasas.SetActive(false);
                    UpgradePanelCasas.SetActive(false);
                    if (posAtualListaCasas != 0 && PanelComprarCasas != null)
                        PanelComprarCasas.SetActive(true);
                    
                
            }
            if (PlayerPrefs.GetInt(nomeDaCasaQueEstouVendo) == 1)
            {
                estrelasTotalCasa.enabled = true;
                bordaEstrelasTotalCasa.enabled = true;
                estrelasTotalCasa.fillAmount = getEstrelasTotaisCasa();

                if (nomeDaCasaQueEstouVendo == getCasaAtual())
                {
                    //Botoes
                    if (EstouNaDescricao)
                    {
                        DescricaoItemPanelCasas.SetActive(true);
                        UpgradePanelCasas.SetActive(false);
                        btnStatus.SetActive(false);
                        botaoEquipar.SetActive(false);
                        btnUpgrade.SetActive(true);
                    }
                    if (EstouNoUpgrade)
                    {
                        DescricaoItemPanelCasas.SetActive(false);
                        UpgradePanelCasas.SetActive(true);
                        btnStatus.SetActive(true);
                        botaoEquipar.SetActive(false);
                        btnUpgrade.SetActive(false);
                        EstrelasVida = GameObject.Find("estrelasVida").GetComponent<Image>();
                        PegaPrecoAtualUpgradeVidaEPassaParaOTxt();

                        if (getTotalUpgradeCasaVida() >= 1)
                        {
                            if (btnUpVida != null)
                            {
                                btnUpVida.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
                                btnUpVida.GetComponent<Button>().enabled = false;
                                btnUpVida.GetComponent<Image>().enabled = false;
                            }
                        }
                        else
                        {
                            if (btnUpVida != null)
                            {
                                btnUpVida.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
                                btnUpVida.GetComponent<Button>().enabled = true;
                                btnUpVida.GetComponent<Image>().enabled = true;
                            }
                        }
                    }

                    //
                }
                if (nomeDaCasaQueEstouVendo != getCasaAtual())
                {
                    EstouNaDescricao = true;
                    if (EstouNaDescricao)
                    {
                        DescricaoItemPanelCasas.SetActive(true);
                        UpgradePanelCasas.SetActive(false);
                        btnStatus.SetActive(false);
                        botaoEquipar.SetActive(true);
                        btnUpgrade.SetActive(false);
                    }

                }

                botaoComprar.SetActive(false);
                    
                
                    if (posAtualListaCasas != 0 && PanelComprarCasas != null)
                        PanelComprarCasas.SetActive(false);
                    
                

            }
        }
    }
    public void PegaPrecoAtualUpgradeForcaEPassaParaOTxt()
    {
        if (getTotalUpgradeForca() == 0.0f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase();
        if (getTotalUpgradeForca() == 0.2f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase() * 2;
        if (getTotalUpgradeForca() == 0.4f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase() * 4;
        if (getTotalUpgradeForca() == 0.6f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase() * 8;
        if (getTotalUpgradeForca() == 0.8f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase() * 16;
        if (getTotalUpgradeForca() == 1f)
            PrecoUpgradeArcoFinalForca = getPrecoUpgradeArcoBase() * 16;

        precoAtualUpgradeForca = PlayerPrefs.GetFloat(getArcoAtual() + "PrecoForca");
        //GameObject.Find("PrecoForcaTxt").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo+ "PrecoForca").ToString();
        GameObject.Find("PrecoForcaTxt").GetComponent<TextMeshProUGUI>().text = PrecoUpgradeArcoFinalForca.ToString();
        EstrelasForca.fillAmount = PlayerPrefs.GetFloat(getArcoAtual() + "UpForca");
        //Debug.Log("Estrelas for fill amount: " + EstrelasForca.fillAmount);
    }    
    public void PegaPrecoAtualUpgradeDelayEPassaParaOTxt()
    {
        if (getTotalUpgradeDelay() == 0.0f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase();
        if (getTotalUpgradeDelay() == 0.2f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase() * 2;
        if (getTotalUpgradeDelay() == 0.4f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase() * 4;
        if (getTotalUpgradeDelay() == 0.6f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase() * 8;
        if (getTotalUpgradeDelay() == 0.8f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase() * 16;
        if (getTotalUpgradeDelay() == 1f)
            PrecoUpgradeArcoFinalDelay = getPrecoUpgradeArcoBase() * 16;

        precoAtualUpgradeDelay = PlayerPrefs.GetFloat(getArcoAtual() + "PrecoDelay");
        //GameObject.Find("PrecoDelayTxt").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo + "PrecoDelay").ToString();
        GameObject.Find("PrecoDelayTxt").GetComponent<TextMeshProUGUI>().text = PrecoUpgradeArcoFinalDelay.ToString();
        EstrelasDelay.fillAmount = PlayerPrefs.GetFloat(getArcoAtual() + "UpDelay");
        //Debug.Log("Estrelas de fill amount: " + EstrelasDelay.fillAmount);
    }
    public void PegaPrecoAtualUpgradeVidaEPassaParaOTxt()
    {
        if (getTotalUpgradeCasaVida() == 0.0f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase();
        if (getTotalUpgradeCasaVida() == 0.2f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase() * 2;
        if (getTotalUpgradeCasaVida() == 0.4f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase() * 4;
        if (getTotalUpgradeCasaVida() == 0.6f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase() * 8;
        if (getTotalUpgradeCasaVida() == 0.8f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase() * 16;
        if (getTotalUpgradeCasaVida() == 1f)
            PrecoUpgradeCasaFinalVida = getPrecoUpgradeCasaBase() * 16;
        precoAtualUpgradeCasaVida = PlayerPrefs.GetFloat(getCasaAtual() + "PrecoVida");
        //Debug.Log("up: "+ PlayerPrefs.GetFloat(getCasaAtual() + "UpCasaVida"));
        //GameObject.Find("PrecoVidaTxt").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat(nomeDaCasaQueEstouVendo + "PrecoVida").ToString();
        GameObject.Find("PrecoVidaTxt").GetComponent<TextMeshProUGUI>().text = PrecoUpgradeCasaFinalVida.ToString();
        EstrelasVida.fillAmount = PlayerPrefs.GetFloat(getCasaAtual() + "UpCasaVida");
        //Debug.Log("Estrelas vida fill amount: " + EstrelasVida.fillAmount);
    }
    //


    //Getters e setters
    //Arco
    public void setEstrelasTotaisArco(float EstrelasTotais)
    {
        PlayerPrefs.SetFloat(nomeDoArcoQueEstouVendo + "EstrelasTotais", EstrelasTotais);
    }
    public float getEstrelasTotaisArco()
    {
        return PlayerPrefs.GetFloat(nomeDoArcoQueEstouVendo + "EstrelasTotais");
    }
    public float getTotalUpgradeForca()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "UpForca");
    }
    public void setTotalUpgradeForca(float UpgradeAtualForca)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "UpForca", UpgradeAtualForca);
    }
    public string getArcoAtual()
    {
        return PlayerPrefs.GetString("ArcoAtual");
    }
    public void setArcoAtual(string ArcoAtual)
    {
        PlayerPrefs.SetString("ArcoAtual", ArcoAtual);
    }
    public float getTotalUpgradeDelay()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "UpDelay");
    }
    public void setPrecoUpgradeDelay(float PrecoDelay)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "PrecoDelay", PrecoDelay);
    }
    public void setPrecoUpgradeForca(float UpgradeAtualForca)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "PrecoForca", UpgradeAtualForca);
    }
    public float getPrecoUpgradeForca()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "PrecoForca");
    }
    public float getPrecoUpgradeDelay()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "PrecoDelay");
    }
    public void setTotalUpgradeDelay(float UpgradeAtualDelay)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "UpDelay", UpgradeAtualDelay);
    }
   
    public float getTotalCarnes()
    {
        carneTotal = PlayerPrefs.GetFloat("carnes");
        return carneTotal;
    }
    public void setTotalCarnes(float carneTotal)
    {
        PlayerPrefs.SetFloat("carnes", carneTotal);
    }
    public string getArcoEquipado()
    {
        return PlayerPrefs.GetString("ArcoEquipado");
    }
    public void setArcoEquipado(string ArcoAtual)
    {
        PlayerPrefs.SetString("ArcoEquipado", ArcoAtual);
    }
    //

    //Casas
    public float getPrecoCasaVidaInicial()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "PrecoVidaInicial");
    }
    public void setPrecoCasaVidaInicial(float PrecoVidaInicial)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "PrecoVidaInicial", PrecoVidaInicial);
    }
    public void setEstrelasTotaisCasa(float EstrelasTotais)
    {
        PlayerPrefs.SetFloat(nomeDaCasaQueEstouVendo + "EstrelasTotais", EstrelasTotais);
    }
    public float getEstrelasTotaisCasa()
    {
        return PlayerPrefs.GetFloat(nomeDaCasaQueEstouVendo + "EstrelasTotais");
    }
    public string getCasaAtual()
    {
        return PlayerPrefs.GetString("CasaAtual");
    }
    public void setCasaAtual(string CasaAtual)
    {
        PlayerPrefs.SetString("CasaAtual", CasaAtual);
    }
    public float getTotalUpgradeCasaVida()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "UpCasaVida");
    }
    public void setTotalUpgradeCasaVida(float UpgradeAtualCasa)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "UpCasaVida", UpgradeAtualCasa);
    }
    public string getHabilidadeCasa()
    {
        return PlayerPrefs.GetString(getCasaAtual() + "Habilidade");
    }
    public void setHabilidadeCasa(string Habilidade)
    {
        PlayerPrefs.SetString(getCasaAtual() + "Habilidade", Habilidade);
    }
    //
    //Botoes
    public void btnPanelErro()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        PanelMsgErro.SetActive(false);
    }
    public void btnEquipar()
    {
        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            switch (posAtualListaArcos)
            {
                case 0:
                    setArcoAtual("Arco de Bambu");
                    Debug.Log("Arco Equipado: " + getArcoAtual());
                    break;
                case 1:
                    setArcoAtual("Arco Explosivo");
                    Debug.Log("Arco Equipado: " + getArcoAtual());
                    break;
                case 2:
                    setArcoAtual("Arco Vampirico");
                    Debug.Log("Arco Equipado: " + getArcoAtual());
                    break;
                case 3:
                    setArcoAtual("Arco Divisor");
                    Debug.Log("Arco Equipado: " + getArcoAtual());
                    break;
            }
        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            switch (posAtualListaCasas)
            {
                case 0:
                    setCasaAtual("Oca Simples");
                    Debug.Log("Oca Equipada: " + getCasaAtual());
                    break;
                case 1:
                    setCasaAtual("Oca Artesanal");
                    Debug.Log("Oca Equipada: " + getCasaAtual());
                    break;
                case 2:
                    setCasaAtual("Oca Fortificada");
                    Debug.Log("Oca Equipada: " + getCasaAtual());
                    break;
            }
        }
    }
    public void btnAnterior()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            if (posAtualListaArcos != 0)
            {
                Destroy(ItensArcos.gameObject);
                GameObject prefabArco, fundoItens;
                posAtualListaArcos--;
                fundoItens = GameObject.Find("FundoItens");
                prefabArco = ListaPrefabArco[posAtualListaArcos];
                ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                ItensArcos.transform.SetParent(fundoItens.transform, false);
                DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;                
            } 
            if(posAtualListaArcos == 0)
            {
                btnAntes.SetActive(false);
            }
            if (posAtualListaArcos < TamanhoMaxListaArcos)
            {
                btnDepois.SetActive(true);
            }

        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            if (posAtualListaCasas != 0)
            {
                Destroy(ItensCasas.gameObject);
                GameObject prefabCasas, fundoItens;
                posAtualListaCasas--;
                fundoItens = GameObject.Find("FundoItens");
                prefabCasas = ListaPrefabCasa[posAtualListaCasas];
                ItensCasas = Instantiate(prefabCasas, new Vector3(0, 40, 0), transform.rotation);
                ItensCasas.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                ItensCasas.transform.SetParent(fundoItens.transform, false);
                DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;               
            }
            if (posAtualListaCasas == 0)
            {
                btnAntes.SetActive(false);
            }
            if (posAtualListaCasas < TamanhoMaxListaCasas)
            {
                btnDepois.SetActive(true);
            }
        }
        EstouNaDescricao = true;
        EstouNoUpgrade = false;
    }
    public void btnProx()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            
            if (posAtualListaArcos < TamanhoMaxListaArcos)
            {
                Destroy(ItensArcos.gameObject);
                GameObject prefabArco, fundoItens;
                posAtualListaArcos++;
                fundoItens = GameObject.Find("FundoItens");
                prefabArco = ListaPrefabArco[posAtualListaArcos];
                ItensArcos = Instantiate(prefabArco, new Vector3(0, 40, 0), transform.rotation);
                ItensArcos.transform.localScale = new Vector3(2.5154f, 2.6154f, 2.6154f);
                ItensArcos.transform.SetParent(fundoItens.transform, false);
                DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
                UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;                               
            }
            if (posAtualListaArcos > 0)
            {
                btnAntes.SetActive(true);
            }
            if(posAtualListaArcos >= TamanhoMaxListaArcos)
            {
                btnDepois.SetActive(false);
            }
        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            if (posAtualListaCasas < TamanhoMaxListaCasas)
            {
                Destroy(ItensCasas.gameObject);
                GameObject prefabCasas, fundoItens;
                posAtualListaCasas++;
                fundoItens = GameObject.Find("FundoItens");
                prefabCasas = ListaPrefabCasa[posAtualListaCasas];
                ItensCasas = Instantiate(prefabCasas, new Vector3(0, 40, 0), transform.rotation);
                ItensCasas.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                ItensCasas.transform.SetParent(fundoItens.transform, false);
                DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
                
            }
            if (posAtualListaCasas > 0)
            {
                btnAntes.SetActive(true);
            }
            if(posAtualListaCasas >= TamanhoMaxListaCasas)
            {
                btnDepois.SetActive(false);
            }
        }
        EstouNaDescricao = true;
        EstouNoUpgrade = false;
    }
    public void AtualizaSetaLoja()
    {
        Debug.Log("Aqui1");
        if (getArcoAtual() == "Arco de Bambu")
        {
            Debug.Log("Aqui");
            btnAntes.SetActive(false);
        }
        else
        {
            Debug.Log("Aqui2");
            btnAntes.SetActive(true);
        }

        if (getArcoAtual() == "Arco Divisor")
        {
            btnDepois.SetActive(false);
        } else
        {
            btnDepois.SetActive(true);            
        }

    }
    public void btnComprar()
    {
        if (IndicaAtualLugarArcos.activeSelf==true)
        {
            switch (posAtualListaArcos)
            {
                case 0:
                    PlayerPrefs.SetInt("Arco de Bambu", 1);
                    break;
                case 1:
                    float precoArcoExplosivo = PlayerPrefs.GetInt("Arco ExplosivoPrecoInicial");
                    if (getTotalCarnes() >= precoArcoExplosivo && PlayerPrefs.GetFloat("Arco de BambuEstrelasTotais") >= 1)
                    {
                        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                        PlayerPrefs.SetInt("Arco Explosivo", 1);
                       // Debug.Log("Tenho o Arco: " + PlayerPrefs.GetInt("Arco Explosivo"));
                        setArcoAtual("Arco Explosivo");
                        setaArcoExplosivo();
                        setTotalCarnes(getTotalCarnes() - precoArcoExplosivo);
                    } else
                    {
                        PanelMsgErro.SetActive(true);
                        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
                    }
                    break;
                case 2:
                    float precoArcoVampirico = PlayerPrefs.GetInt("Arco VampiricoPrecoInicial");
                    if (getTotalCarnes() >= precoArcoVampirico && PlayerPrefs.GetFloat("Arco ExplosivoEstrelasTotais") >= 1)
                    {
                        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                        PlayerPrefs.SetInt("Arco Vampirico", 1);
                        setArcoAtual("Arco Vampirico");
                        setaArcoVampirico();
                        setTotalCarnes(getTotalCarnes() - precoArcoVampirico);
                    }
                    else
                    {
                        PanelMsgErro.SetActive(true);
                        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
                    }
                    break;
                case 3:
                    float precoArcoDivisor = PlayerPrefs.GetInt("Arco DivisorPrecoInicial");
                    if (getTotalCarnes() >= precoArcoDivisor && PlayerPrefs.GetFloat("Arco VampiricoEstrelasTotais") >= 1)
                    {
                        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                        PlayerPrefs.SetInt("Arco Divisor", 1);
                        setArcoAtual("Arco Divisor");
                        setaArcoDivisor();
                        setTotalCarnes(getTotalCarnes() - precoArcoDivisor);
                    }
                    else
                    {
                        PanelMsgErro.SetActive(true);
                        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
                    }
                    break;

            }
            
        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            switch (posAtualListaCasas)
            {
                case 0:
                    PlayerPrefs.SetInt("Oca Simples", 1);
                    break;
                case 1:
                    float precoOcaArtesanal = PlayerPrefs.GetInt("Oca ArtesanalPrecoInicial");
                    if (getTotalCarnes() >= precoOcaArtesanal && PlayerPrefs.GetFloat("Oca SimplesEstrelasTotais") >= 1)
                    {
                        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                        PlayerPrefs.SetInt("Oca Artesanal", 1);
                        setCasaAtual("Oca Artesenal");
                        setaOcaArtesanal();
                        setTotalCarnes(getTotalCarnes() - precoOcaArtesanal);
                        EstouNaDescricao = true;
                        EstouNoUpgrade = false;
                    } else
                    {
                        PanelMsgErro.SetActive(true);
                        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
                    }
                    break;
                case 2:
                    float precoOcaFortificada = PlayerPrefs.GetInt("Oca FortificadaPrecoInicial");
                    if (getTotalCarnes() >= precoOcaFortificada && PlayerPrefs.GetFloat("Oca ArtesanalEstrelasTotais") >= 1)
                    {
                        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                        PlayerPrefs.SetInt("Oca Fortificada", 1);
                        setCasaAtual("Oca Fortificada");
                        setaOcaFortificada();
                        setTotalCarnes(getTotalCarnes() - precoOcaFortificada);
                        EstouNaDescricao = true;
                        EstouNoUpgrade = false;
                    }
                    else
                    {
                        PanelMsgErro.SetActive(true);
                        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
                    }
                    break;
            }
        }

        EstouNaDescricao = true;
        EstouNoUpgrade = false;
    }
    public void OnClickCasa()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        switch (getCasaAtual())
        {
            case "Oca Simples":
                posAtualListaCasas = 0;
                break;
            case "Oca Artesanal":
                posAtualListaCasas = 1;
                break;
            case "Oca Fortificada":
                posAtualListaCasas = 2;
                break;
        }
        if (ItensArcos!=null)
           ItensArcos.SetActive(false);

        IndicaAtualLugarArcos.SetActive(false);
        IndicaAtualLugarCasas.SetActive(true);
        PegaPrefabArcoECasa();

        EstouNaDescricao = true;
        EstouNoUpgrade = false;
    }
    public void OnClickArcos()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        switch (getArcoAtual())
        {
            case "Arco de Bambu":
                posAtualListaArcos = 0;
                break;
            case "Arco Explosivo":
                posAtualListaArcos = 1;
                break;
            case "Arco Vampirico":
                posAtualListaArcos = 2;
                break;
            case "Arco Divisor":
                posAtualListaArcos = 3;
                break;
        }
        if (ItensCasas!=null)
         ItensCasas.SetActive(false);

        IndicaAtualLugarCasas.SetActive(false);
        IndicaAtualLugarArcos.SetActive(true);
        PegaPrefabArcoECasa();

        EstouNaDescricao = true;
        EstouNoUpgrade = false;
    }
    public void OnClickLoja()
    {

        nomeDoArcoQueEstouVendo = getArcoAtual();
        if(ItensCasas!=null)
        ItensCasas.SetActive(false);
        IndicaAtualLugarCasas.SetActive(false);
        //ItensArcos.SetActive(true);
        IndicaAtualLugarArcos.SetActive(true);        
        PegaPrefabArcoECasa();
        EstouNaDescricao = true;
        EstouNoUpgrade = false;

    }
    public void Voltar()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        posAtualListaArcos = 0;
        Destroy(ItensArcos.gameObject);
        LojaItens.SetActive(false);
        MenuInicial.SetActive(true);

    }
    public void OnClickUpgrade()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();

        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            //Debug.Log("UPGRADE ARCO");
            DescricaoItemPanelArcos = ItensArcos.transform.GetChild(2).transform.gameObject;
            UpgradePanelArcos = ItensArcos.transform.GetChild(1).transform.gameObject;
            UpgradePanelArcos.SetActive(true);
            DescricaoItemPanelArcos.SetActive(false);
            //Pega as estrelas
            
            //           
           
        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
                DescricaoItemPanelCasas = ItensCasas.transform.GetChild(2).transform.gameObject;
                UpgradePanelCasas = ItensCasas.transform.GetChild(1).transform.gameObject;
            UpgradePanelCasas.SetActive(true);
            DescricaoItemPanelCasas.SetActive(false);                  

        }
        EstouNoUpgrade = true;
        EstouNaDescricao = false;

        if (PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0)
        {
            TutorialInicial.estouNosUpgrades = true;
            TutorialInicial.positionBtnForca = GameObject.Find("btnUpForca").transform.position;
            GameObject.Find("btnUpDelay").GetComponent<Button>().enabled = false;
            GameObject.Find("btnUpDelay").GetComponent<Image>().enabled = false;
            
        }

    }
    public void OnClickStatus()
    {
        GameObject.Find("CanvasStartMenu").GetComponent<AudioSource>().Play();
        if (IndicaAtualLugarArcos.activeSelf == true)
        {
            UpgradePanelArcos.SetActive(false);
            DescricaoItemPanelArcos.SetActive(true);
        }
        if (IndicaAtualLugarCasas.activeSelf == true)
        {
            UpgradePanelCasas.SetActive(false);
            DescricaoItemPanelCasas.SetActive(true);
        }
        EstouNaDescricao = true;
        EstouNoUpgrade = false;
        
    }
    public void upgradeForca()
    {
        if (getTotalUpgradeForca() < 1)
        {
            if (PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0)
            {
                TutorialInicial.concluiuTutorial = true;
                
            }
                if (getTotalCarnes() >= getPrecoUpgradeForca())
            {
                GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                //Pega nos prefs o valor atual do upgrade salvo
                upgradeAtualForca = getTotalUpgradeForca();
                //Debug.Log("UpgradeAtualForca: " + upgradeAtualForca);
                //Diminui o valor da carne do upgrade
                setTotalCarnes(getTotalCarnes() - precoAtualUpgradeForca);
                //Debug.Log("Carne reduzida, novo valor: " + getTotalCarnes());
                //Atualiza valor atual
                upgradeAtualForca += 0.2f;
                //Salva novo valor
                setTotalUpgradeForca(upgradeAtualForca);
                //Debug.Log("UpgradeAtualForca Atualizado: " + upgradeAtualForca);
                //Atualiza o poder do tiro
                float PoderTiroFinal = getPoderTiroBase() + (upgradeAtualForca * getMultiplicadorStatus());
                setPoderTiro(PoderTiroFinal);
                //Debug.Log(getPoderTiro());
                //PassaValor Atual para a estrela

                //dobra o preco do upgrade
                if(getTotalUpgradeForca() <= 0.8f)
                precoAtualUpgradeForca += precoAtualUpgradeForca;
                //Debug.Log("Preco atual do upgrade: " + precoAtualUpgradeForca);
                //atribui preco atual do upgrade com o nome do arco
                setPrecoUpgradeForca(precoAtualUpgradeForca);
                //Passa o preco novo para o txt
                TextMeshProUGUI txtPrecoForca = GameObject.Find("PrecoForcaTxt").GetComponent<TextMeshProUGUI>();
                txtPrecoForca.text = precoAtualUpgradeForca.ToString();
                //Debug.Log("Novo texto forca: " + txtPrecoForca.text);
            } else
            {
                PanelMsgErro.SetActive(true);
                
            }
        }

    }
    public void upgradeDelay()
    {
        if (getTotalUpgradeDelay() < 1)
        {
            if (PlayerPrefs.HasKey("FezTutoInicial") == false || PlayerPrefs.GetInt("FezTutoInicial") == 0)
            {
                TutorialInicial.concluiuTutorial = true;

            }
            if (getTotalCarnes() >= getPrecoUpgradeDelay())
            {
                GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                //Pega nos prefs o valor atual do upgrade salvo
                upgradeAtualDelay = getTotalUpgradeDelay();
                //Debug.Log("UpgradeAtualForca: " + upgradeAtualForca);
                //Diminui o valor da carne do upgrade
                setTotalCarnes(getTotalCarnes() - precoAtualUpgradeDelay);
                //Debug.Log("Carne reduzida, novo valor: " + getTotalCarnes());
                //Atualiza valor atual
                upgradeAtualDelay += 0.2f;
                //Salva novo valor
                setTotalUpgradeDelay(upgradeAtualDelay);
                //Debug.Log("UpgradeAtualForca Atualizado: " + upgradeAtualForca);
                //Atualiza o poder do tiro
                
                float DelayFinal = getDelayBase() - (upgradeAtualDelay/2);
                setDelay(DelayFinal);
                //Debug.Log(getPoderTiro());

                //dobra o preco do upgrade
                if (getTotalUpgradeDelay() <= 0.8f)
                    precoAtualUpgradeDelay += precoAtualUpgradeDelay;
                //Debug.Log("Preco atual do upgrade: " + precoAtualUpgradeForca);
                //atribui preco atual do upgrade com o nome do arco
                setPrecoUpgradeDelay(precoAtualUpgradeDelay);
                //Passa o preco novo para o txt
                TextMeshProUGUI txtPrecoDelay = GameObject.Find("PrecoDelayTxt").GetComponent<TextMeshProUGUI>();
                txtPrecoDelay.text = precoAtualUpgradeDelay.ToString();
                //Debug.Log("Novo texto forca: " + txtPrecoForca.text);

            } else
            {
                PanelMsgErro.SetActive(true);
                
            }

        }
    }
    public void upgradeVida()
    {
        if (getTotalUpgradeCasaVida() < 1)
        {
            if (getTotalCarnes() >= getPrecoUpgradeCasaVida())
            {
                GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
                upgradeAtualCasa = getTotalUpgradeCasaVida();
                setTotalCarnes(getTotalCarnes() - precoAtualUpgradeCasaVida);
                upgradeAtualCasa += 0.2f;
                setTotalUpgradeCasaVida(upgradeAtualCasa);
                float VidaFinal = getCasaVidaBase() + (upgradeAtualCasa * getMultiplicadorStatusVidaCasa());
                setCasaVidaAtual(VidaFinal);
                if (getTotalUpgradeCasaVida() <= 0.8f)
                    precoAtualUpgradeCasaVida += precoAtualUpgradeCasaVida;
                setPrecoUpgradeCasaVida(precoAtualUpgradeCasaVida);
                //Passa a vida para a descricao
                txtVidaCasaAtual.text = getCasaVidaAtual().ToString();
            }
            else
            {
                PanelMsgErro.SetActive(true);

            }

        }
    }
    //
    // STATUS ARCO
    //Poder Tiro
    public float getPrecoUpgradeArcoBase()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "PrecoUpgradeArcoBase");
    }
    public void setPrecoUpgradeArcoBase(float precoUpgradeBase)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "PrecoUpgradeArcoBase", precoUpgradeBase);
    }
    public float getPoderTiro()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "PoderTiro");
    }
    public void setPoderTiro(float poderTiro)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "PoderTiro", poderTiro);
    }
    public float getPoderTiroBase()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "PoderTiroBase");
    }
    public void setPoderTiroBase(float PoderTiroBase)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "PoderTiroBase", PoderTiroBase);
    }

    //
    //Delay
    public float getDelay()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "Delay");
    }
    public void setDelay(float Delay)
    {

        PlayerPrefs.SetFloat(getArcoAtual() + "Delay", Delay);
    }
    public float getDelayBase()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "DelayBase");
    }
    public void setDelayBase(float DelayBase)
    {

        PlayerPrefs.SetFloat(getArcoAtual() + "DelayBase", DelayBase);
    }
    //
    public string getHabilidade()
    {
        return PlayerPrefs.GetString(getArcoAtual() + "Habilidade");
    }
    public void setHabilidade(string Habilidade)
    {
        PlayerPrefs.SetString(getArcoAtual() + "Habilidade", Habilidade);
    }
    public float getMultiplicadorStatus()
    {
        return PlayerPrefs.GetFloat(getArcoAtual() + "MultiplicadorStatus");
    }
    public void setMultiplicadorStatus(float MultiplicadorStatus)
    {
        PlayerPrefs.SetFloat(getArcoAtual() + "MultiplicadorStatus", MultiplicadorStatus);
    }
    //
    //Status Casa
    //Vida
    public float getPrecoUpgradeCasaBase()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "PrecoUpgradeCasaBase");
    }
    public void setPrecoUpgradeCasaBase(float PrecoUpgradeCasaBase)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "PrecoUpgradeCasaBase", PrecoUpgradeCasaBase);
    }
    public float getCasaVidaBase()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "CasaVidaBase");
    }
    public void setCasaVidaBase(float CasaVidaBase)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "CasaVidaBase", CasaVidaBase);
    }
    public float getCasaVidaAtual()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "CasaVidaAtual");
    }
    public void setCasaVidaAtual(float CasaVidaAtual)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "CasaVidaAtual", CasaVidaAtual);
    }
    public float getMultiplicadorStatusVidaCasa()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "MultiplicadorStatusVidaCasa");
    }
    public void setMultiplicadorStatusVidaCasa(float MultiplicadorStatusVidaCasa)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "MultiplicadorStatusVidaCasa", MultiplicadorStatusVidaCasa);
    }
    public float getPrecoUpgradeCasaVida()
    {
        return PlayerPrefs.GetFloat(getCasaAtual() + "PrecoVida");
    }
    public void setPrecoUpgradeCasaVida(float PrecoVida)
    {
        PlayerPrefs.SetFloat(getCasaAtual() + "PrecoVida", PrecoVida);
    }

    //
    //Inicia Arco
    public void CriaItens()
    {
        //Inicia os itens primeira vez logando
        if (PlayerPrefs.HasKey("ArcoAtual") == false)
        {
            setArcoAtual("Arco de Bambu");
            if (getArcoAtual() == "Arco de Bambu")
            {                
                setaArcoDeBambu();
            }
        }
        if (PlayerPrefs.HasKey("CasaAtual") == false)
        {
            setCasaAtual("Oca Simples");
            setaOcaSimples();
        }
        //Cria as keys dos arcos
        
        if(PlayerPrefs.HasKey("Arco de Bambu") == false)
        {
            PlayerPrefs.SetInt("Arco de Bambu", 1);
            setaArcoDeBambu();
        }
        if (PlayerPrefs.HasKey("Arco Explosivo") == false)
        {
            PlayerPrefs.SetInt("Arco Explosivo", 0);
            setaArcoExplosivo();
        }
        if (PlayerPrefs.HasKey("Arco Vampirico") == false)
        {
            Debug.Log("TESTE SALVAR");
            PlayerPrefs.SetInt("Arco Vampirico", 0);
            setaArcoVampirico();
        }
        if (PlayerPrefs.HasKey("Arco Divisor") == false)
        {
            PlayerPrefs.SetInt("Arco Divisor", 0);
            setaArcoDivisor();
        }
        //Casas
        if (PlayerPrefs.HasKey("Oca Simples") == false)
        {
            PlayerPrefs.SetInt("Oca Simples", 1);
            setaOcaSimples();
        }
        if (PlayerPrefs.HasKey("Oca Artesanal") == false)
        {
            PlayerPrefs.SetInt("Oca Artesanal", 0);
            setaOcaArtesanal();
        }
        if (PlayerPrefs.HasKey("Oca Fortificada") == false)
        {
            PlayerPrefs.SetInt("Oca Fortificada", 0);
            setaOcaFortificada();
        }


    }
   
    public void ajudaMavi()
    {
        ResetaEdaCarne();
    }

    //Atributos Arcos
    private void setaArcoDeBambu()
    {
        setPoderTiro(20);
        setDelay(2);
        setMultiplicadorStatus(5);
        setPoderTiroBase(20);
        setDelayBase(2f);
        setPrecoUpgradeArcoBase(35);
        setPrecoUpgradeForca(35);
        setPrecoUpgradeDelay(35);
        setHabilidade("Nenhuma");
        setEstrelasTotaisArco(0);
        nomeDoArcoQueEstouVendo = "Arco de Bambu";
    }
    private void setaArcoExplosivo( )
    {        
        setPoderTiro(20);
        setDelay(1.75f);
        setMultiplicadorStatus(10);
        setPoderTiroBase(20);
        setDelayBase(1.75f);
        setPrecoUpgradeArcoBase(75);
        setPrecoUpgradeForca(75);
        setPrecoUpgradeDelay(75);
        setHabilidade("Explosão");
        PlayerPrefs.SetInt("Arco ExplosivoPrecoInicial", 2000);
        nomeDoArcoQueEstouVendo = "Arco Explosivo";
        setEstrelasTotaisArco(0);
    }
    private void setaArcoVampirico()
    {
        setPoderTiro(20);
        setDelay(1.50f);
        setMultiplicadorStatus(10);
        setPoderTiroBase(20);
        setDelayBase(1.50f);
        setPrecoUpgradeArcoBase(100);
        setPrecoUpgradeForca(100);
        setPrecoUpgradeDelay(100);
        setHabilidade("Sangue suga");
        PlayerPrefs.SetInt("Arco VampiricoPrecoInicial", 3500);
        nomeDoArcoQueEstouVendo = "Arco Vampirico";
        setEstrelasTotaisArco(0);
    }
    private void setaArcoDivisor()
    {
        setPoderTiro(20);
        setDelay(1.50f);
        setMultiplicadorStatus(10);
        setPoderTiroBase(20);
        setDelayBase(1.50f);
        setPrecoUpgradeArcoBase(120);
        setPrecoUpgradeForca(120);
        setPrecoUpgradeDelay(120);
        setHabilidade("Bifurcar");
        PlayerPrefs.SetInt("Arco DivisorPrecoInicial", 5000);
        nomeDoArcoQueEstouVendo = "Arco Vampirico";
        setEstrelasTotaisArco(0);
    }
    //Atributo Casas
    private void setaOcaSimples()
    {
        setCasaAtual("Oca Simples");
        setCasaVidaBase(50);
        setCasaVidaAtual(50);
        setMultiplicadorStatusVidaCasa(125);
        setPrecoUpgradeCasaBase(75);
        setPrecoCasaVidaInicial(75);
        setPrecoUpgradeCasaVida(75);
        setEstrelasTotaisCasa(0);
        nomeDaCasaQueEstouVendo = "Oca Simples";
    }
    private void setaOcaArtesanal()
    {
        setCasaAtual("Oca Artesanal");
        setCasaVidaBase(100);
        setCasaVidaAtual(100);
        setMultiplicadorStatusVidaCasa(125);
        setPrecoUpgradeCasaBase(125);
        setPrecoCasaVidaInicial(125);
        setPrecoUpgradeCasaVida(125);
        PlayerPrefs.SetInt("Oca ArtesanalPrecoInicial", 3000);
        nomeDaCasaQueEstouVendo = "Oca Artesanal";
        setEstrelasTotaisCasa(0);
    }
    private void setaOcaFortificada()
    {
        setCasaAtual("Oca Fortificada");
        setPrecoUpgradeCasaBase(150);
        setCasaVidaBase(150);
        setCasaVidaAtual(150);
        setMultiplicadorStatusVidaCasa(125);
       
        setPrecoCasaVidaInicial(175);
        setPrecoUpgradeCasaVida(175);
        PlayerPrefs.SetInt("Oca FortificadaPrecoInicial", 4000);
        nomeDaCasaQueEstouVendo = "Oca Artesanal";
        setEstrelasTotaisCasa(0);
    }
    void Atualizacoes()
    {
        if (PlayerPrefs.HasKey("Att02BirdAttack") == false)
        {
            atualizacao02BirdAttack();
            PlayerPrefs.SetInt("Att02BirdAttack", 1);
        }
    }
    void atualizacao02BirdAttack()
    {
        setCasaAtual("Oca Simples");
        setPrecoUpgradeCasaBase(75);
        if (getTotalUpgradeCasaVida() == 0.0f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase());
        if (getTotalUpgradeCasaVida() == 0.2f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase()*2);
        if (getTotalUpgradeCasaVida() == 0.4f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase() * 4);
        if (getTotalUpgradeCasaVida() == 0.6f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase() * 8);
        if (getTotalUpgradeCasaVida() == 0.8f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase() * 16);
        if (getTotalUpgradeCasaVida() == 1f)
            setPrecoUpgradeCasaVida(getPrecoUpgradeCasaBase() * 16);

        setArcoAtual("Arco de Bambu");
        setPrecoUpgradeArcoBase(35);
        //AtualizandoPrecoDelay
        if (getTotalUpgradeDelay() == 0.0f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase());
        if (getTotalUpgradeDelay() == 0.2f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase()*2);
        if (getTotalUpgradeDelay() == 0.4f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase() * 4);
        if (getTotalUpgradeDelay() == 0.6f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase() * 8);
        if (getTotalUpgradeDelay() == 0.8f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase() * 16);
        if (getTotalUpgradeDelay() == 1f)
            setPrecoUpgradeDelay(getPrecoUpgradeArcoBase() * 16);
        //AtualizandoPrecoUpForca
        if (getTotalUpgradeForca() == 0.0f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase());
        if (getTotalUpgradeForca() == 0.2f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase()*2);
        if (getTotalUpgradeForca() == 0.4f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase() * 4);
        if (getTotalUpgradeForca() == 0.6f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase() * 8);
        if (getTotalUpgradeForca() == 0.8f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase() * 16);
        if (getTotalUpgradeForca() == 1f)
            setPrecoUpgradeForca(getPrecoUpgradeArcoBase() * 16);

        PlayerPrefs.SetInt("Arco ExplosivoPrecoInicial", 2000);
        PlayerPrefs.SetInt("Arco VampiricoPrecoInicial", 3500);
    }

}



