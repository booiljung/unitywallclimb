/*
 * Unity3d Wall Climbing Demo
 * 
 * Author: Booil Ted Joung
 * Email : tedfromskyy@gmail.com
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * Wall Climbing Player Behaviour
 * 
 * */
public class PlayerBehaviour
	: MonoBehaviour
{
	// Sense wall
	public Transform hand;
	public float handDistance = 0.2f;
	
	// true if the character is on wall
	private bool climbing = false;

	
	void Start()
	{	
	}
	
		
	void Update()
	{
		float vinput = Input.GetAxis("Vertical");
		float hinput = Input.GetAxis("Horizontal");
		
		// detect wall with hand
		RaycastHit handHit = new RaycastHit();
		this.DetectHit(ref handHit, this.hand);
				
		// detect new wall for climbling
		if (!this.climbing && handHit.collider != null && Input.GetButton("Climb"))
		{
			this.climbing = true;
			this.rigidbody.useGravity = false;
		}
		// no wall or stop climbing
		else if (this.climbing && handHit.collider == null || !Input.GetButton("Climb"))
		{
			this.climbing = false;
			this.rigidbody.useGravity = true;
		}
		
		// Jump
		if (Input.GetButtonDown("Jump"))
		{
			this.climbing = false;
			this.rigidbody.useGravity = true;
			this.rigidbody.AddForce(0f, 300f, 0f);
		}
		
		// climbling action.
		if (this.climbing)
		{
			this.rigidbody.velocity = this.transform.up * vinput * 5f + this.transform.right * hinput * 5f;
			this.rigidbody.angularVelocity = Vector3.zero;
			this.transform.rotation = Quaternion.LookRotation(handHit.normal*-1f);
		}
		// walking or jump action
		else
		{
			this.rigidbody.velocity = this.rigidbody.velocity.y * this.transform.up + this.transform.forward * vinput * 5f;
			this.rigidbody.angularVelocity = new Vector3(0f, hinput * 2f, 0f);
		}
	}
	
	
	// detect raycat hit
	void DetectHit(ref RaycastHit detectedHit, Transform transform)
	{
		RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, this.handDistance);
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider == this.collider)
				continue;
			if (hit.collider.isTrigger)
				continue;
			if (detectedHit.collider == null || hit.distance < detectedHit.distance)
				detectedHit = hit;
		}
	}
		
	
	// usage
	void OnGUI()
	{
		GUI.TextField(new Rect(0f, 0f, 200f, 100f), "up, down, left, right: move, rotation\nleft shift: climbing\n");
	}
	
	
	// draw hand on unity3d scene
	void OnDrawGizmos()
	{
		foreach (Transform hand in this.hand)
        	Gizmos.DrawLine(hand.position, hand.position + hand.forward * this.handDistance);
    }	
}
