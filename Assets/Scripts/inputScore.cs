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
    bool send = true;

    // Start is called before the first frame update
    void Start()
    {
        send = true;
        photonview = PhotonView.Get(this);
        rectransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectransform.anchoredPosition = Vector3.zero;
        if(send && scoreType == 12 || scoreType == 13 || scoreType == 14)
        {

            if (GM.scoreRecord[scoreType] == 0)
            {
                GetComponent<Text>().text = "...";
                GetComponent<Button>().interactable = false;
            }
            else {
                GameObject.Find("GameManager").GetComponent<GM>().sendSB(scoreType, GM.scoreRecord[scoreType]);
                GetComponent<Text>().text = GM.scoreRecord[scoreType].ToString();
                GetComponent<Button>().interactable = false;
                send = false;
            }
        }
        else if (!GM.myTurn && Score.check[scoreType] == 0) { GetComponent<Text>().text = "0"; }
        else if (!GM.myTurn || Score.check[scoreType] == 1) {
            GetComponent<Text>().text = GM.scoreRecord[scoreType].ToString();
            GetComponent<Button>().interactable = false; 
        }
        else if (Score.check[scoreType] == 0 && GM.myTurn)
        {
            GetComponent<Text>().text = GM.scoreRecord[scoreType].ToString();
            GetComponent<Button>().interactable = true;
        }
        
    }

    public void iScore(GameObject self) {
        self.GetComponent<Button>().interactable = false;
        GetComponent<Text>().color = Color.green;
        GM.protect = true;
        GameObject.Find("Canvas").transform.Find("ScoreBoard").GetComponent<OpenScoreBoard>().LookPedigree();
        if (GM.myTurn)
        {
            
            GameObject.Find("GameManager").GetComponent<GM>().sendSB(scoreType, GM.scoreRecord[scoreType]);
            Score.check[scoreType] = 1;
            GameObject.Find("GameManager").GetComponent<GM>().syncResultPhase();
            Score.totalScore();
            Score.bounus_sum();
            Invoke("changeTurn", 3f);
        }
    }

    void changeTurn() {
        GameObject.Find("GameManager").GetComponent<GM>().sendMessage("ChangeTurn", "turn change by photon sync");
    }
}
