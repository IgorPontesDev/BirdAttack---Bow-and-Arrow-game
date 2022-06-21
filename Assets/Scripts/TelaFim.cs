using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TelaFim : MonoBehaviour
{


    public RectTransform PanelGameOver;
    public static bool gameover;
    public Vector3 emcima, centrodatela;
    public static bool ativa, ativaCountdown;
    public bool ativaNormal;
    public float countdown = 1.0f;
    // Use this for initialization
    void Start()
    {
        ativaCountdown = false;
        ativaNormal = false;
        ativa = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        centrodatela = new Vector3(PanelGameOver.position.x, Screen.height / 2, PanelGameOver.position.z);
        emcima = new Vector3(PanelGameOver.position.x, Screen.height * 1.5f, PanelGameOver.position.z);

        if (!gameover)
        {
            PanelGameOver.position = emcima;
        }
        else
        {
            PanelGameOver.position = Vector3.Lerp(PanelGameOver.position, centrodatela, Time.deltaTime * 10);

        }


        if (ativa == true)
        {
            ativaCountdown = true;
            gameover = !gameover;
            ativa = false;
            
        }
        if (ativaCountdown)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0.0f)
            {
                Time.timeScale = 0f;
                countdown = 0.2f;
                ativaCountdown = false;
            }
        }

    }

    public void VoltaMenu()
    {        
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void Continua()
    {
        GameObject.Find("PauseButton").GetComponent<Button>().enabled = false;
        SceneManager.LoadScene("Jogo");
        TelaFim.gameover = false;
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {        
        Application.Quit();
    }    
}