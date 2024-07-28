using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoEmArea : MonoBehaviour {
    bool triggered = false;
    void OnTriggerEnter(Collider other) {
        if (triggered) return;


        if (other.CompareTag("Player")) {
            other.GetComponent<Move>().Morre();
            triggered = true;
        }
    }
}
