using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour 
{
	public float pullRadius = 1000;
	public float pullForce = 50;
	
	/*void FixedUpdate() {
		foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
			// calculate direction from target to planet
			Vector3 forceDirection = transform.position - collider.transform.position;
			
			// apply force on target towards planet
			if (collider.GetComponent<Rigidbody>() != null){
				collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);                
			}
		}
	}*/

    public void Attract(GravityBody body)
    {
        Vector3 gravityUp;
        Vector3 localUp;
        Vector3 localForward;

        Transform bodyTransform = body.transform;
        Rigidbody bodyRigidbody = body.GetComponent<Rigidbody>();

        gravityUp = bodyTransform.position - transform.position;
        gravityUp.Normalize();

        bodyRigidbody.AddForce(gravityUp * pullForce * bodyRigidbody.mass);

        if (bodyRigidbody.freezeRotation)
        {
            localUp = bodyTransform.up;
            Quaternion rotation = Quaternion.FromToRotation(localUp, gravityUp);
            rotation = rotation * bodyTransform.rotation;

            bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, rotation, 0.1f);
            localForward = bodyTransform.forward;
        }
    }
}
