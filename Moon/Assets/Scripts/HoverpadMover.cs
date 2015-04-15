using UnityEngine; using System.Collections;

// Require a character controller to be attached to the same game object 
[RequireComponent(typeof(CharacterController))]
//[AddComponentMenu("Third Person Player/Third Person Controller")]
public class HoverpadMover : MonoBehaviour
{
    public float moveSpeed;
    public float rotationDamping = 20f;
    public Transform target;

    float verticalVelocity;
    Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    void Start()
    {
        controller = (CharacterController)GetComponent(typeof(CharacterController));
    }

    float UpdateMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputVec = new Vector3(x, verticalVelocity, z);
        //inputVec *= moveSpeed;

        moveDirection = new Vector3(-inputVec.x, inputVec.y, -inputVec.z);
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed;
        //controller.Move(moveDirection * Time.deltaTime);
        controller.Move(moveDirection);
        //controller.Move((inputVec + Vector3.up + new Vector3(0, verticalVelocity, 0)) * Time.deltaTime);
        //this.GetComponent<Rigidbody>().AddForce(x, verticalVelocity, z, ForceMode.Force);

        if (inputVec != Vector3.zero)
        {
            if ((Input.GetButton("Mouse X")) || Input.GetButton("a") || Input.GetButton("d"))
            {
                //transform.RotateAround(target.transform, Input.GetAxisRaw("Mouse X"), 
            }
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationDamping);
            //transform.rotation.x = player.transform.rotation.x;
        }

        return inputVec.magnitude;
    }

    void Update()
    {
        if (Input.GetButton("HoverpadUp"))
        {
            verticalVelocity = moveSpeed;
            moveDirection = new Vector3(0, verticalVelocity, 0);
            UpdateMovement();
        }
        else if (Input.GetButton("HoverpadDown"))
        {
            verticalVelocity = -moveSpeed;
            moveDirection = new Vector3(0, verticalVelocity, 0);
            UpdateMovement();
        }

        verticalVelocity = 0;
        UpdateMovement();
    }
}