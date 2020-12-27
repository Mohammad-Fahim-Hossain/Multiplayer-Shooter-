using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerRespawn : NetworkBehaviour
{
    public bool isRespawn = false;

    public int CountDownStartingValue = 9;
    private int CountDownCurrentValue;

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
            isRespawn = true;

            Invoke("RpcReSpawn", 9.0f);
            if (isLocalPlayer)
            {
               
                GameObject textRespawnObj = GameObject.Find("RespawnText");
               Text RespawnText = textRespawnObj.GetComponent<Text>();
                CountDownCurrentValue = CountDownStartingValue;
                 InvokeRepeating("UpdateRespawnText", 1.0f, 1.0f);

               RespawnText.text = CountDownCurrentValue.ToString();
            }
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

        //isRespawn = true;
    }

    void UpdateRespawnText()
    {
        GameObject textRespawnObj = GameObject.Find("RespawnText");
        Text RespawnText = textRespawnObj.GetComponent<Text>();
        CountDownCurrentValue--;
        RespawnText.text = CountDownCurrentValue.ToString();
        print(CountDownCurrentValue.ToString());
        if (CountDownCurrentValue <= 0)
        {
            CancelInvoke("UpdateRespawnText");
            RespawnText.text = "";
        }
    }

}
