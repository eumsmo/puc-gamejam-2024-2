using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour {
    public float speed = 10f;
    public Vector3 direction;

    void Start() {
        Destroy(gameObject, 5f);
    }

    void Update() {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetTarget(Vector3 target) {
        direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Move>().Morre();
        }

        Destroy(gameObject);
    }
}
