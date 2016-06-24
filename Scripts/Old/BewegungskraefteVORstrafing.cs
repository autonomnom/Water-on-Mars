﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Biografie))]

// let the identities dance!
public class BewegungskraefteVORstrafing : MonoBehaviour {

    public float walkSpeed = 13f;
    public float turnSpeed = 1.95f;
    public float jumpForce = 25f;
    public float colliderHalf = 1;

    Vector3 moveAmnount;
    Vector3 smoothMoveVelocity;
    Quaternion rotationAmount;

    // for goundcheck, needed in Xray.cs
    [HideInInspector] public bool grounded;
    public LayerMask groundMask;

    Rigidbody body;
    Biografie bio;

	void Start () {

        body = GetComponent<Rigidbody>();
        bio = GetComponent<Biografie>();
	}

    void Update() {

        if (bio.aktiv) {

            // WASD
            // forward + backwards
            Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmnount = Vector3.SmoothDamp(moveAmnount, targetMoveAmount, ref smoothMoveVelocity, .15f);

            // left + right
            Vector3 horizontalInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0).normalized;
            float desiredAngle = 49f;
            desiredAngle *= horizontalInput.x;
            rotationAmount = Quaternion.AngleAxis(desiredAngle, body.transform.up);

            // up
            if (Input.GetButtonDown("Jump")) {

                if (grounded) {
                    body.AddForce(body.transform.up * jumpForce, ForceMode.VelocityChange);
                }
            }

            grounded = false;
            Ray charles = new Ray(transform.position, -transform.up);
            RaycastHit wonder;
            if (Physics.Raycast(charles, out wonder, colliderHalf + .1f, groundMask)) {

                grounded = true;
            }
        }
    }
	
	void FixedUpdate() {

        if (bio.aktiv) {

            body.MovePosition(body.position + GetComponent<Transform>().TransformDirection(moveAmnount) * Time.fixedDeltaTime);
            body.MoveRotation(Quaternion.Slerp(body.rotation, rotationAmount * body.rotation, Time.fixedDeltaTime * turnSpeed));
        }
	}
}