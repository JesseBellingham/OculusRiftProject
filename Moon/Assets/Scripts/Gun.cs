using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject projectile;
	public bool isHidden = true;

	void Start(){				
		GameObject gun = GameObject.FindGameObjectWithTag("Gun");
		Renderer[] renderers = gun.GetComponentsInChildren<Renderer>();

		foreach (Renderer r in renderers) {
			r.enabled = !isHidden;
		}
	}
	
	// Fire a bullet 
	void Fire () {
		// Create a new bullet pointing in the same direction as the gun 
		GameObject cloneProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
	}

	void EquipUnequipGun(){
		if (isHidden) {
			GameObject gun = GameObject.FindGameObjectWithTag ("Gun");
			Renderer[] renderers = gun.GetComponentsInChildren<Renderer> ();
			isHidden = false;
			
			foreach (Renderer r in renderers) {
				r.enabled = !isHidden;
			}
		} else {
			GameObject gun = GameObject.FindGameObjectWithTag ("Gun");
			Renderer[] renderers = gun.GetComponentsInChildren<Renderer> ();
			isHidden = true;
			
			foreach (Renderer r in renderers) {
				r.enabled = !isHidden;
			}
		}
	}
	
	void Update () {
		// Fire if the left mouse button is clicked
		if (Input.GetButtonDown("GunEquip")) {			
			EquipUnequipGun();
		}

		if ((!isHidden) && (Input.GetButtonDown("Fire1"))) {
			Fire();
		} 
	}
}