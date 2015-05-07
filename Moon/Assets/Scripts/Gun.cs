using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{

	public GameObject projectile;
	public bool isHidden = true;

	void Start()
    {
		// On application start, an array is made of all the renderers of each of the gun models components		

		GameObject gun = GameObject.FindGameObjectWithTag("Gun");
		Renderer[] renderers = gun.GetComponentsInChildren<Renderer>();

		foreach (Renderer r in renderers) 
        {
            // Each component in the model is set to not render when the player first spawns in

			r.enabled = !isHidden;
		}
	}
	 
	void Fire () 
    {
		// Creates a new projectile pointing in the same direction as the gun
		// Projectile is given motion in the Projectile script

		GameObject cloneProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
	}

	void EquipUnequipGun()
    {
		// Runs on button press -- checks if the model isHidden or not	

		if (isHidden) 
        {
            // If the model is hidden, bool isHidden is changed to false
            // and the gun model components are rendered on the screen

			GameObject gun = GameObject.FindGameObjectWithTag ("Gun");
			Renderer[] renderers = gun.GetComponentsInChildren<Renderer> ();
			isHidden = false;
			
			foreach (Renderer r in renderers) 
            {
				r.enabled = !isHidden;
			}
		} 
        else 
        {
            // If the model is not hidden, bool isHidden is changed to true
            // and the gun model components are no longer rendered on the screen

			GameObject gun = GameObject.FindGameObjectWithTag ("Gun");
			Renderer[] renderers = gun.GetComponentsInChildren<Renderer> ();
			isHidden = true;
			
			foreach (Renderer r in renderers) 
            {
				r.enabled = !isHidden;
			}
		}
	}
	
	void Update () 
    {
		// Checks for Fire or GunEquip inputs from the player		

		if (Input.GetButtonDown("GunEquip")) 
        {			
			EquipUnequipGun();
		}

		if ((!isHidden) && (Input.GetButtonDown("Fire"))) 
        {
            // If the gun model is currently not hidden, and the Fire input is made, the gun fires

			Fire();
		} 
	}
}