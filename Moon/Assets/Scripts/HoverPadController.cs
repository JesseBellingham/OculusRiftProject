using UnityEngine;
using System.Collections;

public class HoverPadController : MonoBehaviour {

    public static bool playerFlying = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {  
        TextRender();                
	}

    void OnTriggerEnter(Collider collider)
    {
        // Runs when the player enters the small sphere collider on the deck of the hoverpad
        // Renders the instructions for entering the hoverpad
        // When the player exits the collider, the instructions are not rendered

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject text = GameObject.FindGameObjectWithTag("HoverpadEnterText");
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
        GameObject hoverpad = GameObject.FindGameObjectWithTag("Hoverpad");
        GameObject exitText = GameObject.FindGameObjectWithTag("HoverpadExitText");
        Renderer textRenderer = exitText.GetComponent<Renderer>();
        
        // Checks that the external collider is that of the Player
        if (collider.gameObject.tag == "Player")
        {
            // Checks that the player is not already flying, and has pressed the hoverpad enter button
            if ((Input.GetButtonDown("HoverpadEnter")) && (!playerFlying))
            {         
                player.GetComponent<EnhancedFPSCharacterController>().enabled = false;  // Disables the character controller component on the Player model
                this.GetComponent<HoverpadMover>().enabled = true;                      // Enables movement oriented components on the hoverpad model
                this.GetComponent<CharacterController>().enabled = true;
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                player.transform.parent = GameObject.FindGameObjectWithTag("Hoverpad").transform;   // Temporarily sets the player as a child of the hoverpad so that the player model travels with the hoverpad
                playerFlying = true;
                textRenderer.enabled = true;    // Renders the hoverpad exit instructions

                StartCoroutine(HoverpadExitStopRender(5f));
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Disables the hoverpad enter instructions when the player exits the small sphere collider on the deck of the hoverpad
        GameObject text = GameObject.FindGameObjectWithTag("HoverpadEnterText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        textRenderer.enabled = false;
    }

    void TextRender()
    {
        if (playerFlying)
        {
            // If the player is currently flying the hoverpad, the enter text is disabled
            GameObject text = GameObject.FindGameObjectWithTag("HoverpadEnterText");
            Renderer textRenderer = text.GetComponent<Renderer>();

            textRenderer.enabled = false;
        }
    }

    IEnumerator HoverpadExitStopRender(float waitTime)
    {
        // Coroutine waits for waitTime seconds and then disables the renderer for the hoverpad enter text

        yield return new WaitForSeconds(waitTime);

        GameObject text = GameObject.FindGameObjectWithTag("HoverpadExitText");
        Renderer renderer = text.GetComponent<Renderer>();

        renderer.enabled = false;
    }
}
