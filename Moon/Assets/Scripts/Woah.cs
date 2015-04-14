using UnityEngine;
using System.Collections;

public class Woah : MonoBehaviour {

	static bool foundMonolith = false;
	//bool isHidden = true;

	void Awake(){
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if ((player.transform.localPosition.y > 600) && (Application.loadedLevelName != "ayylmao")){
			Application.LoadLevel ("ayylmao");
		} else if ((player.transform.localPosition.y <= -300) && (Application.loadedLevelName != "ayylmao")) {
			
			Application.LoadLevel ("ayylmao");
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			
			player.GetComponent<Rigidbody> ().isKinematic = false;
			((EnhancedFPSCharacterController)player.GetComponent<EnhancedFPSCharacterController>()).enabled = false;
			this.GetComponent<Rigidbody> ().useGravity = true;
			Physics.gravity = new Vector3 (0, 9.81f, 0);
			foundMonolith = true;
		}
	}

	public static bool GetFoundMonolith(){
		return foundMonolith;
	}
}
