using System;
using Assets.Scripts;
using UnityEngine;

namespace Assets {
    public class PlayerController : MonoBehaviour {

        public Animator animator;
        private Boolean crouched = false;
        private NoiseMaker noiseMaker;
        private Rigidbody rb;
        public float walkSpeed = 1;
        public float sneakSpeed = 1;
        public float runSpeed = 1;
        public float jumpPower = 1;
        public bool isInDark = false;
        public AudioClipGroup AudioClipGroup;
        private CapsuleCollider collider;

        private float soundAcc;
        

        

        // Use this for initialization
        void Start() {
            noiseMaker = GetComponentInChildren<NoiseMaker>();
            rb = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetKey(KeyCode.Escape)){

                Application.Quit();
            }

            if (Input.GetKey(KeyCode.BackQuote)) {
                //UnityEditor.EditorApplication.isPlaying = false; // For Linux can't quit game in editor
            }
            
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            
            if (isgrounded()) {
                var j = Input.GetAxis("Jump");
               rb.AddForce(new Vector3(0, j * jumpPower, 0));
                if (j > 0) {
                    animator.SetBool("jump", true);
                } else {
                    animator.SetBool("jump", false);
                }
            }
            

            if (Input.GetKeyDown(KeyCode.C)){
                crouched = !crouched;
                if (crouched){
                    animator.SetTrigger("goSneak");
                    collider.height = 1.1f;
                    collider.center = new Vector3(collider.center.x, 0.52f, collider.center.z);
                } else {
                    animator.SetTrigger("standUp");
                    collider.height = 1.786118f;
                    collider.center = new Vector3(collider.center.x, 0.8900616f, collider.center.z);
                }
            }
            
           
            animator.SetFloat("dirX", movement.x);
            animator.SetFloat("dirZ", movement.z);
            Vector3 mov = Vector3.ClampMagnitude(new Vector3(movement.z * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x * Mathf.Sin((90f + transform.eulerAngles.y) * Mathf.Deg2Rad), 0, movement.z * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x *
                                Mathf.Cos((90f + transform.eulerAngles.y) * Mathf.Deg2Rad)), 1);
            float speed = walkSpeed;
            if (crouched) {
                speed = sneakSpeed;
            } else if (!crouched && Input.GetKey(KeyCode.LeftShift)) {
                speed = runSpeed * Input.GetAxis("Run");
            }
            rb.velocity = new Vector3(mov.x * speed, rb.velocity.y, mov.z * speed);

            noiseMaker.makeNoise(rb.velocity.magnitude);
            playSteps(speed * mov.magnitude);

        }

        private bool isgrounded() {
            return  Physics.Raycast(transform.position, -Vector3.up, 0.1f);
        }

        private void playSteps(float timesPerSec) {
            if (timesPerSec < 3f) {
                soundAcc = 2.5f;
                return;
            }

            if (soundAcc > 2.5f / timesPerSec) {
                AudioClipGroup.playAtLocation(transform.position);
                soundAcc = 0f;
            }

            soundAcc += Time.deltaTime;

        }
    }

    
}