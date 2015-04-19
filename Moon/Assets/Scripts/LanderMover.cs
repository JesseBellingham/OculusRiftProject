using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(CharacterController))]
public class LanderMover : MonoBehaviour
{

    public float moveSpeed = 20.0f;

    public float gravity = 20.0f;


    // If checked, then the player can change direction while in the air
    //public bool airControl = false;

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
        speed = moveSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        jumpTimer = antiBunnyHopFactor;
    }

    void FixedUpdate()
    {
        //float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (grounded)
        {      
            // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
            
            moveDirection = new Vector3(0, -antiBumpFactor, inputY);
            moveDirection = myTransform.TransformDirection(moveDirection) * speed;
            playerControl = true;                                
        }
        else
        {
            // If we stepped over a cliff or something, set the height at which we started falling
            if (!falling)
            {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }
            
            if (playerControl)
            {
                //moveDirection.x = inputX * speed;
                moveDirection.z = inputY * speed;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }

        if (Input.GetButton("VehicleRotateLeft"))
        {
            // Input.GetButton gets held down keys -- this code block runs as long as the HoverpadRotateLeft key is held down

            this.transform.Rotate(0, -((moveSpeed * 5) * Time.deltaTime), 0);   // Rotates the hoverpad to the left -- the number affects the speed of the rotation
        }
        else if (Input.GetButton("VehicleRotateRight"))
        {
            this.transform.Rotate(0, ((moveSpeed * 5) * Time.deltaTime), 0);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller, and set grounded true or false depending on whether we're standing on something
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }

    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }
}