using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesenhaLinha : MonoBehaviour {
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    public GameObject origem;
    public GameObject destino;
    Vector3 pointAlongLine, pointA, pointB;
    public float lineDrawSpeed;
    float x;
	// Use this for initialization
	void Start () {
        lineDrawSpeed = 1;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.SetPosition(0, origem.transform.position);
        dist = Vector3.Distance(origem.transform.position, destino.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        desenhaLinha();
            if (destino.activeSelf == false) //o lancador.cs esta ativando e desativando o destino e origem
            {
                GetComponent<LineRenderer>().enabled = false;
                zeraLineRenderer();
            }
            if (destino.activeSelf == true)
            {
                GetComponent<LineRenderer>().enabled = true;
                dist = Vector3.Distance(origem.transform.position, destino.transform.position);

                if (counter < dist)
                {
                    counter += .1f / lineDrawSpeed;
                }
            }
        
	}
    void desenhaLinha()
    {
        dist = Vector3.Distance(origem.transform.position, destino.transform.position);
        lineRenderer.SetPosition(0, origem.transform.position);

        x = Mathf.Lerp(1, dist, counter);

        pointA = origem.transform.position;
        pointB = destino.transform.position;

        pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        lineRenderer.SetPosition(1, pointAlongLine);
    }
    void zeraLineRenderer()
    {
        lineRenderer.SetPosition(1, new Vector3(0,0,0));
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
    }
}
