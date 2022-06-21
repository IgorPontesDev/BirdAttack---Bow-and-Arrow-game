using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour {
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
	// Use this for initialization
	void Start () {
        GameIsPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlayerPrefs.SetInt("TemUpgradeArco", 0);
        PlayerPrefs.SetInt("TemUpgradeCasa", 0);
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PlayerPrefs.SetInt("TemUpgradeArco", 0);
        PlayerPrefs.SetInt("TemUpgradeCasa", 0);
    }

    public void AbreMenu()
    {
        PlayerPrefs.SetInt("TemUpgradeArco", 0);
        PlayerPrefs.SetInt("TemUpgradeCasa", 0);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
       
    }
    public void clicouEvoluir()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ClicouEvoluir", 1);
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("TemUpgradeArco", 0);
        PlayerPrefs.SetInt("TemUpgradeCasa", 0);
        Application.Quit();
    }
    public void ApertouPause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
