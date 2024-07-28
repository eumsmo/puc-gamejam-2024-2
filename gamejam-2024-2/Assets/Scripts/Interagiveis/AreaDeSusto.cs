using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaDeSusto : MonoBehaviour {
    bool dentro = false;
    public UnityEvent onSusto;

    void OnTriggerEnter(Collider other) {
        if (dentro) {
            return;
        }

        if (other.CompareTag("Player")) {
            dentro = true;
            Move move = other.GetComponent<Move>();
            move.onAvancar += HandleAvancado;

            if (move.state == PlayerState.Avancando) HandleAvancado();
        }
    }

    void OnTriggerExit(Collider other) {
        if (!dentro) {
            return;
        }

        if (other.CompareTag("Player")) {
            dentro = false;
            Move move = other.GetComponent<Move>();
            move.onAvancar -= HandleAvancado;
        }
    }

    void HandleAvancado() {
        if (dentro) {
            onSusto?.Invoke();
        }
    }
}
