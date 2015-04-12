using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject projectile;
	public float speed;
	public float secondsUntilDestroy = 30;

	private float startTime; 
	
	void Start () {
		// Applies forward force based on the projectile's mass, still having the projectile be affected by gravity

		startTime = Time.time;

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Rigidbody proj = projectile.GetComponent<Rigidbody>();
		proj.AddForce (player.transform.forward * speed, ForceMode.Impulse);
	} 
	
	// Update is called once per frame
	void FixedUpdate () {		
		// If the projectile has existed as long as SecondsUntilDestroy, destroy it

		if (Time.time - startTime >= secondsUntilDestroy) {
			Destroy(this.gameObject);
		} 
	}
}