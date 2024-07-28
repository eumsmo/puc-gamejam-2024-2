using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    public Slider estaminaSlide;
    public Image telaPreta;
    public Animation animation;
    public GameObject pausePanel;

    void Awake() {
        instance = this;
    }

    void Start() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Jogo") {
            if (GameManager.instance.cameFromMenu)
                ShowFire();
            else
                StartCoroutine(HideBlackScreenCoroutine());
        }
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pausePanel.activeSelf) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void UpdateEstamina(float atual, float max) {
        estaminaSlide.value = atual / max;
    }

    public IEnumerator ShowBlackScreenCoroutine() {
        float transitionTime = 1f;
        float timer = 0;
        telaPreta.color = new Color(0, 0, 0,0);
        telaPreta.gameObject.SetActive(true);

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            telaPreta.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timer / transitionTime));
            yield return null;
        }
    }

    public IEnumerator HideBlackScreenCoroutine() {

        float transitionTime = 1f;
        float timer = 0;
        telaPreta.color = new Color(0, 0, 0, 1);
        telaPreta.gameObject.SetActive(true);

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            telaPreta.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timer / transitionTime));
            yield return null;
        }

        telaPreta.gameObject.SetActive(false);
    }

    public void ShowFire() {
        animation.gameObject.SetActive(true);
        animation.Play();
    }

    public void Pause() {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
    }

    public void Menu() {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
    }
}
