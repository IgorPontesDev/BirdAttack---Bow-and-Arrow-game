using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Pontuacao : MonoBehaviour {
    public TextMeshProUGUI PontosTxt;
    public static int score;
    public static bool guardaRecord;
    public int scoreAtual;
    public int scoreMaximoSalvo;
    string nomeDaCena;
    public TextMeshProUGUI PontosRecord;
    public SpawnObjects spawn;
    // aumentar tempo de spawn
    bool podeAtribuir;
    float tempoDeSpawn;
    int PontosParaAumentar;
    int puloDeAumento;
    float tempoParaReduzir;
    float tempoMaxDeReducao;
    public float speed;
    private float speedMax;
    // Use this for initialization
    void Start () {
        speedMax = 6f;
        speed = 3; //velocidade do passaro no jogo
        tempoMaxDeReducao = 1.5f;
        tempoParaReduzir = 0.5f;
        puloDeAumento = 10;
        PontosParaAumentar = 5;
        guardaRecord = false; // está sendo modificado no script BarraVida.cs ao fim do jogo
        score = 0; //está sendo modificado no script Flecha.cs
        scoreAtual = 0;
        nomeDaCena = SceneManager.GetActiveScene().name;

        //PlayerPrefs.SetInt(nomeDaCena + "score", 0); resetar score quando precisar
        if (PlayerPrefs.HasKey(nomeDaCena+"score"))
        {
            scoreMaximoSalvo = PlayerPrefs.GetInt(nomeDaCena + "score");
            PontosRecord.text = scoreMaximoSalvo.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        tempoDeSpawn = spawn.spawnTime;
        if (scoreMaximoSalvo < scoreAtual)
        {
            PontosRecord.text = scoreAtual.ToString();
        }
        PontosTxt.text = score.ToString();
        scoreAtual = score;
        if(guardaRecord)
        {
            ChecarScore();

        }
        controlaVelPassaros();
        
    }

    public void controlaVelPassaros()
    {
        if(scoreAtual > PontosParaAumentar && speed < speedMax)
        {
            if(tempoDeSpawn >= tempoMaxDeReducao)
                spawn.reduzSpawnTime(tempoParaReduzir,tempoMaxDeReducao);

            PontosParaAumentar = PontosParaAumentar+puloDeAumento;
            Debug.Log("Pontos para Aumentar: " + PontosParaAumentar);
            speed += 0.5f;
            Debug.Log("Velocidade Atual: " + speed);
            Mantimentos.minDamage += 2;
            Mantimentos.maxDamage += 2;
            Debug.Log("Novo dano do pombo: min  " + Mantimentos.minDamage + " Max: " + Mantimentos.maxDamage);
        }
        
    }
    

    void ChecarScore()
    {
        if(scoreAtual > scoreMaximoSalvo)
        {
            
            scoreMaximoSalvo = scoreAtual;
            PlayerPrefs.SetInt(nomeDaCena + "score", scoreMaximoSalvo);
            guardaRecord = false;
        }
    }
    
}
