using System;
using UnityEngine;

namespace Assets {
    public class PlayerController : MonoBehaviour {

        public Animator animator;
        private Boolean crouched = false;
        private NoiseMaker noiseMaker;
        private Rigidbody rb;
        public float speed = 1;
        public float jumpPower = 1;
        

        

        // Use this for initialization
        void Start() {
            noiseMaker = GetComponent<NoiseMaker>();
            rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetKey(KeyCode.Escape)){

                Application.Quit();
            }

            if (Input.GetKey(KeyCode.BackQuote)) {
                UnityEditor.EditorApplication.isPlaying = false; // For Linux can't quit game in editor
            }
            
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(KeyCode.A)) {
                movement = new Vector3(-speed, movement.y, movement.z);
            }

            if (Input.GetKey(KeyCode.D)) {
                movement = new Vector3(speed, movement.y, movement.z);
            }

            if (Input.GetKey(KeyCode.W)) {
                movement = new Vector3(movement.x, movement.y, speed);
            }

            if (Input.GetKey(KeyCode.S)) {
                movement = new Vector3(movement.x, movement.y, -speed);
            }

            if (isgrounded() && Input.GetKeyDown(KeyCode.Space)) {
               rb.AddForce(new Vector3(0, jumpPower, 0));
            }

            if (!crouched && Input.GetKey(KeyCode.LeftShift)) {
                movement *= 2;
            }

            if (crouched) {
                movement *= 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.C)){
                crouched = !crouched;
                if (crouched){
                    animator.SetTrigger("goSneak");
                } else {
                    animator.SetTrigger("standUp");
                }
            }
            noiseMaker.makeNoise(movement.magnitude);
            animator.SetFloat("dirX", movement.x);
            animator.SetFloat("dirZ", movement.z);
            rb.velocity = new Vector3(movement.z * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x * Mathf.Sin((90f + transform.eulerAngles.y) * Mathf.Deg2Rad), rb.velocity.y, movement.z * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x *
                                Mathf.Cos((90f + transform.eulerAngles.y) * Mathf.Deg2Rad));
        }

        private bool isgrounded() {
            return  Physics.Raycast(transform.position, -Vector3.up, 0.1f);
        }
    }
}