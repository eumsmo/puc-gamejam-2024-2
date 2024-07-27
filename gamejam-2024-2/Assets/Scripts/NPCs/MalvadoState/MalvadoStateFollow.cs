using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalvadoStateFollow : MalvadoState {
    private NPCMalvado npcMalvado;
    float lostTimer = 0f;

    public MalvadoStateFollow(NPCMalvado malvado) {
        this.npcMalvado = malvado;
    }

    public void Enter() {
        lostTimer = npcMalvado.maxTimePerdeuPlayer;
    }

    public void Update() {
        npcMalvado.agent.SetDestination(npcMalvado.targetPlayer.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(npcMalvado.transform.position, npcMalvado.targetPlayer.transform.position - npcMalvado.transform.position, out hit)) {
            if (hit.transform.CompareTag("Player")) {
                lostTimer = npcMalvado.maxTimePerdeuPlayer;
            } else {
                lostTimer -= Time.deltaTime;
                if (lostTimer <= 0) {
                    npcMalvado.SetState(npcMalvado.searchState);
                }
            }
        }

        if (Vector3.Distance(npcMalvado.transform.position, npcMalvado.targetPlayer.transform.position) <= npcMalvado.attackDistance) {
            npcMalvado.SetState(npcMalvado.atirarState);
        }

    }

    public void Exit() {
        npcMalvado.agent.ResetPath();
    }
}
