using UnityEngine;
using System.Collections;

public class AsteroidRandomRotation : MonoBehaviour {

	public float tumble;

	// Start runs once when the script is initiated
	void Start () {
		// Applies random rotation to the created asteroid

		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
	}
}