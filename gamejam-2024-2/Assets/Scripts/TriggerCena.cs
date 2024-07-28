using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCena : MonoBehaviour {
    public string cenaNome;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(cenaNome);
        }
    }
}
