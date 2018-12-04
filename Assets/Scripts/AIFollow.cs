using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;

    public float chaseTime;
    public Transform[] points;

    private int destPoint = 0;
    private float currentChaseTime;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentChaseTime = 0;
        agent.autoBraking = false;
        GotoNextPoint();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (currentChaseTime <= chaseTime / 2) {
            if (IsInSight(playerPos)) {
                animator.SetTrigger("Run");
                agent.speed = 5;
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

            if (points.Length != 0) {
                agent.destination = points[destPoint].position;
            }
            else
                animator.SetTrigger("Look");
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }


    private bool IsInSight(Vector3 targetLoc)
    {
        Vector3 dirToTarget = targetLoc - transform.position;

        float d = Vector3.Dot(dirToTarget, transform.forward);
        // if player is in field of view of the bot and not behind a wall
        if (!Physics.Raycast(transform.position, dirToTarget, Vector3.Distance(transform.position, targetLoc),
            LayerMask.GetMask("Wall")) && d > 0) {
            return true;
        }
        return false;
    }

    private void GoTo(Vector3 targetLoc) {
        agent.destination = targetLoc;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        animator.SetTrigger("Walk");
        agent.speed = 2;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
}
