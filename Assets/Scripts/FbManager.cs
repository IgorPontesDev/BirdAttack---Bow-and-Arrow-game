using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using TMPro;
public class FbManager : MonoBehaviour {
    //Variaveis dos dias
    public int partidasJogadas=0;
    Image BotaoImg;
    public Sprite LivreShare, BlockShare;
    //
    int partidasNecessarias;
    public GameObject ConfirmacaoRecebeu, btnLikePage, btnSharePage;
    public TextMeshProUGUI txtResultPanel, txtPRestante;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
        

    }
    private void Start()
    {
        partidasNecessarias = 15;
        BotaoImg = btnSharePage.GetComponent<Image>();
        liberaBotaoDeShare();

        
        if (PlayerPrefs.GetInt("FbCurtiu") == 0 || PlayerPrefs.HasKey("FbCurtiu") == false)
        {
            btnLikePage.SetActive(true);
        }
        else
        {
            btnLikePage.SetActive(false);
        }

        

        if (PlayerPrefs.HasKey("FbCurtiu") == false)
        {
            btnLikePage.SetActive(true);
        }
        else
        {
            btnLikePage.SetActive(false);
        }
    }
    private void liberaBotaoDeShare()
    {
        
        partidasJogadas = PlayerPrefs.GetInt("PartidasJogadas");
        if (partidasJogadas >= partidasNecessarias) {
            PlayerPrefs.SetInt("CompartilhouFb", 0);
        }
        if (PlayerPrefs.GetInt("CompartilhouFb") == 1 && partidasJogadas < partidasNecessarias)
        {
            BotaoImg.sprite = BlockShare;
            txtPRestante.enabled = true;
            int resultado = partidasNecessarias - partidasJogadas;
            if(resultado <0)
            {
                resultado = 0;
            }
            txtPRestante.text = "Partidas restantes:" + resultado;
        }
        else
        {
            BotaoImg.sprite = LivreShare;            
            txtPRestante.enabled = false;
        }


    }
    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback: OnLogIn);
    }
    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;

        }
        else
            Debug.Log("Canceled Login");
    }
    public void Share()
    {
        FB.ShareLink(contentTitle: "Bird Attack na playstore, clique abaixo!",
            contentURL:new System.Uri("https://play.google.com/store/apps/details?id=com.EburoGames.BirdAttack"),
            contentDescription:"",
            callback:OnShare);
        
    }
    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            txtResultPanel.text = "Você não compartilhou!";
            ConfirmacaoRecebeu.SetActive(true);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            txtResultPanel.text = "Você não compartilhou!";
            ConfirmacaoRecebeu.SetActive(true);
        }
        else
        {
            //Ele compartilhou
            AtribuiBonusShare();

        }
    }
    public void AtribuiBonusShare()
    {
        PlayerPrefs.SetInt("CompartilhouFb", 1);
        PlayerPrefs.SetFloat("carnes", PlayerPrefs.GetFloat("carnes") + 200);
        txtResultPanel.text = "Recebeu mais 200 carnes!";
        ConfirmacaoRecebeu.SetActive(true);
        BotaoImg.sprite = BlockShare;
        txtPRestante.enabled = true;
        txtPRestante.text = "Partidas restantes: " + partidasNecessarias;
        partidasJogadas = 0;
        PlayerPrefs.SetInt("PartidasJogadas", 0);
    }
#if UNITY_ANDROID
    public void LikePage()
    {
        if (checkPackageAppIsPresent("com.facebook.katana"))
        {
            Application.OpenURL("fb://page/1475647839332413");
            PlayerPrefs.SetInt("FbCurtiu", 1);            
            btnLikePage.SetActive(false);
            PlayerPrefs.SetFloat("carnes", PlayerPrefs.GetFloat("carnes") + 200);
            txtResultPanel.text = "Recebeu mais 200 carnes!";
            StartCoroutine(esperaSeg());

        } else
        {
            Application.OpenURL("https://www.facebook.com/EburoGames");
            PlayerPrefs.SetInt("FbCurtiu", 1);
            btnLikePage.SetActive(false);
            PlayerPrefs.SetFloat("carnes", PlayerPrefs.GetFloat("carnes") + 200);
            txtResultPanel.text = "Recebeu mais 200 carnes!";
            
        }
    }

    private bool checkPackageAppIsPresent(string package)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        //take the list of all packages on the device
        AndroidJavaObject appList = packageManager.Call<AndroidJavaObject>("getInstalledPackages", 0);
        int num = appList.Call<int>("size");
        for (int i = 0; i < num; i++)
        {
            AndroidJavaObject appInfo = appList.Call<AndroidJavaObject>("get", i);
            string packageNew = appInfo.Get<string>("packageName");
            if (packageNew.CompareTo(package) == 0)
            {
                return true;
            }
        }
        return false;
    }


#endif

    private IEnumerator esperaSeg()
    {
        yield return new WaitForSeconds(0.3f);
        ConfirmacaoRecebeu.SetActive(true);
    }

}
