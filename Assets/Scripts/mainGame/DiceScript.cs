using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiceScript : MonoBehaviourPunCallbacks, IPunObservable {

	Rigidbody rb;
	Transform transform;

	
	private float rdirX;
	private float rdirZ;

	float rotX;
	float rotY;
	float rotZ;


	public Vector3 resultPos;
	//public Vector3 keepPos;
	//public Vector3 selectKpos;
	public Vector3 currentPos;
	public Quaternion currentRot;
	public Vector3 diceVelocity;
	public int dice_no;
	public int diceResult;

	// Use this for initialization

	void Awake() {
		Debug.Log("settin1g");
		photonView.Synchronization = ViewSynchronization.Unreliable;
		photonView.ObservedComponents[0] = this;

		rb = this.GetComponent<Rigidbody>();
		transform = this.GetComponent<Transform>();

		currentPos = transform.position;
		currentRot = transform.rotation;

	}
	void Start () {
		Debug.Log("setting");
		
		

		if (!photonView.IsMine) {
			rb.isKinematic = true;
			return; 
		}

		rdirX = Random.Range(-4, 4);
		rdirZ = Random.Range(-4, 4);

		 rotX = Random.Range(0, 360);
		 rotY = Random.Range(0, 360);
		 rotZ = Random.Range(0, 360);

		transform.position = new Vector3(rdirX, 1f, rdirZ);
		transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
		
		
	}

	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;
		
        if (!photonView.IsMine) {
			
			transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * 20f);
			transform.rotation = Quaternion.Slerp(transform.rotation, currentRot, Time.deltaTime * 20f);
			return; 
		}

		if (GM.start_phase)
		{
			transform.position = resultPos;
		} //턴 시작 페이즈 주사위 위치

		if (transform.position.y < -0.5f || transform.position.x > 6 || transform.position.x < -6 || transform.position.z > 12 || transform.position.z < -12)
        {
			//Debug.Log("out of box");
			transform.position = new Vector3(rdirX, 1f ,rdirZ);
        }

		//위치 설정 코드
		if (GM.keep[dice_no] && GM.selec_phase)
		{
			diceResult = GM.diceScore[dice_no];
			transform.position = new Vector3(resultPos.x, resultPos.y, 3.8f);
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);

		} // 다이스가 킵이고 셀렉트 페이즈 일때

		if (!GM.keep[dice_no] && GM.selec_phase)
		{
			diceResult = GM.diceScore[dice_no];
			transform.position = resultPos;
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);

		}// 다이스를 킵하지 않은 상태이고 셀렉트 페이즈 일때

		if (GM.record_phase || (GM.keep[dice_no] && !GM.selec_phase))
		{
			diceResult = GM.diceScore[dice_no];
			transform.position = new Vector3(resultPos.x, resultPos.y, 5.5f);
			transform.rotation = Quaternion.Euler(GM.rotation[diceResult - 1]);
			//다이스를 킵한 상태이고 셀렉트 페이즈가 아닐 때 그냥 주사위 굴러갈때 킵한거임.
		}

	}

	public void Reroll() {

		Debug.Log("rool");
		diceVelocity = rb.velocity;

		float dirX = Random.Range(0, 500);
		float dirY = Random.Range(0, 500);
		float dirZ = Random.Range(0, 500);
		transform.position = new Vector3(transform.position.x, 5, transform.position.z);
		//transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * 500);
		rb.AddTorque(new Vector3 (dirX, dirY, dirZ) );

	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		//throw new System.NotImplementedException();

		if (stream.IsWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(rb.useGravity);
		}
		else {
			currentPos = (Vector3)stream.ReceiveNext();
			currentRot =(Quaternion)stream.ReceiveNext();
		}
    }

    private void OnCollisionEnter(Collision col)
    {
		Debug.Log("hit!");
		GameObject.Find("GameManager").GetComponent<GM>().syncSound(dice_no);
	}
}
