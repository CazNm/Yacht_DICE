using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour {

	Text text;
	public static int diceNumber1;
	public static int diceNumber2;
	public static int diceNumber3;
	public static int diceNumber4;
	public static int diceNumber5;
	public static int diceNumber6;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = diceNumber1.ToString()+ "," + diceNumber2.ToString() + "," + diceNumber3.ToString() + "," + diceNumber4.ToString() + diceNumber5.ToString();
	}
}
