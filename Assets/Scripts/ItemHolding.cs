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

    void Start() {
        currentChargeTime = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (currentlyHeld == null) {
            animator.SetBool("throw", false);
            animator.SetBool("hold", false);
            UIController.instance.disableInteractable2();
            tryToPick();
        }
        else {
            holdItem(hand);
            var consumable = currentlyHeld.gameObject.GetComponent<Consumable>();
            if (consumable != null && !consumable.Used) {
                if (Input.GetButtonDown(consumable.Key)) {
                    if (!(consumable is Flashlight)) {
                        animator.SetTrigger("drink");
                    }
                    consumable.consume();
                } 
                UIController.instance.enableInteractable2(consumable.Key, consumable.Description);
            }
            else {
                UIController.instance.disableInteractable2();
            }
            

            if (Input.GetButtonDown("Interact0")) {
                mouseButtonKeptDown = true;
                currentChargeTime = Time.deltaTime;
            }

            if (mouseButtonKeptDown && currentChargeTime <= maxChargeTime) {
                animator.SetBool("throw", true);
                currentChargeTime += Time.deltaTime;
            }

            if (Input.GetButtonUp("Interact0") && mouseButtonKeptDown) {
                mouseButtonKeptDown = false;
                throwItem(hand);
            }
        }
    }


    private void tryToPick() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickRange,
            LayerMask.GetMask("Pickable"))) {
            if (Input.GetButtonDown("Interact0")) {
                pickUp(hit.collider.gameObject.GetComponent<Holdable>());
                UIController.instance.disableInteractablePic();
            }
            UIController.instance.enableInteractablePic();
        }
        else {
            UIController.instance.disableInteractablePic();
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
        var holdingPlace = currentlyHeld.GetComponentInChildren<Hold>();
        if (holdingPlace != null) {
            currentlyHeld.transform.position = c.position;
            currentlyHeld.transform.position =
                currentlyHeld.transform.TransformPoint(holdingPlace.transform.localPosition);
            currentlyHeld.transform.localRotation = holdingPlace.transform.localRotation;
        }
        else {
            currentlyHeld.transform.position = c.position;
            currentlyHeld.transform.rotation = c.rotation;
        }

        currentlyHeld.transform.parent = c;
    }

    private void throwItem(Transform c) {
        float pow = currentChargeTime / maxChargeTime;
        if (pow > 1)
            pow = 1;
        currentlyHeld.GetComponent<Rigidbody>().isKinematic = false;
        currentlyHeld.transform.parent = null;
        currentlyHeld.enableCollider();
        currentlyHeld.GetComponent<Rigidbody>()
            .AddForce(
                (Camera.main.transform.forward + Camera.main.transform.up * (0.2f * pow) -
                 Camera.main.transform.right * 0.05f) * pow * 50, ForceMode.Impulse);
        currentChargeTime = 0;
        currentlyHeld = null;
    }
}