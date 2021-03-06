﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class selector : MonoBehaviour
{
    GameObject self;
    Button btn;
    RectTransform rectTransform;
    public int dice_int;
    public Vector3 keep_position;
    public Vector3 select_position;
    public bool keep = false;

   
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();

        select_position = rectTransform.position;
        keep_position = new Vector3(keep_position.x, 370, 0);

        //Debug.Log(rectTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.myTurn && GM.start_phase) { keep = false; }

        GM.keep[dice_int] = keep;

        if (!keep)
        {
            rectTransform.position = select_position;
        }
        else {
            rectTransform.anchoredPosition = keep_position;
        }

    }
}
