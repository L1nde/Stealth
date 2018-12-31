using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator animator;

    public float chaseTime;
    public Transform[] points;
    public Eye eye;

    private Vector3 spawnPos;
    private int destPoint = 0;
    private float currentChaseTime;
    private bool reactedToNoise;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentChaseTime = 0;
        agent.autoBraking = false;
        GotoNextPoint();
        spawnPos = transform.position;
        eye = GetComponentInChildren<Eye>();
    }
	
	// Update is called once per frame
	void Update () {
	    var player = GameObject.FindGameObjectWithTag("Player");
	    Vector3 playerPos = player.transform.position;

        if (currentChaseTime <= chaseTime / 2) {
            if (eye.canSeePlayer(player)) 
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

    private IEnumerator delayedWalkCoroutine(Vector3 targetLoc) {
        if (currentChaseTime <= 0 && !reactedToNoise) {
            reactedToNoise = true;
            for (float i = 2; i > 0; i--) {
                Debug.Log(reactedToNoise);
                yield return new WaitForSeconds(1f);
            }
            walkTo(targetLoc);
        }
        reactedToNoise = false;
    }

    public void delayedWalk(Vector3 targetLoc) {
        StartCoroutine("delayedWalkCoroutine", targetLoc);
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

    public bool notBehindWall(Vector3 target) {
        return eye.notBehindWall(target);
    }


}
