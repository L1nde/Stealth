using Assets.Scripts;
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

    public AudioClipGroup soundOnNoticePlayer;

    private Vector3 spawnPos;
    private int destPoint = 0;
    private float currentChaseTime;
    private bool reactedToNoise;
    private bool followingPlayer;

    private bool hasToTurnOnSwitch;
    private LightSwitch switchToTurnOn;

    // Use this for initialization
    void Start() {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentChaseTime = 0;

        followingPlayer = false;
        agent.autoBraking = false;
        hasToTurnOnSwitch = false;

        GotoNextPoint();
        spawnPos = transform.position;
        eye = GetComponentInChildren<Eye>();
    }

    void Update() {
        /* if sees player -> try to catch player
         * While chaseTime > 0 -> update goto pos
         * Once chaseTime <= 0 
         *      resume turning the light on
         *      go to the next waypoint
         *      go back to spawn
         */
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;


        if (eye.canSeePlayer(player)) {
            followPlayer(playerPos);
            currentChaseTime -= Time.deltaTime;
        } else if (currentChaseTime > 0) {
            GoTo(playerPos);
            currentChaseTime -= Time.deltaTime;

        } else {
            if (followingPlayer) {
                followingPlayer = false;
                animator.ResetTrigger("Walk");
                animator.ResetTrigger("Run");
                animator.SetTrigger("Walk");
                agent.speed = 2;
            }
            if (hasToTurnOnSwitch)
                tryToTurnOnSwitch();
            else if ((points.Length != 0 && !agent.hasPath) || (points.Length != 0 && !agent.pathPending && agent.remainingDistance < 1f))
                GotoNextPoint();
            else if (points.Length == 0 && !reactedToNoise) 
                goToSpawn();
            
            
        }

    }

    private void followPlayer(Vector3 playerPos) {
        if (!followingPlayer) {
            soundOnNoticePlayer.playAtLocation(transform.position);
            animator.ResetTrigger("Walk");
            animator.ResetTrigger("Run");
            animator.SetTrigger("Run");
            agent.speed = 7;
            agent.isStopped = false;
            followingPlayer = true;
        }
        if (Vector3.Distance(playerPos, transform.position) < 1)
            GameController.instance.lose();
        
        currentChaseTime = chaseTime;
        GoTo(playerPos);
    }


    private void goToSpawn() {

        if (Vector3.Distance(spawnPos, transform.position) > 1f) {
            GoTo(spawnPos);
        }
        else {
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
                yield return new WaitForSeconds(1f);
            }
            walkTo(targetLoc);
        }
        reactedToNoise = false;
    }

    public void delayedWalk(Vector3 targetLoc) {
        StartCoroutine("delayedWalkCoroutine", targetLoc);
    }

    private void tryToTurnOnSwitch() {
        if (switchToTurnOn.isTurnedOn) {
            hasToTurnOnSwitch = false;
            return;
        }
        Vector3 sPos = switchToTurnOn.transform.position;
        if (Vector3.Distance(sPos, transform.position) < 4) {
            hasToTurnOnSwitch = false;
            switchToTurnOn.interact();
        } else {
            animator.SetTrigger("Walk");
            agent.speed = 2;
            GoTo(sPos);
        }
        
    }


    void GotoNextPoint() {
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

    public void turnOnTheSwitch(LightSwitch lightSwitch) {
        hasToTurnOnSwitch = true;
        switchToTurnOn = lightSwitch;
    }


}
