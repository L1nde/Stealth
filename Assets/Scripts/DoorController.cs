using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class DoorController : Interactable {

    public float rate;

    private bool opened = false;
    private bool closed = true;
    private float direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (opened) {
	        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(180f, 0f, 110f), rate * Time.deltaTime * direction);
        }
	    if (closed) {
	        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(180f, 0f, 0f), rate * Time.deltaTime * direction);
        }

    }

    public override void interact() {
        if (opened) {
            closeDoor();
        } else if (closed) {
            openDoor();
        }
    }

    private void openDoor() {
        opened = true;
        closed = false;
        this.direction = 1f;
    }

    private void closeDoor() {
        closed = true;
        opened = false;
    }
}
