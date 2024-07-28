using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public string sceneGame;
    public void OnClickButtonStart(){
        MenuAudio.instance.Desaparecer();
        SceneManager.LoadScene(sceneGame);
    }

    public void OnCLickQuit(){
        Application.Quit();
    }
}
