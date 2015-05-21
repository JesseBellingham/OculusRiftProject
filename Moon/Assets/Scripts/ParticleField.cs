using UnityEngine;
using System.Collections;

public class ParticleField : MonoBehaviour 
{

    public EnhancedFPSCharacterController player;	

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            // Projectiles inside the particle field have constant upward force applied to them
            collider.GetComponent<Rigidbody>().AddForce(0, 3, 0);
        }
        else if (collider.gameObject.tag == "Player")
        {
            // While the player is inside the particle field they are sent upwards, like a gravity elevator
            player.airControl = true;
            player.gravity = -5f;
            player.walkSpeed = 2;
            player.runSpeed = 2;
        }       
    }

    void OnTriggerExit(Collider collider)
    {
		// Sets player variables back to normal
        player.airControl = false;
        player.gravity = 1.622f;
        player.walkSpeed = 6;
        player.runSpeed = 18;
    }
}
