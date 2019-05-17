using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float Delay = -1;

	// Use this for initialization
	void Start () {
	    if (Delay > 0) {
	        StartCoroutine("destroy");
        }
	}

    private IEnumerator destroy() {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
