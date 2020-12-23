using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using DigitalRuby.PyroParticles;
using Mirror;

public class PlayerShoot : NetworkBehaviour {


	RaycastHit shootHit;
	Ray shootRay;
	
	
	bool isShooting = false;

	public int damagePoints = 10;

	public bool isEnabled = true;

	

    public GameObject[] ProhectilePreFabs;
    private GameObject SelectedProjectilePreFab;
    private GameObject CurrentPreFabOject;
    FireBaseScript CurrentPreFabScript;
    public GameObject ProjectileSpawnPoint;


	// Use this for initialization
	void Start () {
		
		

        IntiallizeProjectile();

	
	}

    // Update is called once per frame
    void Update() {

        if (!isLocalPlayer)
            return;
#if !MOBILE_INPUT
        if (Input.GetButtonDown("Fire1") && isShooting == false && isEnabled == true) {
            CmdShoot();

        }
#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			CmdShoot();
		}
#endif

    }

    [Command]
	public void CmdShoot(){
		isShooting = true;

        SpawnProjectile();
		

		Invoke ("StopShooting", 0.15f);

		

	}

	void StopShooting(){
		
		isShooting = false;
		
	}

	public void DisableShooting(){
		isEnabled = false;
	}

   public void IntiallizeProjectile()
    {
        int selected = Random.Range(1, 1000) % ProhectilePreFabs.Length;
        SelectedProjectilePreFab = ProhectilePreFabs[selected];
    }

    public void SpawnProjectile()
    {
        CurrentPreFabOject = GameObject.Instantiate(SelectedProjectilePreFab);
        CurrentPreFabOject.transform.position = ProjectileSpawnPoint.transform.position;
        CurrentPreFabOject.transform.rotation = ProjectileSpawnPoint.transform.rotation;
       
        
        CurrentPreFabOject.GetComponent<FireProjectileScript>().OwnerName = transform.name;

        NetworkServer.Spawn(CurrentPreFabOject);
    }
}
