using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour {


	RaycastHit shootHit;
	Ray shootRay;
	LineRenderer laserLine;
	int shootableMask;
	
	bool isShooting = false;

	public int damagePoints = 10;

	public bool isEnabled = true;

	public AudioClip laser1;
	public AudioClip laser2;
	public AudioClip laser3;
	public AudioClip laser4;

	AudioSource audios;


	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Enemies");
		laserLine = GetComponentInChildren<LineRenderer> ();
		

		laserLine.enabled = false;
		audios = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {


		#if !MOBILE_INPUT
		if (Input.GetButtonDown ("Fire1") && isShooting == false && isEnabled == true) {
			Shoot ();
			
		} 
		#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			Shoot();
		}
		#endif
	
	}


	public void Shoot(){
		isShooting = true;
		
		laserLine.enabled = true;
		//laserLine.SetPosition (0, transform.position); // todo: fix this to come out off the eyes
		


		//shootRay.origin = transform.position;
		
		shootRay.direction = transform.forward;

		if (Physics.Raycast (shootRay, out shootHit, 100.0f, shootableMask)) {
			laserLine.SetPosition (1, shootHit.point);
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();
			if (enemyHealth != null) {
				enemyHealth.TakeDamage (damagePoints, shootHit.point);
			}
		} else {
			//laserLine.SetPosition (1, LaserBeamEnd.transform.position);
		}

		Invoke ("StopShooting", 0.15f);

		int randomNumber = Random.Range (1, 4);
		switch (randomNumber) {
		case 1:
			audios.PlayOneShot (laser1);
			break;

		case 2:
			audios.PlayOneShot (laser2);
			break;

		case 3:
			audios.PlayOneShot (laser3);
			break;

		case 4:
			audios.PlayOneShot (laser4);
			break;
			
		}

	}

	void StopShooting(){
		laserLine.enabled = false;
		isShooting = false;
		
	}

	public void DisableShooting(){
		isEnabled = false;
	}
}
