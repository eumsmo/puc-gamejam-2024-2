using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour {
    public string tagNome;
    public UnityEvent onEnter, onExit;

    public bool destroyOnEnter = false, destroyOnExit = false;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(tagNome)) {
            onEnter?.Invoke();
        }

        if (destroyOnEnter) Destroy(gameObject);
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag(tagNome)) {
            onExit?.Invoke();
        }

        if (destroyOnExit) Destroy(gameObject);

    }
}
