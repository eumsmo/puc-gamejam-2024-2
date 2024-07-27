using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface MalvadoState {
    void Enter();
    void Update();
    void Exit();
}

public class NPCMalvado : MonoBehaviour {
    public GameObject targetPost, targetPlayer;
    public NavMeshAgent agent;

    public MalvadoState lastState;
    public MalvadoState currentState;
    public MalvadoSearchState searchState;
    public MalvadoStateFollow followState;
    public MalvadoStateAtirar atirarState;
    public MalvadoStateSusto sustoState;


    [Header("IA Config")]
    public float maxTimePerdeuPlayer = 3f;
    public float attackDistance = 2f;
    public float campoDeVisaoEmGraus = 45f;
    public float distanciaDeVisao = 10f;
    public LayerMask layerVisao;
    public GameObject projetilPrefab, saidaDoTiro;
    public float cooldownAfterTiro = 2f, cooldownBeforeTiro = 1f;
    public float stunTime = 2f;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        searchState = new MalvadoSearchState(this);
        followState = new MalvadoStateFollow(this);
        atirarState = new MalvadoStateAtirar(this);
        sustoState = new MalvadoStateSusto(this);
        SetState(searchState);
    }

    public void SetState(MalvadoState state) {
        currentState?.Exit();
        lastState = currentState;
        currentState = state;
        currentState?.Enter();
    }

    void Update() {
        currentState?.Update();
    }

    public void TomarSusto() {
        SetState(sustoState);
    }
}
