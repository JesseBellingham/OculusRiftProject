using UnityEngine;
using System.Collections;

public class AsteroidRandomRotation : MonoBehaviour 
{

	public float tumble;

	void Start () 
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;  // Applies random rotation to the created asteroid
	}
}