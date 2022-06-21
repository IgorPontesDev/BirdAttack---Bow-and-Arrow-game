using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaENoite : MonoBehaviour {
    public GameObject sol, lua, ceu, TapaBuraco;
    Vector3 posFinalSol, posInicialSol;    
    Vector3 posInicialFundo, posFinalFundo;
    float lerpTime = 120, lerpTimeFundo = 120, lerpTimeLua = 45;
    float lerpSolMetadeCaminho, lerpLuaMetadeCaminho;
    float currentLerpTime = 0, currentLerpTimeFundo = 0, currentLerpTimeLua = 0;
    float currentLerpTimeCaminho = 0, currentLerpTimeLuaCaminho = 0;
    float Perc, PercFundo, PercLua;
    float PercCaminho, PercLuaCaminho;
    public bool possoMoverSol;
    public static bool Dia;
    GameObject LuzDoIndio;
	// Use this for initialization
	void Start () {
        LuzDoIndio = GameObject.Find("LuzDoIndio");
        LuzDoIndio.GetComponent<Light>().enabled = false;
        lerpSolMetadeCaminho = lerpTime / 2;
        lerpLuaMetadeCaminho = lerpTimeLua / 2;
        possoMoverSol = true;
        posFinalSol = new Vector3(20, -13.3f, 2);
        posInicialSol = new Vector3(-15.3f, 9, 2);
        posInicialFundo = new Vector3(-3.9f, -271.2f, 3);
        posFinalFundo = new Vector3(-3.9f, 183.7f, 3);
            sol.transform.position = posInicialSol;
            lua.transform.position = posInicialSol;
            //ceu.transform.position = posInicialFundo;
        Dia = true;

    }
	
	// Update is called once per frame
	void Update () {
        
        moveoSol();
       // moveoCeu();
        moveaLua();
    }
    public void moveoSol()
    {       
        if (possoMoverSol)
        {
            lua.transform.GetComponent<Light>().enabled = false;
            sol.transform.GetComponent<Light>().enabled = true;
            currentLerpTime += Time.deltaTime;
            Perc = currentLerpTime / lerpTime;
            
            sol.transform.position = Vector3.Lerp(posInicialSol, posFinalSol, Perc);
            if (currentLerpTime > lerpSolMetadeCaminho)
            {
                currentLerpTimeCaminho += Time.deltaTime;
                PercCaminho = currentLerpTimeCaminho / lerpSolMetadeCaminho;
                sol.transform.GetComponent<Light>().intensity = Mathf.Lerp(15f, 5f, PercCaminho);
            }

            if (currentLerpTime >= lerpTime - (currentLerpTime / 8))
            {
                LuzDoIndio.GetComponent<Light>().enabled = true; ;
            }
            if (currentLerpTime >= lerpTime)
            {
                sol.transform.position = new Vector3(29.6f,34.3f, 2);
                sol.transform.position = posInicialSol;
                possoMoverSol = false;
                currentLerpTimeLua = 0;
                currentLerpTimeLuaCaminho = 0;
                
                lua.transform.GetComponent<Light>().intensity = 3;
            }
        }
    }
    public void moveoCeu()
    {
        currentLerpTimeFundo += Time.deltaTime;
        PercFundo = currentLerpTimeFundo / lerpTimeFundo;
        ceu.transform.position = Vector3.Lerp(posInicialFundo, posFinalFundo, Mathf.PingPong(PercFundo, 1.0f));
    }
    public void moveaLua()
    {
        if (!possoMoverSol)
        {
            
            currentLerpTimeLua += Time.deltaTime;
            PercLua = currentLerpTimeLua / lerpTimeLua;
            Dia = false;
            lua.transform.position = Vector3.Lerp(posInicialSol, posFinalSol, PercLua);
            lua.transform.GetComponent<Light>().enabled = true;
            sol.transform.GetComponent<Light>().enabled = false;
            if (currentLerpTimeLua > lerpLuaMetadeCaminho)
            {
                currentLerpTimeLuaCaminho += Time.deltaTime;
                PercLua = currentLerpTimeLuaCaminho / lerpLuaMetadeCaminho;
                lua.transform.GetComponent<Light>().intensity = Mathf.Lerp(5f, 15f, PercLua);
            }

            if (currentLerpTimeLua >= lerpTimeLua - (currentLerpTimeLua / 3))
            {
                LuzDoIndio.GetComponent<Light>().enabled = false;
            }

            if (currentLerpTimeLua >= lerpTimeLua)
            {
                Dia = true;                
                possoMoverSol = true;                
               lua.transform.position = new Vector3(29.6f, 34.3f, 2);
                lua.transform.position = posInicialSol;
                sol.transform.GetComponent<Light>().intensity = 15;
                currentLerpTimeCaminho = 0;
                currentLerpTime = 0;
                
            }
        }
        
    }
}
