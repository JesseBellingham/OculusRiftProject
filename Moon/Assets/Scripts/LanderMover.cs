using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class LanderMover : MonoBehaviour
{

    public float maxSpeed = 20.0f;
    public float turnSpeed = 20.0f;
    public float downforce;
    public float gravity = 20.0f;

    public WheelCollider frontRWheel;
    public WheelCollider frontLWheel;
    public WheelCollider rearRWheel;
    public WheelCollider rearLWheel;

	// Carried over from EnhancedFPSCharacterController...pretty sure it's not doing anything
    public float antiBumpFactor = .75f;

    // Carried over from EnhancedFPSCharacterController...pretty sure it's not doing anything
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
    private WheelCollider[] wheelColliders = new WheelCollider[4];
    private GameObject[] wheelMeshes = new GameObject[4];
    private Vector3 centreOfMassOffset;
    private Rigidbody rigidbody;

    private Quaternion[] wheelMeshLocalRotations;

    public float AccelInput { get; private set; }

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        myTransform = transform;
        //speed = moveSpeed;
        rayDistance = controller.height * .5f + controller.radius;

        wheelMeshLocalRotations = new Quaternion[4];

        for (int i = 0; i < 4; i++)
        {
            wheelMeshLocalRotations[i] = wheelMeshes[i].transform.localRotation;
        }
        wheelColliders[0].attachedRigidbody.centerOfMass = centreOfMassOffset;
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float inputY = Input.GetAxis("Vertical");
        Move(inputY);
    }

    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contactPoint = hit.point;
    }

    void Move(float accel)
    {
        /*for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 position;
            wheelColliders[i].GetWorldPose(out position, out quat);
            wheelMeshes[i].transform.position = position;
            wheelMeshes[i].transform.rotation = quat;
        }*/
        AccelInput = Mathf.Clamp(accel, 0, 1);
        Debug.Log(AccelInput);
        ApplyDrive(AccelInput);
        /*if (grounded)
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
        }*/

        LanderSteering();

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;      


        AddDownForce();
    }

    private void ApplyDrive(float accel)
    {    
        rearLWheel.motorTorque = maxSpeed * AccelInput;
        rearRWheel.motorTorque = maxSpeed * AccelInput;        
    }

    private void LanderSteering()
    {
        if (Input.GetButton("VehicleRotateLeft"))
        {
            // Input.GetButton gets held down keys -- this code block runs as long as the VehicleRotateLeft key is held down

            float turn = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, -1);
            frontLWheel.steerAngle = turnSpeed * turn;
            frontRWheel.steerAngle = turnSpeed * turn;
        }
        else if (Input.GetButton("VehicleRotateRight"))
        {
            float turn = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, -1);
            frontLWheel.steerAngle = turnSpeed * turn;
            frontRWheel.steerAngle = turnSpeed * turn;
        }
    }

    private void AddDownForce()
    {
        frontLWheel.attachedRigidbody.AddForce(-transform.up * downforce *
                                                     frontLWheel.attachedRigidbody.velocity.magnitude);

        frontRWheel.attachedRigidbody.AddForce(-transform.up * downforce *
                                                     frontRWheel.attachedRigidbody.velocity.magnitude);

        rearLWheel.attachedRigidbody.AddForce(-transform.up * downforce *
                                                     rearLWheel.attachedRigidbody.velocity.magnitude);

        rearRWheel.attachedRigidbody.AddForce(-transform.up * downforce *
                                                     rearRWheel.attachedRigidbody.velocity.magnitude);
    }
}