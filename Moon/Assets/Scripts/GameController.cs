using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	bool isHidden = true;
	float startTime;

	// Use this for initialization
	void Start () {
		// Renderer for the respawn text is enabled on scene start
		// Coroutine is started which waits 5 seconds before executing the subsequent code

		//GameObject monolith = GameObject.FindGameObjectWithTag("Monolith");
				
		//Woah woah = monolith.GetComponent<Woah> ();

		//respawnRenderer.enabled = !isHidden;
		//monolithRenderer.enabled = !isHidden;
		StartCoroutine (CheckTextDisplay (1f));		
	}

	IEnumerator WaitAndStopRespawnRender(float waitTime){
		// Coroutine waits for waitTime seconds and then disables the renderer for the respawn text

		yield return new WaitForSeconds (waitTime);
			
		//GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject text = GameObject.FindGameObjectWithTag ("RespawnText");
		Renderer renderer = text.GetComponent<Renderer> ();
			
		renderer.enabled = !isHidden;
	}

	IEnumerator WaitAndStopMonolithRender(float waitTime){
		// Coroutine waits for waitTime seconds and then disables the renderer for the monolith text
		
		yield return new WaitForSeconds (waitTime);
		
		//GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject text = GameObject.FindGameObjectWithTag ("MonolithText");
		Renderer renderer = text.GetComponent<Renderer> ();
		
		renderer.enabled = !isHidden;
	}

	IEnumerator CheckTextDisplay(float waitTime){
		yield return new WaitForSeconds (waitTime);

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject respawnText = GameObject.FindGameObjectWithTag("RespawnText");
		GameObject monolithText = GameObject.FindGameObjectWithTag("MonolithText");
		Renderer monolithRenderer = monolithText.GetComponent<Renderer> ();
		Renderer respawnRenderer = respawnText.GetComponent<Renderer> ();

		if (Woah.GetFoundMonolith ()) {
			monolithRenderer.enabled = isHidden;
			StartCoroutine(WaitAndStopMonolithRender (5f));
		} else {
			respawnRenderer.enabled = isHidden;
			StartCoroutine (WaitAndStopRespawnRender (5f));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
