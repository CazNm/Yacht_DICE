﻿using Photon.Pun;
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
		diceVelocity1 = GameObject.Find($"{GM.dices[0].name}(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity2 = GameObject.Find($"{GM.dices[1].name}(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity3 = GameObject.Find($"{GM.dices[2].name}(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity4 = GameObject.Find($"{GM.dices[3].name}(Clone)").GetComponent<DiceScript>().diceVelocity;
		diceVelocity5 = GameObject.Find($"{GM.dices[4].name}(Clone)").GetComponent<DiceScript>().diceVelocity;
		if (PhotonNetwork.IsMasterClient) { GameObject.Find("GameManager").GetComponent<GM>().sendPoint(); }
	}

	void OnTriggerStay(Collider col)
	{
		DiceStop(diceVelocity1, 0, col , $"{GM.dices[0].name}(Clone)");
		DiceStop(diceVelocity2, 1, col , $"{GM.dices[1].name}(Clone)");
		DiceStop(diceVelocity3, 2, col , $"{GM.dices[2].name}(Clone)");
		DiceStop(diceVelocity4, 3, col , $"{GM.dices[3].name}(Clone)");
		DiceStop(diceVelocity5, 4, col , $"{GM.dices[4].name}(Clone)");
	}

	void DiceStop(Vector3 diceVel, int dice_no, Collider col, string Dice) {


		if (diceVel.x == 0f && diceVel.y == 0f && diceVel.z == 0f && col.gameObject.transform.parent.name == Dice)
		{
			if (!PhotonNetwork.IsMasterClient) { return; }
			//Debug.Log( "Dice" + (dice_no + 1) + "stopped");
			int diceNo = dice_no;

			GM.diceStop[diceNo] = true;
			GameObject.Find("GameManager").GetComponent<GM>().sendStop(GM.diceStop[diceNo], diceNo);
			switch (col.gameObject.name)
			{
				case "Side1":
					GM.diceScore[diceNo] = 6;
					break;
				case "Side2":
					GM.diceScore[diceNo] = 5;
					break;
				case "Side3":
					GM.diceScore[diceNo] = 4;
					break;
				case "Side4":
					GM.diceScore[diceNo] = 3;
					break;
				case "Side5":
					GM.diceScore[diceNo] = 2;
					break;
				case "Side6":
					GM.diceScore[diceNo] = 1;
					break;
			}
		}

	}
}
