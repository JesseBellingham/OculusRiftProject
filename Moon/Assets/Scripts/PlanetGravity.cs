using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	public float pullRadius = 1000;
	public float pullForce = 50;
	
	void FixedUpdate() {
		foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
			// calculate direction from target to me
			Vector3 forceDirection = transform.position - collider.transform.position;
			
			// apply force on target towards me
			if (collider.GetComponent<Rigidbody>() != null){
				collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);                
			}
		}
	}
}
