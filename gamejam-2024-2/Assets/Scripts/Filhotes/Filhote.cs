using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum FilhoteState {
    SLEEP, FOLLOWING, DISTRACTED, DEAD
}

public class Filhote : MonoBehaviour {
    public FilhoteState state = FilhoteState.SLEEP;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public Transform distraction;
    float distractionOffset = 1.5f;

    public float followSafeDistance = 3f;
    public float dormirPraAcordadoDist = 2f;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("A mimir", true);
        animator.SetBool("Andando", false);
    }

    void Update() {
        Vector3 followPos;
        Vector3 dir;

        switch (state) {
            case FilhoteState.SLEEP:
                break;
            case FilhoteState.FOLLOWING:
                followPos = player.position;
                dir = (transform.position - followPos).normalized;
                followPos += dir * 0.5f;
                agent.SetDestination(followPos);
                break;
            case FilhoteState.DISTRACTED:
                followPos = distraction.position;
                dir = (transform.position - followPos).normalized;
                followPos += dir * distractionOffset;
                agent.SetDestination(followPos);
                break;
        }

        if (state == FilhoteState.SLEEP || state == FilhoteState.DISTRACTED) {
            Collider[] closeObjs = Physics.OverlapSphere(transform.position, dormirPraAcordadoDist);
            foreach (Collider obj in closeObjs) {
                if (obj.CompareTag("Player")) {
                    SetPlayer(obj.transform);
                    break;
                }
            }
        }
    }

    public void SetPlayer(Transform player) {
        this.player = player;
        state = FilhoteState.FOLLOWING;
        animator.SetBool("A mimir", false);
        animator.SetBool("Andando", true);
    }

    public void TryToDistract(Transform distraction, float distractOffset = 1.5f) {
        if (state == FilhoteState.SLEEP || state == FilhoteState.DEAD) {
            return;
        } else if (state == FilhoteState.FOLLOWING) {
            if (Vector3.Distance(player.position, transform.position) <= followSafeDistance) {
                return;
            }
        }

        this.distraction = distraction;
        this.distractionOffset = distractOffset;
        state = FilhoteState.DISTRACTED;
        animator.SetBool("A mimir", false);
        animator.SetBool("Andando", true);

    }

    public void Die() {
        state = FilhoteState.DEAD;
        animator.SetBool("A mimir", false);
        animator.SetBool("Andando", false);
        animator.SetTrigger("Morrer");

        agent.enabled = false;
    }
}
