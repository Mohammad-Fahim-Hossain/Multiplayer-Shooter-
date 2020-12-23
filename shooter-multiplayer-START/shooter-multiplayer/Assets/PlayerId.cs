using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerId : NetworkBehaviour
{
    [SyncVar] public string PlayerUniqueName;
    private string PlayerNetID;

    public override void OnStartLocalPlayer()
    {
        // get the network ID
        //set the ID of the player

        GetNetIdentity();
        SetIdentity();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.name=="" || transform.name == "ironmanPrefab(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        PlayerNetID = GetComponent<NetworkIdentity>().netId.ToString();
        //tell all the clients about the player ID
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

   void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            this.transform.name = PlayerUniqueName;
        }
        else
        {
            this.transform.name = MakeUniqueIdentity();
        }

    }

   public string MakeUniqueIdentity()
    {
        return "Player" + PlayerNetID;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        PlayerUniqueName = name;
    }
}
