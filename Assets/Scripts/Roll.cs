using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviourPun
{

    Button BT;
    Text buttonText;
    PhotonView photonview;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        buttonText.text = "Roll! (" + GM.r_count + ")";
        photonview = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = "Roll! (" + GM.r_count + ")";
        if (!GM.myTurn)
        {
            //Debug.Log("false");
            GetComponent<Button>().interactable = false;
            //gameObject.SetActive (false);
        }
        else GetComponent<Button>().interactable = true;
    }   
    
    public void Rolling() {
        GM.selec_phase = false;
        Debug.Log("Roll by button");
        
        GameObject.Find("GameManager").GetComponent<GM>().sendPhase();
        //Debug.Log("rolling sequence");
        GameObject.Find("GameManager").GetComponent<GM>().sendRoll();
        GetComponent<Button>().interactable = false;
    }
}
