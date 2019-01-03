using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoiseMaker))]
public class Holdable : MonoBehaviour {

    private GameObject[] enemies;
    private NoiseMaker noiseMaker;

    public AudioClipGroup onHitWall;
    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        noiseMaker = GetComponent<NoiseMaker>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        float NoiseRange = collision.relativeVelocity.magnitude;
        if (NoiseRange < 3) {
            return;
        }
        changeVol(NoiseRange);
        noiseMaker.makeNoise(NoiseRange);
        noiseMaker.cancelNoise();
        onHitWall.playAtLocation(transform.position);
    }

    private void changeVol(float impactStrength) {
        float newVolume = impactStrength / 10;
        if (newVolume > 2)
            newVolume = 2;
        onHitWall.volumeMin = newVolume;
        onHitWall.volumeMax = newVolume;

    }

    public void enableCollider() {
        GetComponent<BoxCollider>().enabled = true;
    }

}
