﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour {

	Text text;

	public static int [] diceNumbers = new int [5];
	public static int[] numCount = new int[6] { 0, 0, 0, 0, 0, 0 };

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		for (int x = 0; x < 5; x++) {
			diceNumbers[x] = Random.Range(1,6);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (GM.myTurn)
		{
			for (int x = 0; x < 5; x++)
			{
				diceNumbers[x] = GM.diceScore[x];
			}
			//Debug.Log(diceNumbers[0] + "/" + diceNumbers[1] + "/" + diceNumbers[2] + "/" + diceNumbers[3] + "/" + diceNumbers[4] + "/");
			text.text = diceNumbers[0].ToString() + " , " + diceNumbers[1].ToString() + " , " + diceNumbers[2].ToString() + " , " + diceNumbers[3].ToString() + " , " + diceNumbers[4].ToString();
		}
		else if (!GM.myTurn && GM.record_phase) {
			text.text = "상대가 기록중 입니다.";
			return;
		}
		else if (!GM.myTurn)
		{
			text.text = "상대가 플레이 중 입니다...";
			return;
		}

		if (GM.start_phase) { text.text = "Your Turn!"; }
		else if (GM.record_phase) { text.text = "Record your score"; }
		else { text.text = diceNumbers[0].ToString() + " , " + diceNumbers[1].ToString() + " , " + diceNumbers[2].ToString() + " , " + diceNumbers[3].ToString() + " , " + diceNumbers[4].ToString(); }
	}
}
