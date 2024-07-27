using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quebraveis : MonoBehaviour, Avancavel {
    public GameObject onBreakSpawn;

    public void HandleAvancado(Collision collision) {
        if (onBreakSpawn != null) {
            Instantiate(onBreakSpawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
