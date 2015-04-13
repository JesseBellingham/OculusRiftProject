using UnityEngine;
using System.Collections;

public class MonolithAudio : MonoBehaviour {

	public Transform target;
	
	void Start () {
		
		StartCoroutine(AdjustVolume());
		
	}
	
	IEnumerator AdjustVolume () {
		
		while(true) {
			
			if(GetComponent<AudioSource>().isPlaying) { // do this only if some audio is being played in this gameObject's AudioSource
				
				float distanceToTarget = Vector3.Distance(transform.position, target.position); // Assuming that the target is the player or the audio listener
				
				if(distanceToTarget < 1) { distanceToTarget = 1; }
				
				GetComponent<AudioSource>().volume = 10/distanceToTarget; // this works as a linear function, while the 3D sound works like a logarithmic function, so the effect will be a little different (correct me if I'm wrong)
				
				yield return new WaitForSeconds(1); // this will adjust the volume based on distance every 1 second (Obviously, You can reduce this to a lower value if you want more updates per second)
				
			}
		}
		
	}
}