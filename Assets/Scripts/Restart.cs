using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartSequence()
    {
        GM.resulton = false;
        GM.resultcount = 0;

        GM.round += 1;

        GM.myTurn = true;
        GM.p2Turn = false;

        GM.ycheck = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        GM.ccheck = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        GM.resultboard.SetActive(false);
    }
}
