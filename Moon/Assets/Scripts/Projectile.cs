using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject projectile;
	public float speed;
	public float secondsUntilDestroy = 30;

	private float startTime; 
	
	void Start () {
		startTime = Time.time; 
	} 
	
	// Update is called once per frame
	void FixedUpdate () {

		Physics.IgnoreCollision (projectile.GetComponent<Collider>(), GetComponent<Collider>());
		// Move forward 
		this.gameObject.transform.position += speed * this.gameObject.transform.forward;
		
		// If the projectile has existed as long as SecondsUntilDestroy, destroy it 
		if (Time.time - startTime >= secondsUntilDestroy) {
			Destroy(this.gameObject);
		} 
	}
	
	void OnCollisionEnter(Collision collision) {
		
		// Remove the projectile from the world 
		Destroy(this.gameObject); 
	}
}