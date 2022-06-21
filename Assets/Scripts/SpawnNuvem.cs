using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNuvem : MonoBehaviour
{
    public float maxHeight;
    public float minHeight;
    public float spawnTime = 2f;
    float randY;
    Vector3 whereToSpawn;
    float nextSpawn = 0.0f;
    public GameObject prefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            randY = Random.Range(minHeight, maxHeight);
            whereToSpawn = new Vector3(transform.position.x, randY, transform.position.z);
            Instantiate(prefab, whereToSpawn, Quaternion.identity);
        }
    }

}
