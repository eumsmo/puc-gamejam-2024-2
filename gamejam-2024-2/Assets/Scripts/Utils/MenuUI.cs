using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public string sceneGame;
    public Animation animation;

    public void OnClickButtonStart(){
        StartCoroutine(StartGameCoroutine());
        
    }

    IEnumerator StartGameCoroutine(){
        MenuAudio.instance.Desaparecer();
        animation.gameObject.SetActive(true);
        animation.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneGame);
    }

    public void OnCLickQuit(){
        Application.Quit();
    }
}
