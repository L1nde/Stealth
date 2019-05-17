using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapser : MonoBehaviour {

    public GameObject remains;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.F1)) {
	        Instantiate(remains, transform.position, transform.rotation, transform.parent);
	        Destroy(gameObject);
	    }
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Player") {
            Instantiate(remains, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }


}
