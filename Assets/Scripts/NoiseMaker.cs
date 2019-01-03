using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour {

    private SphereCollider noise;

	// Use this for initialization
	void Start () {
	    noise = gameObject.AddComponent<SphereCollider>();
//	    noise.center += transform.up * 1.5f;
	    noise.radius = 0f;
	    noise.isTrigger = true;
        
	}

    public void makeNoise(float amount) {
         noise.radius = amount;
    }

    public void cancelNoise() {
        StopCoroutine("cancel");
        StartCoroutine("cancel");
    }

    private IEnumerator cancel() {
        yield return new WaitForSeconds(0.1f);
        noise.radius = 0;
    }
}
