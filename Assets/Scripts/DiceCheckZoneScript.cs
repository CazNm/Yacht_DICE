using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 diceVelocity1;
	Vector3 diceVelocity2;
	Vector3 diceVelocity3;
	Vector3 diceVelocity4;
	Vector3 diceVelocity5;

	// Update is called once per frame
	void FixedUpdate () {
		diceVelocity1 = GameObject.Find("dice1(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity2 = GameObject.Find("dice2(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity3 = GameObject.Find("dice3(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity4 = GameObject.Find("dice4(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity5 = GameObject.Find("dice5(Clone)").GetComponent<DiceScript>().diceVelocity;
	}

	void OnTriggerStay(Collider col)
	{
		DiceStop(diceVelocity1, 0, col , "dice1(Clone)");
		DiceStop(diceVelocity2, 1, col , "dice2(Clone)");
		DiceStop(diceVelocity3, 2, col , "dice3(Clone)");
		DiceStop(diceVelocity4, 3, col , "dice4(Clone)");
		DiceStop(diceVelocity5, 4, col , "dice5(Clone)");
	}

	void DiceStop(Vector3 diceVel, int dice_no, Collider col, string Dice) {


		if (diceVel.x == 0f && diceVel.y == 0f && diceVel.z == 0f && col.gameObject.transform.parent.name == Dice)
		{
			//Debug.Log( "Dice" + (dice_no + 1) + "stopped");
			int diceNo = dice_no;

			GM.diceStop[diceNo] = true;
			switch (col.gameObject.name)
			{
				case "Side1":
					DiceNumberTextScript.diceNumbers[diceNo] = 6;
					break;
				case "Side2":
					DiceNumberTextScript.diceNumbers[diceNo] = 5;
					break;
				case "Side3":
					DiceNumberTextScript.diceNumbers[diceNo] = 4;
					break;
				case "Side4":
					DiceNumberTextScript.diceNumbers[diceNo] = 3;
					break;
				case "Side5":
					DiceNumberTextScript.diceNumbers[diceNo] = 2;
					break;
				case "Side6":
					DiceNumberTextScript.diceNumbers[diceNo] = 1;
					break;
			}
		}

	}
}
