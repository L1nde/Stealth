using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

    public bool canSeePlayer(GameObject player) {
        return isInSight(player.transform.position) ||
               isInSight(player.GetComponentInChildren<Camera>().transform.position);
    }

    public bool isInSight(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;
        float d = Vector3.Dot(dirToTarget, transform.forward);
        Debug.DrawRay(transform.position, dirToTarget * 10, Color.red);
        if (notBehindWall(targetLoc) && d > 0) {

            return true;
        }
        return false;
    }


    public bool notBehindWall(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dirToTarget, out hit, Vector3.Distance(transform.position, targetLoc))) {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "Player") {
                return true;
            }
        }
        return false;
    }
}
