﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public static GameLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(ToMenuSceneGame());
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        
    }
    public IEnumerator ToMenuSceneGame()
    {
        yield return new WaitForSeconds(8f);
        //yield return StartCoroutine(SplashToLoading());
        yield return StartCoroutine(LoadingToMenu());
    }
    public IEnumerator SplashToLoading()
    {
        yield return StartCoroutine(LoadYourAsyncScene("LoadingScene"));
        //yield return new WaitForSeconds(3f);
        //if (ApplovinBridge.instance.availableAOA())
        //{
        //    ApplovinBridge.instance.CheckOpenAds();
        //}
        //else
        //{

        //}
        //LoadSceneLoading();
    }
    public IEnumerator LoadingToMenu()
    {
        //yield return new WaitForSeconds(3f);
        yield return StartCoroutine(LoadYourAsyncScene("Main"));
        //ApplovinBridge.instance.CheckOpenAds();
    }
    public void MenuScene()
    {
        //if (ApplovinBridge.instance.availableAOA())
        //{
        //    ApplovinBridge.instance.CheckOpenAds();
        //}
        //else
        //{

        //}
    }
    public void LoadSceneLoading()
    {
        StartCoroutine(LoadYourAsyncScene("Loading"));
    }
    public void LoadSceneMenu()
    {
        StartCoroutine(LoadYourAsyncScene("Menu"));
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        //LoadPanel.SetActive(true);
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        //Wait until the last operation fully loads to return anything

        //if (PlayerData.LevelSurvival < 5 && PlayerData.IsCampaignUnlocked(0, 5))
        //{
        //    lbTip.text = "TIP: " + tipPool[0];
        //}
        //else
        //{
        //    lbTip.text = "TIP: " + tipPool[Random.Range(1, tipPool.Length)];
        //}

        while (!asyncLoad.isDone)
        {
            //sprLoad.width = Mathf.RoundToInt(asyncLoad.progress * 1585f);
            float percent = asyncLoad.progress * 100f;
            //lbLoad.text = percent.ToString("00.00") + "%";
            yield return null;
        }
        //LoadPanel.SetActive(false);
    }
}
