using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebraveis : MonoBehaviour, Avancavel {
    public GameObject onBreakSpawn;
    public GameObject destroiOutraCoisaQueNaoEEu;

    public void HandleAvancado(Collision collision) {
        if (onBreakSpawn != null) {
            Instantiate(onBreakSpawn, transform.position, Quaternion.identity);
        }

        if (destroiOutraCoisaQueNaoEEu != null) {
            Destroy(destroiOutraCoisaQueNaoEEu);
        } else Destroy(gameObject);
    }
}
