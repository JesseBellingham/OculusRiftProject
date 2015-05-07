using UnityEngine;
using System.Collections;

// Require a character controller to be attached to the same game object 
[RequireComponent(typeof(CharacterController))]
//[AddComponentMenu("Third Person Player/Third Person Controller")]
public class HoverpadMover : MonoBehaviour
{
    public float moveSpeed;
	public float gamepadRotateSpeed;
	public Vector2 sensitivity = new Vector2(2, 2);
	public Vector2 smoothing = new Vector2(3, 3);

	Vector2 smoothMouse;

    float verticalVelocity;
    Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    void Start()
    {
        controller = (CharacterController)GetComponent(typeof(CharacterController));
    }

    void UpdateMovement()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject hoverpad = GameObject.FindGameObjectWithTag("Hoverpad");

		// Get raw mouse input for a cleaner reading on more sensitive mice.
		Vector2 mouseDelta = new Vector2(0, Input.GetAxisRaw("Mouse X"));
		
		// Scale input against the sensitivity setting and multiply that against the smoothing value.
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);

		mouseDelta += smoothMouse;

        float z = Input.GetAxis("Vertical");    // Due to the hoverpad model's axes orientation, the z axis is assigned to forward and backward motion

        Vector3 inputVec = new Vector3(0, verticalVelocity, z); // inputVec gathers all current movement inputs into a single Vector3

        moveDirection = new Vector3(-inputVec.x, inputVec.y, -inputVec.z);  // Inverts axes to correspond to the player's orientation
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed; // Increases the speed of motion by a factor of moveSpeed
        controller.Move(moveDirection);
        
        // This is for controller input
        /*if (Input.GetAxis ("Horizontal") == -1) {
			// Input.GetButton gets held down keys -- this code block runs as long as the HoverpadRotateLeft key is held down

			hoverpad.transform.Rotate (0, -((gamepadRotateSpeed * 30) * Time.deltaTime), 0);   // Rotates the hoverpad to the left -- the number affects the speed of the rotation
		} else if (Input.GetAxis ("Horizontal") == 1) {
			hoverpad.transform.Rotate (0, ((gamepadRotateSpeed * 30) * Time.deltaTime), 0);
		}*/

        if (Input.GetAxis("Horizontal") < 0)
        {
            // Input.GetButton gets held down keys -- this code block runs as long as the HoverpadRotateLeft key is held down

            hoverpad.transform.Rotate(0, -((gamepadRotateSpeed * 30) * Time.deltaTime), 0);   // Rotates the hoverpad to the left -- the number affects the speed of the rotation
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            hoverpad.transform.Rotate(0, ((gamepadRotateSpeed * 30) * Time.deltaTime), 0);
        }
        
        /*else if (){
			//player.GetComponent<SimpleSmoothMouseLook>().enabled = false;
			//hoverpad.transform.Rotate (mouseDelta);
			Vector3 rotation = new Vector3(0, player.transform.localRotation.y, 0);
			hoverpad.transform.Rotate(rotation * gamepadRotateSpeed);
		}*/
    }

    void Update()
    {


        if (HoverPadController.playerFlying)
        {
            if (Input.GetButtonDown("VehicleExit"))
            {
                StartCoroutine(HoverpadLand(0.016f)); 
            }

            if (Input.GetButton("HoverpadUp"))
            {
                verticalVelocity = moveSpeed;
                UpdateMovement();   // Runs UpdateMovement with upwards vertical movement included
            }
            else if (Input.GetButton("HoverpadDown"))
            {
                verticalVelocity = -moveSpeed;
                UpdateMovement();   // Runs UpdateMovement with downwards vertical movement included            
            }

            verticalVelocity = 0;
            UpdateMovement();   // Runs UpdateMovement with no vertical movement included            
        }               
    }

    IEnumerator HoverpadLand(float waitTime)
    {
        HoverPadController.playerFlying = false;

        while ((controller.collisionFlags & CollisionFlags.Below) == 0) // While there have not been any collisions on the underside of the hoverpad
        {
            verticalVelocity = -moveSpeed;
            UpdateMovement();   // Runs UpdateMovement with downwards vertical movement included
            yield return new WaitForSeconds(waitTime);  // Waits the aproximate time that each frame takes to display 
            // (without this line, the coroutine is simply run as fast as the computer can process it, so the hoverpad goes from air to ground within the span of a single frame)
        }        
        SetPlayerWalking();
    }

    private void SetPlayerWalking()
    {
        // Sets all the Player model components that allow movement
        // Disables hoverpad components that allow movement
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //GameObject.FindGameObjectWithTag("Attention").SetActive(true);
        player.GetComponent<EnhancedFPSCharacterController>().enabled = true;
        this.GetComponent<HoverpadMover>().enabled = false;
        this.GetComponent<CharacterController>().enabled = false;
        this.transform.position = new Vector3(this.transform.localPosition.x, 
        this.transform.localPosition.y - 3, this.transform.localPosition.z); // One of the components on the hoverpad prevents it from properly reaching the ground when it is landed
        // My workaround was to simply alter the hoverpad's Y position when the player steps off
        player.transform.parent = null; // Sets the Player model to no longer be a child of the hoverpad
		//player.GetComponent<SimpleSmoothMouseLook> ().enabled = true;
    }
}