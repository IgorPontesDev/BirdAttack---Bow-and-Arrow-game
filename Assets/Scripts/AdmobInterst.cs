using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobInterst : MonoBehaviour {
    public static bool Morri;
    InterstitialAd AD;
    private string idAnuncioInterstitial = "ca-app-pub-2193421095808625/4285213224";
    // Use this for initialization
    void Start () {
        RequestInterstitialAd();
    
    }
    private void Update()
    {
        if (Morri)
        {
            ShowInters();
            Morri = false;
        }
    }
    public void ShowInters()
    {
        if (AD.IsLoaded())
        {
            AD.Show();
        } else
        {
            RequestInterstitialAd();
        }
    }
    private void RequestInterstitialAd()
    {
        AD = new InterstitialAd(idAnuncioInterstitial);
        AdRequest requset = new AdRequest.Builder().Build();//id do apk
        AD.LoadAd(requset);

       
        
    }
  
}
