using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputScore : MonoBehaviour
{

    public int scoreType;

    // Start is called before the first frame update
    void Start()
    {
        
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

            GM.myTurn = false;
            GM.p2Turn = true;
            GM.start_phase = true;
            
        }
        else {
  
            GM.p2Turn = false;
            GM.myTurn = true;
            GM.start_phase = true;
        }
        Score.check[scoreType] = 1;
    }
}
