using UnityEngine;
using System.Collections;

public class HoverPadController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    void OnTriggerEnter(Collider collider)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject text = GameObject.FindGameObjectWithTag("HoverpadText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        if (collider.gameObject.tag == "Player")
        {
            textRenderer.enabled = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject hoverpad = GameObject.FindGameObjectWithTag("Hoverpad");
        GameObject text = GameObject.FindGameObjectWithTag("HoverpadText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        if (collider.gameObject.tag == "Player")
        {         
            if (Input.GetButtonDown("HoverpadEnter"))
            {
                player.GetComponent<EnhancedFPSCharacterController>().enabled = false;
                this.GetComponent<HoverpadMover>().enabled = true;
                this.GetComponent<CharacterController>().enabled = true;
                player.transform.parent = GameObject.FindGameObjectWithTag("Hoverpad").transform;

                textRenderer.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GameObject text = GameObject.FindGameObjectWithTag("HoverpadText");
        Renderer textRenderer = text.GetComponent<Renderer>();

        textRenderer.enabled = false;
    }
}
