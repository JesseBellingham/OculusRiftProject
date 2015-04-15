using UnityEngine;
using System.Collections;

public class MonolithAudio : MonoBehaviour {

	public Transform target;
	
	void Start () {
		
		StartCoroutine(AdjustVolume());
		
	}
	
	IEnumerator AdjustVolume () {
		
		while(true) {

			// do this only if some audio is being played in this gameObject's AudioSource
			if(GetComponent<AudioSource>().isPlaying) { 

				// Assuming that the target is the player or the audio listener
				float distanceToTarget = Vector3.Distance(transform.position, target.position);
				// this increases the audio clip's volume linearly, based on the player's proximity to the Monolith
				GetComponent<AudioSource>().volume = 10/distanceToTarget; 
				// this will adjust the volume based on distance every 0.1 second
				yield return new WaitForSeconds(0.1f); 
				
			}
		}
		
	}
}