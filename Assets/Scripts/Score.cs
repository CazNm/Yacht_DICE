using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameObject sboard;
    DiceNumberTextScript DNum;

    // Start is called before the first frame update
    void Start()
    {
        sboard = GameObject.Find("Pedigree");
        int textcount = sboard.transform.childCount;
        for (int i = 1; i < textcount; i++)
        {
            sboard.transform.GetChild(i).GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int num0 = DNum.diceNumbers[0];
       // Debug.Log(num0);
    }
}
