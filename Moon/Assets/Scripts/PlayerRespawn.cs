using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	void Update() 
	{	
		// Allows player to reload the main level if they get stuck
		if (Input.GetButtonDown("PlayerRespawn")) Application.LoadLevel("Moon");		
	}
}
