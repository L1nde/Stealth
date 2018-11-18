using System;
using UnityEngine;

namespace Assets {
    public class PlayerController : MonoBehaviour {

        public Animator animator;
        private Boolean crouched = false;
        private CharacterController controller;
        public float speed = 1;
        public float jumpPower = 1;
        private float jumpAlt = 0;

        

        // Use this for initialization
        void Start() {
            controller = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update() {
            if(Input.GetKey(KeyCode.Escape)){
                UnityEditor.EditorApplication.isPlaying = false; // For Linux can't quit game in editor
                Application.Quit();
            }
            
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(KeyCode.A)) {
                movement = new Vector3(-1, movement.y, movement.z);
            }

            if (Input.GetKey(KeyCode.D)) {
                movement = new Vector3(1, movement.y, movement.z);
            }

            if (Input.GetKey(KeyCode.W)) {
                movement = new Vector3(movement.x, movement.y, 1);
            }

            if (Input.GetKey(KeyCode.S)) {
                movement = new Vector3(movement.x, movement.y, -1);
            }

            if (controller.isGrounded && Input.GetKey(KeyCode.Space)) {
                jumpAlt = jumpPower;
            }
            else if (!controller.isGrounded) {
                jumpAlt += Physics.gravity.y * Time.deltaTime;
                movement.y = jumpAlt;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                crouched = !crouched;
                if (crouched){
                    animator.SetTrigger("goSneak");
                } else {
                    animator.SetTrigger("standUp");
                }
                
            }
            animator.SetFloat("speed", Mathf.Sqrt(Mathf.Pow(movement.x, 2) + Mathf.Pow(movement.z, 2)));
            
            controller.Move(new Vector3(movement.z * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x *
                                        Mathf.Sin(
                                            (90f + transform.eulerAngles.y) * Mathf.Deg2Rad), movement.y,
                                movement.z * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad) + movement.x *
                                Mathf.Cos(
                                    (90f + transform.eulerAngles.y) * Mathf.Deg2Rad)).normalized * speed *
                            Time.deltaTime);
        }
    }
}