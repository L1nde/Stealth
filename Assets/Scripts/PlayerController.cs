using System;
using UnityEngine;

namespace Assets {
    public class PlayerController : MonoBehaviour {
        private CharacterController controller;

        private float sensitivity = 1.0f;

        public Boolean invertedY = false;
        public float speed = 1;
        public float jumpPower = 1;
        private float jumpAlt = 0;

        public Camera camera;

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

            float inverted = -1;
            if (invertedY) {
                inverted = 1;
            }

            float xRot = Input.GetAxis("Mouse Y") * sensitivity * inverted + camera.transform.eulerAngles.x;
            if (xRot > 89 && xRot < 271) {
                xRot = camera.transform.eulerAngles.x;
            }


            camera.transform.eulerAngles =
                new Vector3(xRot, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);

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