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
        }
    }   
    
    public void Rolling() {

        GM.selec_phase = false;
        GM.start_phase = false;

        GameObject.Find("GameManager").GetComponent<GM>().sendPhase();
        //Debug.Log("rolling sequence");
        GameObject.Find("GameManager").GetComponent<GM>().sendRoll();
       
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
