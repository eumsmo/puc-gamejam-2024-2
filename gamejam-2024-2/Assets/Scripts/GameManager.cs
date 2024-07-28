using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public bool cameFromMenu = true;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(gameObject);
    }

    public void Reset() {

    }

    public void GameOver() {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine() {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(UIController.instance.ShowBlackScreenCoroutine());
        yield return new WaitForSeconds(0.3f);
        cameFromMenu = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Jogo");
        yield return StartCoroutine(UIController.instance.HideBlackScreenCoroutine());
    }
}
