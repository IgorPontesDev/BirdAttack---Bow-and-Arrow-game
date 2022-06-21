using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using TMPro;
public class AdmobReward : MonoBehaviour {
    [SerializeField] private GameObject ConfirmacaoRecebeu, NaoTemVideo;
    [SerializeField] TextMeshProUGUI FeedbackTxt, FeedbackQtdCarne;
    private RewardBasedVideoAd rewardedAd;
    private string rewardedAdID = "ca-app-pub-2193421095808625/4049710824";//"ca-app-pub-3940256099942544/5224354917"; id de teste


    float RandomCarne;
    private bool videoInexistente = false, ganhouRecompensa = false,  clicouBotao = false;
    // Use this for initialization
    void Start () {
        
        clicouBotao = false;
        ConfirmacaoRecebeu.SetActive(false);
        NaoTemVideo.SetActive(false);
        rewardedAd = RewardBasedVideoAd.Instance;
        
        rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestRewardedAd();
    }
    private void Update()
    {
        if (videoInexistente)
        {
            if(NaoTemVideo.activeSelf == false && clicouBotao)
            {                
                NaoTemVideo.SetActive(true);
                FeedbackTxt.text = "Vídeo indisponivel";
                videoInexistente = false;
                clicouBotao = false;

            }
        }
        if (ganhouRecompensa)
        {
            if(ConfirmacaoRecebeu.activeSelf == false)
            {
                NaoTemVideo.SetActive(false);
                ConfirmacaoRecebeu.SetActive(true);
                ganhouRecompensa = false;
            }
        }

    }
    public void RequestRewardedAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request, rewardedAdID);
    }

    public void ShowRewardedAd()
    {
        clicouBotao = true;
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();            
        } else
        {
            RequestRewardedAd();
            rewardedAd.Show();
        }

    }
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        videoInexistente = true;
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        RandomCarne = UnityEngine.Random.Range(30f, 60f);
        FeedbackQtdCarne.text = "Recebeu mais" + (int)RandomCarne + " carnes!";
        ganhouRecompensa = true;
        
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        RequestRewardedAd();
    }
    public void OnOkClickNaoTemVideo()
    {
        GameObject.Find("btnGetCarne").GetComponent<Image>().enabled = true;
        GameObject.Find("btnGetCarne").GetComponent<Button>().enabled = true;
        NaoTemVideo.SetActive(false);
    }
    public void OnOkClick()
    {
        
        PlayerPrefs.SetFloat("carnes", PlayerPrefs.GetFloat("carnes") + (int)RandomCarne);
        ConfirmacaoRecebeu.SetActive(false);
    }
}
