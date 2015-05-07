﻿using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

[RequireComponent (typeof (CharacterController))]
public class EnhancedFPSCharacterController : MonoBehaviour 
{
	
	public float walkSpeed = 6.0f;
	
	public float runSpeed = 11.0f;
	
	// If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
	public bool limitDiagonalSpeed = true;
	
	// If checked, the run key toggles between running and walking. Otherwise player runs if the key is held down and walks otherwise
	// There must be a button set up in the Input Manager called "Run"
	public bool toggleRun = false;
	
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	
	// Units that player can fall before a falling damage function is run. To disable, type "infinity" in the inspector
	public float fallingDamageThreshold = 10.0f;
	
	// If checked, then the player can change direction while in the air
	public bool airControl = false;
	
	// Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
	public float antiBumpFactor = .75f;
	
	// Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
	public int antiBunnyHopFactor = 1;
	

	private Vector3 moveDirection = Vector3.zero;
	private bool grounded = false;
	private CharacterController controller;
	private Transform myTransform;
	private float speed;
	private RaycastHit hit;
	private float fallStartLevel;
	private bool falling;
	private float rayDistance;
	private Vector3 contactPoint;
	private bool playerControl = false;
	private int jumpTimer;
	
	void Start() 
    {
		controller = GetComponent<CharacterController>();
		myTransform = transform;
		speed = walkSpeed;
		rayDistance = controller.height * .5f + controller.radius;
		jumpTimer = antiBunnyHopFactor;
	}
	
	void FixedUpdate() 
    {
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");
		// If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
		float inputModifyFactor = (inputX != 0.0f && inputZ != 0.0f && limitDiagonalSpeed)? .7071f : 1.0f;

		if (grounded) 
        {
			
			// If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
			if (falling) 
            {
				falling = false;
				if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                {
                    FallingDamageAlert(fallStartLevel - myTransform.position.y);
                }					
			}
			
			// If running isn't on a toggle, then use the appropriate speed depending on whether the run button is down
			if (!toggleRun)
            {
                speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
            }			

			// Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
			else 
            {
				moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputZ * inputModifyFactor);
				moveDirection = myTransform.TransformDirection(moveDirection) * speed;
				playerControl = true;
			}
			
			// Jump! But only if the jump button has been released and player has been grounded for a given number of frames
			if (!Input.GetButton("Jump"))
				jumpTimer++;
			else if (jumpTimer >= antiBunnyHopFactor) 
            {
				moveDirection.y = jumpSpeed;
				jumpTimer = 0;
			}
		}
		else 
        {
			// If we stepped over a cliff or something, set the height at which we started falling
			if (!falling) {
				falling = true;
				fallStartLevel = myTransform.position.y;
			}
			
			// If air control is allowed, check movement but don't touch the y component
			if (airControl && playerControl) 
            {
				moveDirection.x = inputX * speed * inputModifyFactor;
				moveDirection.z = inputZ * speed * inputModifyFactor;
				moveDirection = myTransform.TransformDirection(moveDirection);
			}
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller, and set grounded true or false depending on whether we're standing on something
		grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
	}
	
	void Update () 
    {
		// If the run button is set to toggle, then switch between walk/run speed. (We use Update for this...
		// FixedUpdate is a poor place to use GetButtonDown, since it doesn't necessarily run every frame and can miss the event)

		if (toggleRun && grounded && Input.GetButtonDown("Run"))
			speed = (speed == walkSpeed? runSpeed : walkSpeed);
	}

	
	// Store point that we're in contact with for use in FixedUpdate if needed
	void OnControllerColliderHit (ControllerColliderHit hit) 
    {
		contactPoint = hit.point;
	}
	
	// If falling damage occured, this is the place to do something about it. You can make the player
	// have hitpoints and remove some of them based on the distance fallen, add sound effects, etc.
	void FallingDamageAlert (float fallDistance) 
    {
		print ("Ouch! Fell " + fallDistance + " units!");
	}
}