using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeAudio : MonoBehaviour {
    public AudioSource musicaFundo;
    public AudioSource adicionalChaseMusica;

    void Start() {
        musicaFundo.Play();

        if (adicionalChaseMusica != null) {
            adicionalChaseMusica.Play();
            adicionalChaseMusica.volume = 0;
        }

        NPCMalvado.OnChaseChange += OnChaseChange;
        if (NPCMalvado.IsOnChase) StartChase();
    }

    void OnDestroy() {
        NPCMalvado.OnChaseChange -= OnChaseChange;
    }

    void OnChaseChange(bool dif) {
        Debug.Log(dif);
        if (dif) StartChase();
        else StopChase();
    }

    public IEnumerator StartChaseCoroutine() {
        Debug.Log(adicionalChaseMusica);
        if (adicionalChaseMusica == null) {
            yield break;
        }

        float transitionTime = 0.5f;
        float timer = 0;

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            adicionalChaseMusica.volume = Mathf.Lerp(0, 1, timer / transitionTime);
            yield return null;
        }
    }

    public void StartChase() {
        StartCoroutine(StartChaseCoroutine());
    }

    public IEnumerator StopChaseCoroutine() {
        if (adicionalChaseMusica == null) {
            yield break;
        }

        float transitionTime = 0.5f;
        float timer = 0;

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            adicionalChaseMusica.volume = Mathf.Lerp(1, 0, timer / transitionTime);
            yield return null;
        }
    }

    public void StopChase() {
        StartCoroutine(StopChaseCoroutine());
    }

    public void ChangeAudio(AudioClip clip) {
        musicaFundo.clip = clip;
        musicaFundo.Play();
    }

    public void ChangeChaseAudio(AudioClip clip) {
        adicionalChaseMusica.clip = clip;
        adicionalChaseMusica.Play();
        adicionalChaseMusica.volume = 0;
    }
}
