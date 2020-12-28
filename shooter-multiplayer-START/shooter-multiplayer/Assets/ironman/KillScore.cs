using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KillScore : NetworkBehaviour
{
    [SyncVar] public int Socre=0;

    public GameObject ScoreText;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            ScoreText = GameObject.Find("KillScoreText");
            ScoreText.GetComponent<Text>().text = "Score :" + Socre.ToString();
        }
    }
}
