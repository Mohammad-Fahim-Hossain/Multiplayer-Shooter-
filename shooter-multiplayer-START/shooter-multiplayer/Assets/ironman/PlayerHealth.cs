using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mirror;

public class PlayerHealth : NetworkBehaviour {

	public int startingHealth = 100;
    [SyncVar]
    public int currentHealth = 100;

    public Text HealtText;


	float shakingTimer = 0;
	public float timeToShake = 1.0f;
	public float shakeIntensity = 3.0f;
	bool isShaking = false;

	public bool isDead = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();

        HealtText = GameObject.Find("HPtext").GetComponent<Text>();
		
	}

	// Update is called once per frame
	void Update () {
        //healthText.text = "HP: " + currentHealth.ToString ();

        UpdateHP();

		if (isShaking == true && shakingTimer < timeToShake) {
			shakingTimer += Time.deltaTime;
			float x = Mathf.PerlinNoise (Camera.main.transform.position.x, Camera.main.transform.position.y);
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + x * shakeIntensity, Camera.main.transform.position.y, Camera.main.transform.position.z);

			if (shakingTimer > timeToShake) {
				isShaking = false;
			}
		}

        if (!isDead)
        {
            if (currentHealth <= 0)
            {
                Death();
            }
        }

    }


	public void TakeDamage(int amount){
        if (!isServer)
        {
            return;
        }

        if (isDead)
			return;

		//ShakeCamera ();

		currentHealth -= amount;
		
	}

	public void Death(){
		if (isDead)
			return;
			
		anim.SetTrigger ("Death");
		//agent.enabled = false;
		isDead = true;
		currentHealth = 0;
		//Destroy (gameObject, 2.5f);
	}

	void ShakeCamera(){
		shakingTimer = 0;
		isShaking = true;
		
	}

    void UpdateHP()
    {
        if (isLocalPlayer)
        {
            HealtText.text = " HP: " + currentHealth.ToString();
        }
    }


}
