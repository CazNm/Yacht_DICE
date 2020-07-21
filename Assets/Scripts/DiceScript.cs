using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	Rigidbody rb;
	Transform transform;

	
	private float rdirX;
	private float rdirZ;

	public Vector3 currentPos;
	public static Vector3 diceVelocity;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		transform = this.GetComponent<Transform>();
		rdirX = Random.Range(-4, 4);
		rdirZ = Random.Range(-4, 4);

		float rotX = Random.Range(0, 360);
		float rotY = Random.Range(0, 360);
		float rotZ = Random.Range(0, 360);

		transform.position = new Vector3(rdirX, 1f, rdirZ);
		transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
		currentPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;
		currentPos = transform.position;

		if (currentPos.y < -0.5f)
        {
			Debug.Log("out of box");
			transform.position = new Vector3(rdirX, 1f ,rdirZ);
        }


	}

	public void Reroll() {
		diceVelocity = rb.velocity;
		float dirX = Random.Range(100, 500);
		float dirY = Random.Range(100, 500);
		float dirZ = Random.Range(100, 500);
		transform.position = new Vector3(currentPos.x, 5, currentPos.z);
		//transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * 500);
		rb.AddTorque(dirX, dirY, dirZ);
	}
}
