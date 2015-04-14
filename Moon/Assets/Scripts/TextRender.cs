using UnityEngine;
using System.Collections;

public class TextRender : MonoBehaviour {

	bool isHidden = true;

	// Use this for initialization
	void Start () {
		Renderer renderer = this.GetComponent<Renderer> ();		
		
		renderer.enabled = !isHidden;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
