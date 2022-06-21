using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class Admob : MonoBehaviour {
    private BannerView bannerView;
    [SerializeField] public string bannerID = "ca-app-pub-2193421095808625/8378880214";
    // Use this for initialization
    void Start () {
        this.RequestBanner();
    }
    public void OnClickLojaDestroiBanner()
    {
        bannerView.Hide();
    }
    public void OnClickVoltarPoemBanner()
    {
        RequestBanner();
    }
    private void RequestBanner()
    {
        //string adUnitId = "ca-app-pub-3940256099942544/6300978111";// id de teste
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();//.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
        bannerView.LoadAd(request);        
    }
    
    void OnDestroy()
    {
        bannerView.Hide();
    }

}
