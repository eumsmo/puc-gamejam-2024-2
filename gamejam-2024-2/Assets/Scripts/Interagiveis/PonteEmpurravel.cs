using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteEmpurravel : MonoBehaviour, Avancavel {
    public string derrubarTrigger;
    public Animator animator;
    bool derrubada = false;

    public void HandleAvancado(Collision collision) {
        if (derrubada) {
            return;
        }

        animator.SetTrigger(derrubarTrigger);
        derrubada = true;
    }
}
