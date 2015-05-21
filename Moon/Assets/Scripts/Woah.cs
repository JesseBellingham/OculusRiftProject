using UnityEngine;
using System.Collections;

public class Woah : MonoBehaviour 
{

	static bool foundMonolith = false;

    // Awake() runs when the scene loads
	void Awake()
    {
		DontDestroyOnLoad (this);   // When a new scene is loaded, do not destroy the Monolith object
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckPlayerPosition();
	}

	void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Player") 
        {
            InvertGravity();
		}
	}

	public static bool GetFoundMonolith()
    {
		return foundMonolith;
	}

    void InvertGravity()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject hoverpad = GameObject.FindGameObjectWithTag("Hoverpad");

        hoverpad.GetComponent<Rigidbody>().isKinematic = false; // Sets hoverpad to not require direct instruction for movement
        hoverpad.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;   // Sets player to not require direct instruction for movement
        ((EnhancedFPSCharacterController)player.GetComponent<EnhancedFPSCharacterController>()).enabled = false;
        this.GetComponent<Rigidbody>().useGravity = true;   // Sets monolith to be affected by gravity
		GameObject[] stonehenge = GameObject.FindGameObjectsWithTag ("Stonehenge");
		// Sets the boxes in the arrangement around the Monolith to be affected by gravity
		foreach (GameObject box in stonehenge) {
			box.GetComponent<Rigidbody>().isKinematic = false;
			box.GetComponent<Rigidbody>().useGravity = true;
		}
        Physics.gravity = new Vector3(0, 9.81f, 0);         // Inverts gravity
        foundMonolith = true;
    }

    void CheckPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

		// Loads the secret room if the player goes out of bounds on the main level
        if ((player.transform.localPosition.y > 2000) && (Application.loadedLevelName != "ayylmao"))
        {   
            Application.LoadLevel("ayylmao");
        }
        else if ((player.transform.localPosition.y <= -300) && (Application.loadedLevelName != "ayylmao"))
        {
            Application.LoadLevel("ayylmao");
        }
    }
}
