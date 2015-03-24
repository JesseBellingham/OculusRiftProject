using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	GameObject planet;
	GameObject player;
	float gravitationalAcceleration = 5.0f;
	// Use this for initialization
	void Start () {
		planet = GameObject.FindGameObjectWithTag("Planet");
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rigidbody rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
		rigidbody.velocity += gravitationalAcceleration * Time.fixedTime * (planet.transform.position - transform.position);
	}
}
