﻿using UnityEngine;
using System.Collections;

// camera, who to follow and staying smooth
public class FollowTheTarget : MonoBehaviour {

    [SerializeField] private float camDistance = 42f;
    [SerializeField] private float camUp = 10f;
    [SerializeField] private float smoothy1 = 1.2f;
    private Vector3 targetPosition;
    private Transform foollow;

    void Awake() {

        findFollow();
    }

	void Start() {
	    
	}

    void Update() {
        
        findFollow();
    }

	void FixedUpdate () {
        
        // Maybe got to split this values up to get independent rotation/adjusting speed 
        // for vertical and horizontal.

        //getting in position
        targetPosition = foollow.position + foollow.up * camUp - foollow.forward * camDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothy1);

        //adjust rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(foollow.position - transform.position, foollow.up), 0.15f);
	}

    /// <summary>
    /// <para>Find the identity to follow.</para>
    /// <value>index : the index of child in the parents childlist, has to be of type "Seele".</value>
    /// </summary>
    void findFollow(int index = 0) {

        Transform hihi = GameObject.Find(G.identitaet.ToString()).transform.GetChild(index);
        foollow = GameObject.Find(hihi.name).transform; 
    }
}
