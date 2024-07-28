using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadilha : MonoBehaviour {
    public Animator animator;
    public string triggerTrigger;
    bool triggered = false;

    void OnTriggerEnter(Collider other) {
        if (triggered) return;
        
        bool isTriggering = false;

        if (other.CompareTag("Player")) {
            other.GetComponent<Move>().Morre();
            isTriggering = true;
        } else if (other.CompareTag("Filhote")) {
            other.GetComponent<Filhote>().Die();
            isTriggering = true;
        }

        if (isTriggering) {
            if (animator != null) animator.SetTrigger(triggerTrigger);
            triggered = true;
        }
    }
}
