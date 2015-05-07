using UnityEngine;
using System.Collections;

public class PlayerRandomSpawn : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {
		SpawnPlayer();	
	}
	void SpawnPlayer()
    {
        // An array of gameobjects is created containing all scene objects tagged as "Spawnpoint"
        
		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Quaternion playerRotation = new Quaternion (0, 90, 0, 0);

        Vector3 spawnPoint = spawnPoints[Mathf.RoundToInt(Random.Range(0, spawnPoints.Length))].transform.position;   // The player's spawn location on starting the game is decided randomly from the array's pool of possible locations
        player.transform.position = spawnPoint; // The player's position is set to the randomly chosen spawnpoint
	}	
}
