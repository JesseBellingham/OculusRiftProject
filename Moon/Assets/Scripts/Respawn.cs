using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	bool isHidden = true;
	Vector3 playerStartPosition;
	float secondsToDisappear = 5;

	private float startTime;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerStartPosition = player.transform.localPosition;

		Renderer renderer = this.GetComponent<Renderer> ();
		renderer.enabled = !isHidden;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Renderer renderer = this.GetComponent<Renderer> ();


		if ((player.transform.localPosition.y <= -300) && (Application.loadedLevelName != "ayylmao")){
			//player.transform.position = playerStartPosition;
			/*startTime = Time.time;

			if (Time.time - startTime >= 5){
				renderer.enabled = isHidden;
			}
			renderer.enabled = !isHidden;
			*/

			Application.LoadLevel("ayylmao");
		}
	}
}
