using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPost : MonoBehaviour {
    public Transform nextPost;
    public float stopInPost = 3.0f;

    void OnDrawGizmosSelected() {
        if (nextPost != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nextPost.position);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
