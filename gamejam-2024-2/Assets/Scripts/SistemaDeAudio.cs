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
        if (dif) StartChase();
        else StopChase();
    }

    public IEnumerator StartChaseCoroutine() {
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
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.volume = 0;
        newAudio.loop = true;
        newAudio.Play();

        StartCoroutine(TransitionBetween(musicaFundo, newAudio, () => {
            Destroy(musicaFundo);
            musicaFundo = newAudio;
        }));
    }

    public void ChangeChaseAudio(AudioClip clip) {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.volume = 0;
        newAudio.loop = true;
        newAudio.Play();

        StartCoroutine(TransitionBetween(adicionalChaseMusica, newAudio, () => {
            Destroy(adicionalChaseMusica);
            adicionalChaseMusica = newAudio;
        }));
    }

    IEnumerator TransitionBetween(AudioSource a, AudioSource b, System.Action afterCallback = null) {
        float transitionTime = 2f;
        float timer = 0;
        float targetVolume = a.volume;

        while (timer < transitionTime) {
            timer += Time.deltaTime;
            a.volume = Mathf.Lerp(targetVolume, 0, timer / transitionTime);
            b.volume = Mathf.Lerp(0, targetVolume, timer / transitionTime);
            yield return null;
        }

        a.Stop();

        if (afterCallback != null) {
            afterCallback();
        }
    }
}
