using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb;
	Vector3 currentPos;
	public static Vector3 diceVelocity;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		float rdirX = Random.Range(-4, 4);
		float rdirZ = Random.Range(-4, 4);
		
		transform.position = new Vector3(rdirX, 0.5f, rdirZ);
		currentPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;
	}

	public void Reroll() {
		diceVelocity = rb.velocity;
		DiceNumberTextScript.diceNumber = 0;
		float dirX = Random.Range(100, 500);
		float dirY = Random.Range(100, 500);
		float dirZ = Random.Range(100, 500);
		transform.position = new Vector3(currentPos.x, 2, currentPos.z);
		transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * 500);
		rb.AddTorque(dirX, dirY, dirZ);
	}
}
