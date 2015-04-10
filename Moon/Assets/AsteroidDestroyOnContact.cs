using UnityEngine;
using System.Collections;

public class AsteroidDestroyOnContact : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Floor") {
			Instantiate (explosion, transform.position, transform.rotation);
			//Destroy (this);
		}
	}
}
