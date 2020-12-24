using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRespawn : NetworkBehaviour
{
    public bool isRespawn = false;

    // Start is called before the first frame update
    void Start()
    {
        isRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<PlayerHealth>().currentHealth<=0 && !isRespawn)
        {
            Invoke("RpcReSpawn", 2.5f);
        }
        
    }

    [ClientRpc]
    void RpcReSpawn()
    {
        Transform Position = NetworkManager.singleton.GetStartPosition();
        this.transform.position = Position.position;

        GetComponent<PlayerHealth>().currentHealth = GetComponent<PlayerHealth>().startingHealth;

        GetComponent<PlayerHealth>().isDead = false;

        GetComponent<Animator>().Play("IdleWalk");

        isRespawn = true;
    }

}
