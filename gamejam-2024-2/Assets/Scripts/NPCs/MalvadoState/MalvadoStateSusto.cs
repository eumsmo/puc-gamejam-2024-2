using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalvadoStateSusto : MalvadoState {
    public NPCMalvado npcMalvado;
    float stunTimer;

    public MalvadoStateSusto(NPCMalvado malvado) {
        this.npcMalvado = malvado;
    }

    public void Enter() {
        npcMalvado.agent.isStopped = true;
        stunTimer = npcMalvado.stunTime;
        // npcMalvado.animator.SetTrigger("Susto");
    }

    public void Update() {
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0) {
            MalvadoState lastState = npcMalvado.lastState;
            npcMalvado.lastState = null;
            npcMalvado.SetState(lastState);
        }
    }

    public void Exit() {
        npcMalvado.agent.isStopped = false;
    }


}
