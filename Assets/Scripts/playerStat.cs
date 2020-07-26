using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class playerStat : MonoBehaviourPun
{
    // Start is called before the first frame update
    public bool isMyturn;
    public int[] score = new int [15] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    void Start()
    {
        if (photonView.IsMine)
        {
            gameObject.name = "player";
        }
        else
        {
            gameObject.name = "other";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            gameObject.name = "player";
        }
        else {
            gameObject.name = "other";
        }
    }
}
