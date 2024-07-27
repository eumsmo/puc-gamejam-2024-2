using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalvadoStateAtirar : MalvadoState {
    private NPCMalvado npcMalvado;
    float timer = 0f;
    bool atirou = false;

    public MalvadoStateAtirar(NPCMalvado malvado) {
        this.npcMalvado = malvado;
    }

    public void Enter() {
        npcMalvado.agent.isStopped = true;
        npcMalvado.animator.SetBool(npcMalvado.andandoBool, false);
        timer = npcMalvado.cooldownBeforeTiro;
        atirou = false;
        
    }

    public void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            if (!atirou) {
                Atirar();
            } else {
                npcMalvado.SetState(npcMalvado.followState);
            }
        }
    }

    public void Exit() {
        npcMalvado.agent.isStopped = false;
    }

    void Atirar() {
        GameObject projetil = GameObject.Instantiate(npcMalvado.projetilPrefab, npcMalvado.saidaDoTiro.transform.position, Quaternion.identity);
        Projetil projetilScript = projetil.GetComponent<Projetil>();
        Targetable targetable = npcMalvado.targetPlayer.GetComponent<Targetable>();
        projetilScript.SetTarget(targetable.meioDoModelo.position);
        atirou = true;

        npcMalvado.animator.SetTrigger(npcMalvado.acaoTrigger);
        timer = npcMalvado.cooldownAfterTiro;
    }
}
