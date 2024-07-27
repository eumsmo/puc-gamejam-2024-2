using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { 
    Andando, Avancando, Morto
}

public class Move : MonoBehaviour {
    CharacterController controller;
    public float speed = 12f, avancarSpeed = 24f, correndoSpeed = 18f;
    public Transform modelo, meioDoModelo;
    /*[HideInInspector]*/ public PlayerState state = PlayerState.Andando;

    // Avancando
    [Header("Avançar")]
    public KeyCode avancarKey = KeyCode.Space;
    Vector3 avancandoDirection;
    public float estamina = 50f, estaminaMax = 50f;
    public float estaminaAoAvancar = 40f;
    public float estaminaPorSegundoCorrida = 3f;
    public float regenEstaminaPorSegundo = 5f;
    public float tempoAvancando = 1f;
    float avancandoTimer = 0;
    bool correndo = false;


    // Eventos
    public System.Action<float> onEstaminaChange;
    public System.Action onAvancar;
    


    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();

        onEstaminaChange += (float estamina) => {
            UIController.instance.UpdateEstamina(estamina, estaminaMax);
        };

        estamina = estaminaMax;
        onEstaminaChange?.Invoke(estamina);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(avancarKey)) {
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

    public bool Avancar() {
        if (estamina < estaminaAoAvancar) {
            return false;
        }

        state = PlayerState.Avancando;
        avancandoDirection = modelo.forward;
        avancandoTimer = tempoAvancando;
        estamina -= estaminaAoAvancar;
        onEstaminaChange?.Invoke(estamina);
        onAvancar?.Invoke();

        return true;
    }

    void Andando() {
        if (correndo) {
            if (((estamina < 0) || !Input.GetKey(avancarKey))) correndo = false;
            else {
                estamina -= estaminaPorSegundoCorrida * Time.deltaTime;
                onEstaminaChange?.Invoke(estamina);
            }
        }

        if (!correndo && estamina < estaminaMax) {
            estamina += regenEstaminaPorSegundo * Time.deltaTime;
            if (estamina > estaminaMax) {
                estamina = estaminaMax;
            }

            onEstaminaChange?.Invoke(estamina);
        }

        float speed = correndo ? this.correndoSpeed : this.speed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 1) {
            move.Normalize();
        }

        if (controller.isGrounded) {
            move.y = 0;
        } else {
            move.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(move * speed * Time.deltaTime);

        Vector3 dir = move.normalized;
        dir.y = 0;

        if (dir != Vector3.zero) {
            modelo.forward = dir;
        }
    }

    void Avancando() {
        controller.Move(avancandoDirection * avancarSpeed * Time.deltaTime);
        avancandoTimer -= Time.deltaTime;
        if (avancandoTimer <= 0) {
            avancandoTimer = 0;
            state = PlayerState.Andando;
            correndo = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (state == PlayerState.Avancando) {
            if (collision.gameObject.GetComponent<Avancavel>() != null) {
                Avancavel avancavel = collision.gameObject.GetComponent<Avancavel>();
                avancavel.HandleAvancado(collision);
                return;
            }
        }
        
        if (collision.gameObject.GetComponent<Projetil>() != null) {
            state = PlayerState.Morto;
            Destroy(collision.gameObject);
            Debug.Log("ai");
        }
    }
}
