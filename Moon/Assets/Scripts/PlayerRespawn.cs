using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	void Update() 
	{		
		if (Input.GetButtonDown("PlayerRespawn")) Application.LoadLevel("Moon");		
	}
}
