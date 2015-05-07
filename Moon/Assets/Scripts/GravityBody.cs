using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour 
{

    public PlanetGravity attractor;

    private int grounded;

	// Use this for initialization
	void Start () 
    {
        this.GetComponent<Rigidbody>().WakeUp();
        this.GetComponent<Rigidbody>().useGravity = false;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.layer == 7) && (grounded > 0))
        {
            grounded--;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (attractor)
        {
            attractor.Attract(this);
        }
	}
}
