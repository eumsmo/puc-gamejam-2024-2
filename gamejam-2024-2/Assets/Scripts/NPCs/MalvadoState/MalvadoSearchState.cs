using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalvadoSearchState : MalvadoState {
    private NPCMalvado npcMalvado;
    float waitingTime = 0;

    public enum SearchState { WalkToPost, WaitInPost }
    public SearchState searchState = SearchState.WalkToPost;

    public MalvadoSearchState(NPCMalvado npcMalvado) {
        this.npcMalvado = npcMalvado;
    }

    public void Enter() {
        if (npcMalvado.agent.destination == null) {
            npcMalvado.agent.SetDestination(npcMalvado.targetPost.transform.position);
        }
    }

    public void Update() {
        if (searchState == SearchState.WaitInPost) {
            waitingTime -= Time.deltaTime;
            if (waitingTime <= 0) {
                searchState = SearchState.WalkToPost;
                npcMalvado.targetPost = npcMalvado.targetPost.GetComponent<NPCPost>().nextPost.gameObject;
                npcMalvado.agent.SetDestination(npcMalvado.targetPost.transform.position);
            }
        }

        if (searchState == SearchState.WalkToPost && npcMalvado.agent.remainingDistance <= npcMalvado.agent.stoppingDistance) {
            NPCPost post = npcMalvado.targetPost.GetComponent<NPCPost>();
            waitingTime = post.stopInPost;
            searchState = SearchState.WaitInPost;
        }

        Transform[] vistos = LineOfSight.Calculate(npcMalvado.saidaDoTiro.transform, npcMalvado.campoDeVisaoEmGraus, npcMalvado.distanciaDeVisao, npcMalvado.layerVisao);
        float menorDistancia = float.MaxValue;
        Transform alvo = null;
        foreach (Transform visto in vistos) {
            if (!visto.CompareTag("Player")) continue;
            float distancia = Vector3.Distance(npcMalvado.saidaDoTiro.transform.position, visto.position);
            if (distancia < menorDistancia) {
                menorDistancia = distancia;
                alvo = visto;
            }
        }

        if (alvo != null) {
            npcMalvado.targetPlayer = alvo.gameObject;
            npcMalvado.SetState(npcMalvado.followState);
        }
    }

    public void Exit() {
        npcMalvado.agent.ResetPath();
    }
}
