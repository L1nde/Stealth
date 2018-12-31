using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark : MonoBehaviour {

    private PlayerController player;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            player.isInDark = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
            player.isInDark = false;
    }

}
