using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {
    public float maxHeight;
    public float minHeight;
    public float spawnTime;
    float randY;
    Vector3 whereToSpawn;
    float nextSpawn = 0.0f;
    public GameObject [] prefab;
	// Use this for initialization
	void Start () {
        spawnTime = 4f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            randY = Random.Range(minHeight, maxHeight);
            whereToSpawn = new Vector3(transform.position.x, randY, transform.position.z);
            if (DiaENoite.Dia)//se estiver de dia spawna pombo
            {
                if (PlayerPrefs.GetInt("TutoInGame") == 1)                
                    Instantiate(prefab[0], whereToSpawn, Quaternion.identity);
                
            }
            if (!DiaENoite.Dia) // se estiver a noite spawna coruja
            {
                if(PlayerPrefs.GetInt("TutoInGame") == 1)
                Instantiate(prefab[1], whereToSpawn, Quaternion.identity);
            }
        }
    }

    public void reduzSpawnTime(float tempo, float tempoMinReducao)
    {        
        if(spawnTime > tempoMinReducao)
        {
            if (spawnTime - tempo > tempoMinReducao)
            {
                spawnTime -= tempo;
                Debug.Log("Tempo de spawn: " + spawnTime + " segundos");
            } else
            {
                if (spawnTime - tempo < tempoMinReducao)
                    spawnTime = tempoMinReducao;
            }
        }
        
        
    }
}
