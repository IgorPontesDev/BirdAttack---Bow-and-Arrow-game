using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Habilidades : MonoBehaviour {
    // Use this for initialization
    public static bool mouseEmCimaDoBotao = false;
    public static Vector3 Direcao;
    public GameObject btnExplodir, btnBifurcar;
    public static GameObject FlechaAtual;
    public GameObject prefabFlecha;
    public GameObject FlechaInstanciada1, FlechaInstanciada2;
    public static bool possoBifurcar;
    public static float forca;
	void Start () {
        possoBifurcar = false;
		if(PlayerPrefs.GetString("ArcoAtual") == "Arco Explosivo" )
        {
            if (btnExplodir != null)
            btnExplodir.SetActive(true);
        } else
        {
            if (btnExplodir != null)
            btnExplodir.SetActive(false);
        }
        if (PlayerPrefs.GetString("ArcoAtual") == "Arco Divisor")
        {
            if (btnBifurcar != null)
                btnBifurcar.SetActive(true);
        }
        else
        {
            if (btnBifurcar != null)
                btnBifurcar.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(1) && PlayerPrefs.GetString("ArcoAtual") == "Arco Divisor")
        {
            Bifurcar();
        }
        if (Input.GetMouseButton(1) && PlayerPrefs.GetString("ArcoAtual") == "Arco Explosivo")
        {
            Flecha.podeExplodirFlecha = true;
            Time.timeScale = 1;
            GameObject.Find("Tutorial").GetComponent<Image>().enabled = false;
        }
    }
    public void OnClickHabilidadeFlechaExplosiva()
    {
        Flecha.podeExplodirFlecha = true;
        Time.timeScale = 1;
        GameObject.Find("Tutorial").GetComponent<Image>().enabled = false;
    }

    public void OnClickHabilidadeBifurcar()
    {
        Bifurcar();
    }
    
    private void Bifurcar()
    {
        if (possoBifurcar)
        {
            Vector3 direcao1 = new Vector3(Direcao.x - 0.1f, Direcao.y - 0.1f, Direcao.z);
            Vector3 direcao2 = new Vector3(Direcao.x + 0.1f, Direcao.y - 0.3f, Direcao.z);
            Vector3 posInicial1 = new Vector3(FlechaAtual.transform.position.x, FlechaAtual.transform.position.y + 0.5f, FlechaAtual.transform.position.z);
            Vector3 posInicial2 = new Vector3(FlechaAtual.transform.position.x, FlechaAtual.transform.position.y - 0.5f, FlechaAtual.transform.position.z);
            FlechaInstanciada1 = Instantiate(prefabFlecha, posInicial1, Quaternion.identity);
            FlechaInstanciada2 = Instantiate(prefabFlecha, posInicial2, Quaternion.identity);

            FlechaInstanciada1.GetComponent<Rigidbody2D>().AddForce(direcao1 * forca, ForceMode2D.Impulse);
            FlechaInstanciada2.GetComponent<Rigidbody2D>().AddForce(direcao2 * forca, ForceMode2D.Impulse);
            possoBifurcar = false;
        }
    }

    //SKILL BLOQUEIO ESTÁ NOS MANTIMENTOS
    public void ToNoBotao()
    {
        mouseEmCimaDoBotao = true;
    }
    public void SaiDoBotao()
    {
        mouseEmCimaDoBotao = false;
    }
}
