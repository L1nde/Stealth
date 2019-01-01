using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

    public bool canSeePlayer(GameObject player) {
        bool isInDark = player.GetComponent<PlayerController>().isInDark;
        return (isInSight(player.transform.position) ||
               isInSight(player.GetComponentInChildren<Camera>().transform.position)) && !isInDark;
    }

    public bool isInSight(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;
        float d = Vector3.Dot(dirToTarget, transform.forward);
        float angle = Mathf.Abs(Vector3.Angle(transform.forward, dirToTarget));
        if (notBehindWall(targetLoc) && angle <= 75f) {
            Debug.DrawRay(transform.position, dirToTarget * 10, Color.red);
            return true;
        }
        return false;
    }


    public bool notBehindWall(Vector3 targetLoc) {
        Vector3 dirToTarget = targetLoc - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dirToTarget, out hit, Vector3.Distance(transform.position, targetLoc))) {
            if (hit.collider.tag == "Player") {
                return true;
            }
        }
        return false;
    }
}
