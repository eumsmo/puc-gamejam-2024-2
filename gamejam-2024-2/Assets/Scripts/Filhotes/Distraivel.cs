using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraivel : MonoBehaviour {
    public float raioDeDistracao = 3f;
    public LayerMask layerDoFilhote;

    void FixedUpdate() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, raioDeDistracao, layerDoFilhote);
        foreach (Collider collider in colliders) {
            Filhote filhote = collider.GetComponent<Filhote>();
            if (filhote != null) {
                filhote.TryToDistract(transform);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioDeDistracao);
    }
}
