using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolding : MonoBehaviour {

    private Holdable currentlyHeld;
    public float pickRange;
    public float maxChargeTime;
    public Transform hand;
    private float currentChargeTime;
    private bool mouseButtonKeptDown;
    private Animator animator;

	void Start () {
        currentChargeTime = 0;
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentlyHeld == null) {
	        animator.SetBool("throw", false);
	        animator.SetBool("hold", false);
            tryToPick();
        }
        else {
            holdItem(hand);
            var consumable = currentlyHeld.gameObject.GetComponent<Consumable>();
            if (consumable != null && !consumable.Used && Input.GetKey(KeyCode.F)) {
                animator.SetTrigger("drink");
                consumable.consume();
            }

            if (Input.GetMouseButtonDown(0)) {
                mouseButtonKeptDown = true;
                currentChargeTime = Time.deltaTime;
            }
            if (mouseButtonKeptDown && currentChargeTime <= maxChargeTime) {
                animator.SetBool("throw", true);
                currentChargeTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) && mouseButtonKeptDown) {
                mouseButtonKeptDown = false;
                throwItem(hand);
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
        animator.SetBool("hold", true);
    }

    private void holdItem(Transform c) {
        //currentlyHeld.transform.rotation = transform.rotation*Quaternion.Euler(new Vector3(-90,0,-90));
        currentlyHeld.transform.position = c.position + c.up/20 - c.forward/20;
        currentlyHeld.transform.rotation = c.rotation;
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
        //StartCoroutine("throwCoroutine", c);
    }

    private IEnumerator throwCoroutine(Holdable holdable) {
        
        yield return new WaitForSeconds(1f);
    }

}
