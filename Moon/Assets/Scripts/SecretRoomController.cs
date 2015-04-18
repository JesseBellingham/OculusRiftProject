using UnityEngine;
using System.Collections;

public class SecretRoomController : MonoBehaviour {

	bool isHidden = true;
	float startTime;

	void Start () {
		// CheckTextDisplay selects which renderer for the respawn text is enabled on scene start
		// Coroutine is started which waits 1 second before executing the subsequent code
		
		StartCoroutine (CheckTextDisplay (1f));		
	}

	IEnumerator WaitAndStopRespawnRender(float waitTime){
		// Coroutine waits for waitTime seconds and then disables the renderer for the respawn text

		yield return new WaitForSeconds (waitTime);
		
		GameObject text = GameObject.FindGameObjectWithTag ("RespawnText");
		Renderer renderer = text.GetComponent<Renderer> ();
			
		renderer.enabled = !isHidden;
	}

	IEnumerator WaitAndStopMonolithRender(float waitTime){
		// Coroutine waits for waitTime seconds and then disables the renderer for the monolith text
		
		yield return new WaitForSeconds (waitTime);
		
		GameObject text = GameObject.FindGameObjectWithTag ("MonolithText");
		Renderer renderer = text.GetComponent<Renderer> ();
		
		renderer.enabled = !isHidden;
	}

	IEnumerator CheckTextDisplay(float waitTime){
        // Coroutine waits for waitTime seconds

		yield return new WaitForSeconds (waitTime);

		GameObject respawnText = GameObject.FindGameObjectWithTag("RespawnText");
		GameObject monolithText = GameObject.FindGameObjectWithTag("MonolithText");
		Renderer monolithRenderer = monolithText.GetComponent<Renderer> ();
		Renderer respawnRenderer = respawnText.GetComponent<Renderer> ();

		if (Woah.GetFoundMonolith ()) {
            // If the player has found the Monolith, render MonolithText and then start the coroutine to stop the renderer
			monolithRenderer.enabled = isHidden;
			StartCoroutine(WaitAndStopMonolithRender (5f));
		} else {
            // If the player is respawned any other way, render RespawnText and then start the coroutine to stop the renderer
			respawnRenderer.enabled = isHidden;
			StartCoroutine (WaitAndStopRespawnRender (5f));
		}
	}	
}
