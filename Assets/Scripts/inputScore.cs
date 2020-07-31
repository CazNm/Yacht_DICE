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
        if (!GM.myTurn || Score.check[scoreType] == 1) { GetComponent<Button>().interactable = false; }
        else if(Score.check[scoreType] == 0 && GM.myTurn  ) {
            GetComponent<Text>().text = GM.scoreRecord[scoreType].ToString();
            GetComponent<Button>().interactable = true; 
        }
    }

    public void iScore(GameObject self) {
        self.GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas").transform.Find("ScoreBoard").GetComponent<OpenScoreBoard>().LookPedigree();
        if (GM.myTurn)
        {
            GameObject.Find("GameManager").GetComponent<GM>().sendSB(scoreType, GM.scoreRecord[scoreType]);
            GameObject.Find("GameManager").GetComponent<GM>().sendMessage("ChangeTurn", "turn change by photon sync");
            Score.check[scoreType] = 1;
        }
    }
}
