﻿/**
 * @author © Kent Nystedt Björknäsgymansiet TE12
 */
using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	//Medlemsvariabler referenseras
	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;

	// Ändrar positionen efter siktet
	void Start () {
		targetPos = transform.position;
	}
	
	// Updaterar en gång per bildrutan ifall målet är satt
	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 10f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
			
			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);
			
		}
	}
}
