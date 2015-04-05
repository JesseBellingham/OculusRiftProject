using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject projectile;

	void Start(){
		 
	}
	
	// Fire a bullet 
	void Fire () {
		// Create a new bullet pointing in the same direction as the gun 
		GameObject cloneProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject; 
	} 
	
	void FixedUpdate () {
		// Fire if the left mouse button is clicked 
		if (Input.GetButtonDown("Fire1")) {
			Fire();
		} 
	}
}