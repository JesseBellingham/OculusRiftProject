using UnityEngine;
using System.Collections;

public class AsteroidRandomSpawn : MonoBehaviour {

	public GameObject asteroid;
	public float secondsUntilDestroy = 30;
	public float timeToSpawn;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		InvokeRepeating ("CreateAsteroid", 15, timeToSpawn);
	}

	void CreateAsteroid(){
		Vector3 spawnLocation = new Vector3(Random.Range (-1024f, 1024f), Random.Range (50f, 200f), Random.Range (-1024f, 1024f));
		GameObject cloneAsteroid = Instantiate(asteroid, spawnLocation, Random.rotation) as GameObject;

		if (cloneAsteroid.transform.position.y < 0){
			Destroy(cloneAsteroid);
		}
		
		Destroy (cloneAsteroid, secondsUntilDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

		foreach (GameObject a in asteroids){
			if (a.transform.position.y < 0) {
				Destroy(a);
			}
			Destroy (a, secondsUntilDestroy);
		}
	}
}
