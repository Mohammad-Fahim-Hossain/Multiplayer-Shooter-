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

		

        if (!isDead)
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
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

	

		currentHealth -= amount;
		
	}

	public void Death(){
		if (isDead)
			return;

        //anim.SetTrigger ("Death");
        //agent.enabled = false;
        //isDead = true;
        //currentHealth = 0;
        //Destroy (gameObject, 2.5f);

        if (isServer)
        {
            RpcDead();
        }
        else
        {
            anim.SetTrigger ("Death");
          
            isDead = true;
        }
    }

    [ClientRpc]
    public void RpcDead()
    {
        if (isDead)
            return;
        anim.SetTrigger("Death");
        isDead = true;
        currentHealth = 0;
    }


    void UpdateHP()
    {
        if (isLocalPlayer)
        {
            HealtText.text = " HP: " + currentHealth.ToString();
        }
    }


}
