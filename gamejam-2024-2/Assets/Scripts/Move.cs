using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { 
    Andando, Avancando, Morto
}

public class Move : MonoBehaviour {
    CharacterController controller;
    public float speed = 12f, avancarSpeed = 18f;
    public Transform modelo;
    /*[HideInInspector]*/ public PlayerState state = PlayerState.Andando;

    // Avancando
    Vector3 avancandoDirection;
    public float tempoAvancando = 1f;
    float avancandoTimer = 0;


    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Avancar();
        }

        switch (state) {
            case PlayerState.Andando:
                Andando();
                break;
            case PlayerState.Avancando:
                Avancando();
                break;
            case PlayerState.Morto:
                break;
        }
    }

    public void Avancar() {
        state = PlayerState.Avancando;
        avancandoDirection = modelo.forward;
        avancandoTimer = tempoAvancando;;
    }

    void Andando() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (move != Vector3.zero) {
            modelo.forward = move.normalized;
        }
    }

    void Avancando() {
        controller.Move(avancandoDirection * avancarSpeed * Time.deltaTime);
        avancandoTimer -= Time.deltaTime;
        if (avancandoTimer <= 0) {
            avancandoTimer = 0;
            state = PlayerState.Andando;
        }
    }
}
