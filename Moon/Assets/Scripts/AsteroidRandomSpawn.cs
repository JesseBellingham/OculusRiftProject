using UnityEngine;
using System.Collections;

public class AsteroidRandomSpawn : MonoBehaviour {

	public GameObject asteroid;
	public float secondsUntilDestroy = 120;
	public float timeToSpawn;
	public float cloneSpeed;

	// Use this for initialization
	void Start () {
		// InvokeRepeating runs when this script starts -- it waits 15 seconds before calling CreateAsteroid()
		// timeToSpawn dictates the delay in seconds between subsequent calls after the first.
		InvokeRepeating ("CreateAsteroid", 15, timeToSpawn);
	}


	void CreateAsteroid(){
		// Creates a new asteroid with a randomly generated rotation (X rotation is limited to only spawn asteroids towards the floor),
		// and randomly generated location coordinates somewhere above the floor.	

		Quaternion rotation = new Quaternion (Random.Range (0, 180), Random.Range (-360, 360), -360, 360);
		Vector3 spawnLocation = new Vector3(Random.Range (200f, 1848f), Random.Range (750, 1250f), Random.Range (0f, 1024f));
		GameObject cloneAsteroid = Instantiate(asteroid, spawnLocation, rotation) as GameObject;
        cloneAsteroid.transform.parent = GameObject.FindGameObjectWithTag("AsteroidSpawner").transform; // Asteroid is made a child of the AsteroidSpawner object to keep the hierarchy view tidy.
        cloneAsteroid.transform.localScale = Vector3.one * Random.Range(5, 20);                         // Asteroid is given a random size between 5 and 20, upon creation.
	}
	
	// Update is called once per frame
	void Update () {		
        // Creates an array of all the asteroid clones in the scene and checks if any are out of the specified coordinate bounds.

		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

		foreach (GameObject a in asteroids){
            // If an asteroid goes out of bounds, it is destroyed as long as it is not the last one
            // (There must always be at least one asteroid in the scene in order for new clones to be created)
            // -- otherwise it is destroyed after secondsUntilDestroy.

			if ((a.transform.localPosition.y < -1000) || (a.transform.localPosition.y > 5000)) {                
				if (asteroids.Length > 1){
					Destroy(a);
				}
				
			} else if ((a.transform.localPosition.x < -2000) || (a.transform.localPosition.x > 2000)){
				if (asteroids.Length > 1){
					Destroy(a);
				}
			}
			Destroy (a, secondsUntilDestroy);
		}
	}
}
