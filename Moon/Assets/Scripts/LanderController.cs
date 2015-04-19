using UnityEngine;
using System.Collections;

public class LanderController : MonoBehaviour {

    public static bool playerDriving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        // Runs when the player enters the small sphere collider on the deck of the lander
        // Renders the instructions for entering the hoverpad
        // When the player exits the collider, the instructions are not rendered

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject text = GameObject.FindGameObjectWithTag("LanderEnterText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        if (collider.gameObject.tag == "Player")
        {
            textRenderer.enabled = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        // Runs when the player enters the collider and continues to run as long as the player stays within the collider

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject lander = GameObject.FindGameObjectWithTag("Lander");
        GameObject exitText = GameObject.FindGameObjectWithTag("LanderExitText");
        Renderer textRenderer = exitText.GetComponent<Renderer>();

        // Checks that the external collider is that of the Player
        if (collider.gameObject.tag == "Player")
        {
            // Checks that the player is not already driving, and has pressed the vehicle enter button
            if ((Input.GetButtonDown("VehicleEnter")) && (!playerDriving))
            {
                Vector3 landerDrivePos = lander.transform.localPosition;
                player.GetComponent<EnhancedFPSCharacterController>().enabled = false;  // Disables the character controller component on the Player model
                this.GetComponent<LanderMover>().enabled = true;                      // Enables movement oriented components on the lander model
                //this.GetComponent<CharacterController>().enabled = true;
                this.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<SimpleSmoothMouseLook>().enabled = false;
                player.transform.parent = GameObject.FindGameObjectWithTag("Lander").transform;   // Temporarily sets the player as a child of the lander so that the player model travels with the hoverpad
                player.transform.position = landerDrivePos + new Vector3(-5, 7, 0);
                player.transform.rotation = new Quaternion(0, 0, 0, 0);
                playerDriving = true;
                textRenderer.enabled = true;    // Renders the lander exit instructions

                StartCoroutine(LanderExitStopRender(5f));
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Disables the lander enter instructions when the player exits the sphere collider around the lander
        GameObject text = GameObject.FindGameObjectWithTag("LanderEnterText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        textRenderer.enabled = false;
    }

    void TextRender()
    {
        if (playerDriving)
        {
            // If the player is currently driving the lander, the enter text is disabled
            GameObject text = GameObject.FindGameObjectWithTag("LanderEnterText");
            Renderer textRenderer = text.GetComponent<Renderer>();

            textRenderer.enabled = false;
        }
    }

    IEnumerator LanderExitStopRender(float waitTime)
    {
        // Coroutine waits for waitTime seconds and then disables the renderer for the lander enter text

        yield return new WaitForSeconds(waitTime);

        GameObject text = GameObject.FindGameObjectWithTag("LanderExitText");
        Renderer renderer = text.GetComponent<Renderer>();

        renderer.enabled = false;
    }
}
