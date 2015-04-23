using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject projectile;
	public float speed;
	public float secondsUntilDestroy = 30;

	private float startTime; 
	
	void Start () {		

		startTime = Time.time;

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Rigidbody proj = projectile.GetComponent<Rigidbody>();
        proj.AddForce(player.transform.forward * speed, ForceMode.Impulse);    // Applies forward force based on the projectile's mass, still having the projectile be affected by gravity
	} 
	
	// Update is called once per frame
	void FixedUpdate () {		
		// If the projectile has existed as long as SecondsUntilDestroy, destroy it

		if (Time.time - startTime >= secondsUntilDestroy) {
			Destroy(this.gameObject);
		} 
	}

    void OnCollisionEnter(Collision collision)
    {
        Vector3 location = collision.transform.localPosition;
        ProjectileExplosion(location);
    }

    void ProjectileExplosion(Vector3 location)
    {
        for (int i = 0; i < 10; i++)
        {
            Quaternion rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(90, 270), 0);
            GameObject cloneProjectile = Instantiate(projectile, location, rotation) as GameObject;
        }        
    }
}