using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p2scoreInput : MonoBehaviour
{
    public int scoreType;
    public int score;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score = GM.p2scoreRec[scoreType];
        text.text = score.ToString();
    }
}
