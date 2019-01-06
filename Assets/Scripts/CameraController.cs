﻿using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector3 offset;
	public Transform head;
	public Boolean invertedY = false;
	public Rigidbody rb;
	private float sensitivity = 1.0f;
	private Quaternion currentRotation;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position = head.position;
		float inverted = -1;
		if (invertedY) {
			inverted = 1;
		}
		float xRot = Input.GetAxis("Mouse Y") * sensitivity * inverted + transform.eulerAngles.x;
		if (xRot > 89 && xRot < 271) {
			xRot = transform.eulerAngles.x;
		}
		// transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, Mathf.Clamp(transform.rotation.w, -0.5f, 0.5f));
		
		// transform.up = Vector3.up;
		transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
		rb.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0);

	}
}
