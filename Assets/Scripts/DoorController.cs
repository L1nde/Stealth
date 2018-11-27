using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public Transform door;
    public float rate;

    private bool open = false;
    private bool close;
    private float direction = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (open) {
	        door.transform.rotation = Quaternion.RotateTowards(door.rotation, Quaternion.Euler(270f, 0f, -110f), rate * Time.deltaTime * direction);
        }
	    if (close) {
	        door.transform.rotation = Quaternion.RotateTowards(door.rotation, Quaternion.Euler(270f, 0f, 0f), rate * Time.deltaTime * direction);
	    }

    }

    public void openDoor(float direction) {
        open = true;
        this.direction = direction;
    }

    public void closeDoor() {
        close = true;
        open = false;
    }
}
