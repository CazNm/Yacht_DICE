using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class inputScore : MonoBehaviourPun
{

    public int scoreType;
    PhotonView photonview;
    RectTransform rectransform;

    // Start is called before the first frame update
    void Start()
    {
        
        photonview = PhotonView.Get(this);
        rectransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectransform.anchoredPosition = Vector3.zero;
        if (!GM.myTurn && Score.check[scoreType] == 0) { GetComponent<Text>().text = "0"; }
        else if (!GM.myTurn || Score.check[scoreType] == 1) { GetComponent<Button>().interactable = false; }
        else if (Score.check[scoreType] == 0 && GM.myTurn)
        {
            GetComponent<Text>().text = GM.scoreRecord[scoreType].ToString();
            GetComponent<Button>().interactable = true;
        }
        
    }

    public void iScore(GameObject self) {
        self.GetComponent<Button>().interactable = false;
        GM.protect = true;
        GameObject.Find("Canvas").transform.Find("ScoreBoard").GetComponent<OpenScoreBoard>().LookPedigree();
        if (GM.myTurn)
        {
            
            GameObject.Find("GameManager").GetComponent<GM>().sendSB(scoreType, GM.scoreRecord[scoreType]);
            Score.check[scoreType] = 1;
            GameObject.Find("GameManager").GetComponent<GM>().syncResultPhase();
            Invoke("changeTurn", 3f);
        }
    }

    void changeTurn() {
        GameObject.Find("GameManager").GetComponent<GM>().sendMessage("ChangeTurn", "turn change by photon sync");
    }
}
