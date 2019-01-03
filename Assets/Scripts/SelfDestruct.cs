using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    StartCoroutine("destroy");
	}

    private IEnumerator destroy() {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
