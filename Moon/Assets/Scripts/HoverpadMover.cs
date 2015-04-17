using UnityEngine; using System.Collections;

// Require a character controller to be attached to the same game object 
[RequireComponent(typeof(CharacterController))]
//[AddComponentMenu("Third Person Player/Third Person Controller")]
public class HoverpadMover : MonoBehaviour
{
    public float moveSpeed;
    public float rotationDamping = 20f;

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
        GameObject hoverpad = GameObject.FindGameObjectWithTag("Hoverpad");
        float z = Input.GetAxis("Vertical");

        Vector3 inputVec = new Vector3(0, verticalVelocity, z);

        moveDirection = new Vector3(-inputVec.x, inputVec.y, -inputVec.z);
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed;
        controller.Move(moveDirection);
        
        if (Input.GetButton("HoverpadRotateLeft"))
        {
            hoverpad.transform.Rotate(0, -((moveSpeed * 30)* Time.deltaTime), 0);
        }
        else if (Input.GetButton("HoverpadRotateRight"))
        {
            hoverpad.transform.Rotate(0, ((moveSpeed * 30) * Time.deltaTime), 0);
        }
        return inputVec.magnitude;
    }

    void Update()
    {
        if (HoverPadController.playerFlying)
        {
            if (Input.GetButtonDown("HoverpadExit"))
            {
                /*
                 * GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<EnhancedFPSCharacterController>().enabled = true;
                this.GetComponent<HoverpadMover>().enabled = false;
                this.GetComponent<CharacterController>().enabled = false;
                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponent<Rigidbody>().isKinematic = false;
                player.transform.position = this.transform.localPosition;
                player.transform.parent = null;
                HoverPadController.playerFlying = false;
                 * 
                 */

                StartCoroutine(HoverpadLand(0.016f));                
            }            
        }

        if (HoverPadController.playerFlying == true)
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

    IEnumerator HoverpadLand(float waitTime)
    {
        HoverPadController.playerFlying = false;

        while ((controller.collisionFlags & CollisionFlags.Below) == 0)
        {
            verticalVelocity = -moveSpeed;
            moveDirection = new Vector3(0, verticalVelocity, 0);
            UpdateMovement();
            yield return new WaitForSeconds(waitTime);
        }        
        SetPlayerWalking();
    }

    private void SetPlayerWalking()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        player.GetComponent<EnhancedFPSCharacterController>().enabled = true;
        this.GetComponent<HoverpadMover>().enabled = false;
        this.GetComponent<CharacterController>().enabled = false;
        this.transform.position = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 3, this.transform.localPosition.z);
        player.transform.parent = null;
    }
}