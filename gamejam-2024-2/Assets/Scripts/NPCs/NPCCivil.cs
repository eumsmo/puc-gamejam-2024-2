using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCivil : MonoBehaviour {
    NavMeshAgent agent;
    Animator animator;
    public string fugaTrigger;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
    }

    public void Fugir() {
        SafePoint point = SafePoint.GetClosestSafePoint(transform.position);
        if (point == null) {
            return;
        }

        agent.SetDestination(point.transform.position);
        // animator.SetTrigger(fugaTrigger);
    }
}
