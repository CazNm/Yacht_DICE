using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour {

	Text text;

	public static int [] diceNumbers = new int [5];

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GM.playerTurn)
		{
			text.text = diceNumbers[0].ToString() + " , " + diceNumbers[1].ToString() + " , " + diceNumbers[2].ToString() + " , " + diceNumbers[3].ToString() + " , " + diceNumbers[4].ToString();
		}
		if (GM.start_phase) { text.text = "Your Turn!"; }
		else if (GM.record_phase) { text.text = "Record your score"; }
		else { text.text = diceNumbers[0].ToString() + " , " + diceNumbers[1].ToString() + " , " + diceNumbers[2].ToString() + " , " + diceNumbers[3].ToString() + " , " + diceNumbers[4].ToString(); }
	}
}
