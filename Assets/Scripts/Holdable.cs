using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour {

    private GameObject[] enemies;

    public AudioClipGroup onHitWall;
    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        float NoiseRange = collision.relativeVelocity.magnitude;
        
        if (NoiseRange < 3)
            return;
        else {
            changeVol(NoiseRange);
            onHitWall.playAtLocation(transform.position);
            AIFollow closestEnemy = getClosestEnemyToImpact(NoiseRange);
            if (closestEnemy == null)
                return;
            closestEnemy.walkTo(transform.position);

        }
    }

    private void changeVol(float impactStrength) {
        float newVolume = impactStrength / 10;
        if (newVolume > 2)
            newVolume = 2;
        onHitWall.volumeMin = newVolume;
        onHitWall.volumeMax = newVolume;

    }

    private AIFollow getClosestEnemyToImpact(float range) {
        AIFollow closestEnemy = null;
        float closestDist = range;
        for (int i = 0; i < enemies.Length; i++) {
            AIFollow enemy = enemies[i].GetComponent<AIFollow>();
            if (enemy == null || !enemy.notBehindWall(transform.position))
                continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist) {
                closestDist = dist;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }


    public void enableCollider() {
        GetComponent<BoxCollider>().enabled = true;
    }

}
