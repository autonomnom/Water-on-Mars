﻿using UnityEngine;
using System.Collections;

// camera, who to follow and staying smooth
public class FollowTheTarget : MonoBehaviour {

    [SerializeField] private float camDistance = 42f;
    [SerializeField] private float camUp = 10f;
    [SerializeField] private float smoothy1 = 1.2f;
    private Vector3 targetPosition;
    private Transform foollow;

    private Rigidbody body;

    void Awake() {

        findFollow();
        body = GetComponent<Rigidbody>();
    }

	void Start() {
	    
	}

    void Update() {
        
        findFollow();
    }

	void FixedUpdate () {
        
        // getting in position
        // using body.postion instead of transform.position enables camera collision, got to be adjusted though
        targetPosition = foollow.position + foollow.up * camUp - foollow.forward * camDistance;
        body.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothy1);

        // adjust rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foollow.position - transform.position, foollow.up), 0.15f);
	}

    /// <summary>
    /// <para>Find the identity to follow.</para>
    /// </summary>
    void findFollow() {

        Transform hihi = GameObject.Find(G.identitaet.ToString()).GetComponentInChildren<Seele>().transform;
        foollow = GameObject.Find(hihi.name).transform; 
    }
}
