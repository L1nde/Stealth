using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolding : MonoBehaviour {

    private Holdable currentlyHeld;
    public float pickRange;
    public float maxChargeTime;
    private float currentChargeTime;
    private bool mouseButtonKeptDown;

	void Start () {
        currentChargeTime = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (currentlyHeld == null)
            tryToPick();
        else {
            Transform c = GetComponentInChildren<Camera>().transform;
            holdItem(c);
            if (Input.GetMouseButtonDown(0)) {
                mouseButtonKeptDown = true;
                currentChargeTime = Time.deltaTime;
            }
            if (mouseButtonKeptDown && currentChargeTime <= maxChargeTime) {
                currentChargeTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) && mouseButtonKeptDown) {
                mouseButtonKeptDown = false;
                throwItem(c);
            }

        }
    }

    private void tryToPick() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickRange, LayerMask.GetMask("Pickable"))) {
               pickUp(hit.collider.gameObject.GetComponent<Holdable>());
            }
        }
    }

    public void pickUp(Holdable item) {
        currentlyHeld = item;
        currentlyHeld.GetComponent<Rigidbody>().isKinematic = true;
        currentChargeTime = 0;
        currentlyHeld.GetComponent<BoxCollider>().enabled = false;
    }

    private void holdItem(Transform c) {
        currentlyHeld.transform.rotation = transform.rotation*Quaternion.Euler(new Vector3(-90,0,-90));
        currentlyHeld.transform.position = c.position + (c.forward*(1-currentChargeTime/maxChargeTime) + c.right*0.5f)*0.8f + new Vector3(0,-0.4f,0);
    }

    private void throwItem(Transform c) {
        float pow = currentChargeTime / maxChargeTime;
        if (pow > 1)
            pow = 1;
        currentlyHeld.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHeld.transform.position = c.position + (c.forward * 1.2f + c.right * 0.5f) * 0.8f;
        currentlyHeld.enableCollider();
        currentlyHeld.GetComponent<Rigidbody>().AddForce((c.forward + c.up*(0.2f*pow) - c.right*0.05f) * pow * 50, ForceMode.Impulse);
//        Debug.Log(currentlyHeld.transform.position);
        currentChargeTime = 0;

        currentlyHeld = null;
    }

}
