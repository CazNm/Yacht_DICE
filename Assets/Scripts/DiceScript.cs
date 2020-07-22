using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	Rigidbody rb;
	Transform transform;

	
	private float rdirX;
	private float rdirZ;

	public Vector3 resultPos;
	//public Vector3 keepPos;
	//public Vector3 selectKpos;
	public Vector3 currentPos;
	public Vector3 diceVelocity;
	public int dice_no;
	public int diceResult;

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
			//Debug.Log("out of box");
			transform.position = new Vector3(rdirX, 1f ,rdirZ);
        }

		if (GM.start_phase) {
			transform.position = resultPos;
		} //턴 시작 페이즈 주사위 위치


		//위치 설정 코드
		if (GM.keep[dice_no] && GM.selec_phase) {
			diceResult = DiceNumberTextScript.diceNumbers[dice_no];
			transform.position = new Vector3(resultPos.x, resultPos.y, 3.8f);
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);
			
		} // 다이스가 킵이고 셀렉트 페이즈 일때

		if (!GM.keep[dice_no] && GM.selec_phase) {
			diceResult = DiceNumberTextScript.diceNumbers[dice_no];
			transform.position = resultPos;
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);
			
		}// 다이스를 킵하지 않은 상태이고 셀렉트 페이즈 일때

		if ((GM.keep[dice_no] && !GM.selec_phase) || GM.record_phase) {
			diceResult = DiceNumberTextScript.diceNumbers[dice_no];
			transform.position = new Vector3(resultPos.x, resultPos.y, 5.5f);
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);
			//다이스를 킵한 상태이고 셀렉트 페이즈가 아닐 때 그냥 주사위 굴러갈때 킵한거임.
		}


	}

	public void Reroll() {

		diceVelocity = rb.velocity;

		float dirX = Random.Range(0, 500);
		float dirY = Random.Range(0, 500);
		float dirZ = Random.Range(0, 500);
		transform.position = new Vector3(currentPos.x, 5, currentPos.z);
		//transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * 500);
		rb.AddTorque(new Vector3 (dirX, dirY, dirZ) );

	}
}
