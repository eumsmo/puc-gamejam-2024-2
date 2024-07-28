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
    public static System.Action<bool> OnChaseChange;
    public static bool IsOnChase { get { return OnChase.Count > 0; }}
    static List<NPCMalvado> OnChase = new List<NPCMalvado>();
    public static void AddOnChase(NPCMalvado malvadao) {
        if (OnChase.Contains(malvadao)) return;

        OnChase.Add(malvadao);
        if (OnChase.Count == 1) OnChaseChange?.Invoke(true);
    }

    public static void RemoveOnChase(NPCMalvado malvadao) {
        if (!OnChase.Contains(malvadao)) return;

        OnChase.Remove(malvadao);
        if (OnChase.Count == 0) OnChaseChange?.Invoke(false);
    }

    public GameObject targetPost, targetPlayer;
    public NavMeshAgent agent;
    public Animator animator;

    public MalvadoState lastState;
    public MalvadoState currentState;
    public MalvadoSearchState searchState;
    public MalvadoStateFollow followState;
    public MalvadoStateAtirar atirarState;
    public MalvadoStateSusto sustoState;

    public AudioSource sustoAudio, tiroAudio;


    [Header("IA Config")]
    public float maxTimePerdeuPlayer = 3f;
    public float attackDistance = 2f;
    public float campoDeVisaoEmGraus = 45f;
    public float distanciaDeVisao = 10f;
    public LayerMask layerVisao;
    public GameObject projetilPrefab, saidaDoTiro;
    public float cooldownAfterTiro = 2f, cooldownBeforeTiro = 1f;
    public float stunTime = 2f;

    [Header("Animacoes")]
    public string acaoTrigger;
    public string sustoTrigger;
    public string andandoBool;

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
        if (state == followState) NPCMalvado.AddOnChase(this);
        else if (state == searchState) NPCMalvado.RemoveOnChase(this);
        lastState = currentState;
        currentState = state;
        currentState?.Enter();
    }

    void Update() {
        currentState?.Update();
    }

    public void TomarSusto() {
        SetState(sustoState);
        sustoAudio.Play();
    }
}
