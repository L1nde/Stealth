using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;

    public float chaseTime;
    public Transform[] points;

    private Vector3 spawnPos;
    private int destPoint = 0;
    private float currentChaseTime;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentChaseTime = 0;
        agent.autoBraking = false;
        GotoNextPoint();
        spawnPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (currentChaseTime <= chaseTime / 2) {
            if (IsInSight(playerPos)) 
                followPlayer(playerPos);
        }

        if (currentChaseTime > 0) {
            GoTo(playerPos);
            currentChaseTime -= Time.deltaTime;
        }

        // Get new Pos to go to
        else if (!agent.hasPath) {

            if (points.Length != 0) {
                GotoNextPoint();
            }
            else {
                goToSpawn();
            }
        }
        // Upon reaching the target pos
        else if (!agent.pathPending && agent.remainingDistance < 1f)
            if (points.Length == 0) {
                goToSpawn();
            }
            else
                GotoNextPoint();
    }

    private void followPlayer(Vector3 playerPos) {
        animator.SetTrigger("Run");
        agent.speed = 5;
        currentChaseTime = chaseTime;
        agent.isStopped = false;
        GoTo(playerPos);
    }


    private void goToSpawn() {
        agent.speed = 2;
        animator.ResetTrigger("Run");
        if (Vector3.Distance(spawnPos, transform.position) > 1f) { 
            animator.SetTrigger("Walk");
            GoTo(spawnPos);
        } else {
            agent.destination = transform.position;
            animator.SetTrigger("Look");
        }
    }

    public void GoTo(Vector3 targetLoc) {
        agent.destination = targetLoc;
    }

    public void walkTo(Vector3 targetLoc) {
        animator.SetTrigger("Walk");
        GoTo(targetLoc);
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

    public bool IsInSight(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;
        float d = Vector3.Dot(dirToTarget, transform.forward);

        if (notBehindWall(targetLoc) && d > 0) {
            return true;
        }
        return false;
    }

    public bool notBehindWall(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;

        if (!Physics.Raycast(transform.position, dirToTarget, Vector3.Distance(transform.position, targetLoc), LayerMask.GetMask("Wall"))) {
            return true;
        }
        return false;
    }
}
