using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoiseListener : MonoBehaviour {
    private NavMeshAgent nav;
    private AIFollow enemAiFollow;

    void Start() {
        nav = GetComponent<NavMeshAgent>();
        enemAiFollow = GetComponent<AIFollow>();
    }

    private void OnTriggerEnter(Collider other) {
        SphereCollider c = (SphereCollider) other;
        reactToNoise(c);
        c.radius = 0;
    }

    private void OnTriggerStay(Collider other) {
        SphereCollider c = (SphereCollider)other;
        reactToNoise(c);
        c.radius = 0;
    }

    private void reactToNoise(SphereCollider other) {
        if (canHearNoise(other.transform.position,other.radius)) {
            enemAiFollow.delayedWalk(other.transform.position);
        }
    }



    private bool canHearNoise(Vector3 loc, float intensity) {
        NavMeshPath path = new NavMeshPath();
        Vector3[] allWaypoints = new Vector3[path.corners.Length + 2];
        allWaypoints[0] = transform.position;
        allWaypoints[allWaypoints.Length - 1] = loc;
        for (int i = 0; i < path.corners.Length; i++) {
            allWaypoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWaypoints.Length - 1; i++) {
            pathLength += Vector3.Distance(allWaypoints[i], allWaypoints[i + 1]);
            if (pathLength > intensity) {
                return false;
            }
        }

        return true;



    }

}
