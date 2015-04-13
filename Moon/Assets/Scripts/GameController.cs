using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	bool isHidden = true;
	float startTime;

	// Use this for initialization
	void Start () {
		// Renderer for the respawn text is enabled on scene start
		// Coroutine is started which waits 5 seconds before executing the subsequent code

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject text = GameObject.FindGameObjectWithTag("RespawnText");
		Renderer renderer = text.GetComponent<Renderer> ();		

		renderer.enabled = isHidden;

		StartCoroutine (WaitAndStopRender (5f));
	}

	IEnumerator WaitAndStopRender(float waitTime){
		// Coroutine waits for waitTime seconds and then disables the renderer for the respawn text

		yield return new WaitForSeconds (waitTime);

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject text = GameObject.FindGameObjectWithTag("RespawnText");
		Renderer renderer = text.GetComponent<Renderer> ();

		renderer.enabled = !isHidden;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
