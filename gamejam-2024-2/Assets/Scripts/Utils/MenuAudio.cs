using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour {
    public static MenuAudio instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void Desaparecer() {
        StartCoroutine(DesaparecerCoroutine());
    }

    IEnumerator DesaparecerCoroutine() {
        float transitionTime = 0.5f;
        float timer = 0;

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timer / transitionTime);
            yield return null;
        }

        MenuAudio.instance = null;
        Destroy(gameObject);
    }
}
