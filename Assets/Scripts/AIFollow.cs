using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;

    public float chaseTime;
    private float currentChaseTime;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentChaseTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (currentChaseTime <= chaseTime / 2) {
            if (IsInSight(playerPos)) {
                animator.SetBool("walking", true);
                currentChaseTime = chaseTime;
                agent.isStopped = false;
                GoTo(playerPos);
            }
        }

        if (currentChaseTime > 0) {
            GoTo(playerPos);
            currentChaseTime -= Time.deltaTime;
        }
        else if (!agent.hasPath) {
            agent.isStopped = true;
            animator.SetBool("walking", false);
        }
    }


    private bool IsInSight(Vector3 targetLoc)
    {
        Vector3 dirToTarget = targetLoc - transform.position;

        if (!Physics.Raycast(transform.position, dirToTarget, Vector3.Distance(transform.position, targetLoc),
            LayerMask.GetMask("Wall"))) {
            return true;
        }
        return false;
    }

    private void GoTo(Vector3 targetLoc) {
        agent.destination = targetLoc;
    }
}
