using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantimentos : MonoBehaviour {
    public float VariadorDano;
    public static float minDamage, maxDamage;
    public static bool BloqueouDano;
    private float randomBloquear;
    public static GameObject Casa;
    //SpawnBloqueio
    List<GameObject> popUpList;
    public GameObject clone;
    public GameObject popUpObject;
    public bool possoExibirBloqueio = false;
    // Use this for initialization
    void Start () {

        BloqueouDano = false;
        minDamage = 20;
        maxDamage = 30;
	}

    private void OnTriggerEnter2D(Collider2D outroObj)
    {
        if(outroObj.gameObject.CompareTag("bomba"))
        {
            if (PlayerPrefs.GetString("CasaAtual") == "Oca Fortificada")
            {
                GameObject.Find("CasaSomHit").GetComponent<AudioSource>().Play();
                possoExibirBloqueio = true;
                randomBloquear = Random.Range(1, 100);
                if (randomBloquear > 15 )
                {
                    BloqueouDano = false;
                    Destroy(outroObj.gameObject);
                    if (DiaENoite.Dia)
                        VariadorDano = Random.Range(minDamage, maxDamage);
                    if (!DiaENoite.Dia)
                        VariadorDano = Random.Range(minDamage + 5, maxDamage + 5);
                    BarraVida.vidaAtualEstatica -= VariadorDano;
                }
                else
                {
                    GameObject.Find("CasaSomBloqueio").GetComponent<AudioSource>().Play();
                    if (possoExibirBloqueio)
                        ProduceBloqueio();

                    BloqueouDano = true;
                    Destroy(outroObj.gameObject);
                }
            }
            else
            {
                GameObject.Find("CasaSomHit").GetComponent<AudioSource>().Play();
                BloqueouDano = false;
                Destroy(outroObj.gameObject);
                if (DiaENoite.Dia)
                    VariadorDano = Random.Range(minDamage, maxDamage);
                if (!DiaENoite.Dia)
                    VariadorDano = Random.Range(minDamage + 5, maxDamage + 5);
                BarraVida.vidaAtualEstatica -= VariadorDano;
            }
        }
    }
    public void ProduceBloqueio()
    {
        possoExibirBloqueio = false;
        //Create Instance Of Popup

        Vector3 PosCorrigida = new Vector3(Casa.transform.position.x, Casa.transform.position.y+5, Casa.transform.position.z);
        clone = Instantiate(popUpObject, PosCorrigida, Quaternion.identity);

        StartCoroutine(Wait(clone));
    }
    IEnumerator Wait(GameObject clone)
    {
        yield return new WaitForSeconds(0.8f);
       
        Destroy(clone);
    }
}
