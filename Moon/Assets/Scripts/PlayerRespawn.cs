using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	void OnGUI () 
	{		
		if (Event.current.Equals (Event.KeyboardEvent ("r"))) Application.LoadLevel("Moon");		
	}
}
