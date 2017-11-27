using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    AsyncOperation async;
    public Camera menu;
    public Camera fps;
    /*public void StartLoading()
    {
        StartCoroutine(load());
        
    }*/
    void Begin()
    {
        StartCoroutine(load());
    }
    IEnumerator load()
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
                         "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        GameObject.Find("TitleText").GetComponent<Text>().text = "Loading...";
        SceneManager.LoadScene(1);

    }
}
