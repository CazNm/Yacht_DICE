using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class inputScore : MonoBehaviourPun
{

    public int scoreType;
    PhotonView photonview;

    // Start is called before the first frame update
    void Start()
    {
        photonview = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void iScore(GameObject self) {
        self.GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas").transform.Find("ScoreBoard").GetComponent<OpenScoreBoard>().LookPedigree();
        if (GM.myTurn)
        {
            GameObject.Find("GameManager").GetComponent<GM>().sendMessage("ChangeTurn", "turn change by photon sync");
            Score.check[scoreType] = 1;
        }
    }
}
