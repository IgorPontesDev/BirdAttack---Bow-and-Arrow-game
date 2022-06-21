using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour {
    public static bool PodeDestruir, podeExplodirFlecha;
    
    
    public Animator animacaoFlechaExplode;
	// Use this for initialization
	void Start () {
        podeExplodirFlecha = false;
        animacaoFlechaExplode = GameObject.Find("ExplosaoFlecha").GetComponent<Animator>();
        PodeDestruir = false;
        
        

    }
	
	// Update is called once per frame
	void Update () {
        
        if (PodeDestruir)
            Destroy(gameObject, 5);
        if (Input.GetMouseButton(1) && PlayerPrefs.GetString("ArcoAtual") == "Arco Explosivo")
        {
            podeExplodirFlecha = true;
        }
        if (podeExplodirFlecha)
        {
            
            GameObject.Find("ExplosaoFlecha").transform.position = gameObject.transform.position;
            animacaoFlechaExplode.SetTrigger("FlechaExplodiu");
            GameObject.Find("Indio").GetComponent<AudioSource>().Play();
            StartCoroutine(KillOnAnimationEnd());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics.IgnoreLayerCollision(8, 8);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (podeExplodirFlecha && PlayerPrefs.GetString("ArcoAtual") == "Arco Explosivo")
        {
            if (collision.gameObject.CompareTag("enemy"))
            {

                GameObject.Find("ExplosaoFlecha").transform.position = gameObject.transform.position;
                animacaoFlechaExplode.SetTrigger("FlechaExplodiu");
                Destroy(gameObject);

            }
        }
    }
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
