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
        if (GM.playerOb.GetComponent<playerStat>().isMyturn)
        {

            GM.playerOb.GetComponent<playerStat>().isMyturn = false;
            //GM.otherOb.GetComponent<playerStat>().isMyturn = true;
            GM.start_phase = true;
            
        }
        else {
  
            //GM.otherOb.GetComponent<playerStat>().isMyturn = false;
            GM.playerOb.GetComponent<playerStat>().isMyturn = true;
            GM.start_phase = true;
        }
        Score.check[scoreType] = 1;
    }
}
