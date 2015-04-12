using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		// Applies forward motion to the Rigidbody component of each asteroid created

		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}
}