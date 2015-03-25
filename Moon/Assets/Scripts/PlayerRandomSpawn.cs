using UnityEngine;
using System.Collections;

public class PlayerRandomSpawn : MonoBehaviour {

	public GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
	public GameObject player = GameObject.FindGameObjectWithTag ("Player");

	// Use this for initialization
	void Start () {
		//SpawnPlayer ();
	
	}
	void SpawnPlayer(){
		//Vector3 spawnPoint = spawnPoints [Mathf.RoundToInt(Random.Range (0, spawnPoints.Length))];
		//Instantiate(player, spawnPoint, Quaternion.identity);
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
