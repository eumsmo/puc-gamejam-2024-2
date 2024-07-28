using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour {
    public string tagNome;
    public UnityEvent onEnter, onExit;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(tagNome)) {
            onEnter?.Invoke();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag(tagNome)) {
            onExit?.Invoke();
        }
    }
}
