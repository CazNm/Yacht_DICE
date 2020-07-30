using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using JetBrains.Annotations;

public class GM : MonoBehaviourPunCallbacks
{
    public static int playerIndex;
    public static int otherIndex;
    public GameObject[] dices = new GameObject[5];
    //public GameObject player;

    public static int r_count;
    public static int round;
    public static bool myTurn;
    public static bool p2Turn;
    public static Vector3[] rotation = { new Vector3(90, 0, 0), new Vector3(0, 90, -90), new Vector3(0, 0, 0), new Vector3(180, 0, 0), new Vector3(0, 0, 90), new Vector3(-90, 0, 0) };
    public static int[] diceScore = { 0, 0, 0, 0, 0 };
    public static int[] scoreRecord = new int[12];
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };
    public static GameObject[] s_ui;

    public static bool start_game;
    public static bool start_phase = true;
    public static bool semiResult = false;
    public static bool selec_phase = false;
    public static bool record_phase = false;
    public static bool rolling_phase = false;

 
    GameObject dice1;
    GameObject dice2;
    GameObject dice3;
    GameObject dice4;
    GameObject dice5;

    public static GameObject scoreBoard;
    Button rollButton;

    PhotonView photonview;
    

    //시스템적으로 선공 후공 결정해야됨, 일단은 사람 vs 컴으로 구성 사람이 선공인 상황을 가정
    // Start is called before the first frame update

    void Start()
    {
        start_game = true;
        rollButton = GameObject.Find("Canvas").transform.Find("RollButton").GetComponent<Button>();
        scoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;
        photonview = PhotonView.Get(this);
        r_count = 3;
       
    }

    // Update is called once per frame
    void Update()
    {

        if ( myTurn ) { sendPhase(); }

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        
        /*if (PhotonNetwork.CurrentRoom.PlayerCount != 2) {
            GameObject.Find("Canvas").transform.Find("WTimg").gameObject.SetActive(true);
            start_game = true;
            return; 
        }*/

        if (start_game) {
            rollButton.interactable = true;
            spawnDice();
            setTurn();
            Debug.Log(playerIndex);
            round = 1;
            start_game = false;
        } //여기서 호스트가 먼저 생성해야될 것들을 먼저 생성한다.

        //동기화 되어야 하는 물체가 다 로딩이 되었는지 체크하는 로직 생성이나 동기화가 덜 되어서 게임 로직으로 넘어가는 것을 방지
        if (dice1 != null && dice2 != null && dice3 != null && dice4 != null && dice5 != null)
        {
            GameObject.Find("Canvas").transform.Find("NL").gameObject.SetActive(false);
        }
        else {
            dice1 = GameObject.Find("dice1(Clone)");
            dice2 = GameObject.Find("dice2(Clone)");
            dice3 = GameObject.Find("dice3(Clone)");
            dice4 = GameObject.Find("dice4(Clone)");
            dice5 = GameObject.Find("dice5(Clone)");
            GameObject.Find("Canvas").transform.Find("NL").gameObject.SetActive(true);
            return; 
        }

        GameObject.Find("Canvas").transform.Find("WTimg").gameObject.SetActive(false);

        if (!myTurn) {
            rollButton.interactable = false;
            scoreBoard.GetComponent<Button>().interactable = false;
        }
        // 마스터 클라이언트라면 마스터 클라이언트에서 계속 턴을 진행 하는 로직을 진행... 문제는 이제 UI나 다른것들에 대한 판정을 RPC로 전달해야됨.

        if (myTurn && diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(true);
            selec_phase = true;

        }
        // mainLogic();
        if (r_count == 0) { photonView.RPC("ChangeTurn", RpcTarget.All, "!!"); }
    }


    void spawnDice() {
       // PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.Euler(0, 0, 0));
        if (!PhotonNetwork.IsMasterClient) { return; }
        for (int x = 0; x < 5; x++) {
            PhotonNetwork.Instantiate(dices[x].name, dices[x].GetComponent<DiceScript>().resultPos, Quaternion.Euler(0, 0, 0));
        }
    }

    void setTurn() {

        playerIndex =  PhotonNetwork.LocalPlayer.ActorNumber - 1;
        Debug.Log(playerIndex + "!!!");
        if (playerIndex == 1)
        {
            myTurn = false;
        }
        else
        {
            myTurn = true;
        }
    }
    void StartPhase() {

        Debug.Log("start phase");

        r_count = 4;
        semiResult = false;
        selec_phase = false;
        record_phase = false;

        diceStop = new bool[5] { false, false, false, false, false};
        keep = new bool[5] { false, false, false, false, false };

        if (PhotonNetwork.IsMasterClient) {
            dice1.GetComponent<Rigidbody>().useGravity = false;
            dice2.GetComponent<Rigidbody>().useGravity = false;
            dice3.GetComponent<Rigidbody>().useGravity = false;
            dice4.GetComponent<Rigidbody>().useGravity = false;
            dice5.GetComponent<Rigidbody>().useGravity = false;
        }
        

        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);

        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(true);
        
        if (!myTurn) { rollButton.interactable = false; }
        else { 
            rollButton.interactable = true;
            GameObject.Find("Canvas").transform.Find("RollButton").gameObject.SetActive(true);
        }
        //scoreBoard.GetComponent<Button>().interactable = false;

    }

    void allKeep() {
        if (keep[0] && keep[1] && keep[2] && keep[3] && keep[4])
        {
            rollButton.interactable = false;
        }
        else {
            rollButton.interactable = true;
        }
    }

    [PunRPC]
    public void Rolldice()
    {

        selec_phase = false;
        start_phase = false;
        rolling_phase = true;
        r_count -= 1;
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(false);

       

        // Rolldice() 호출시에 마스터 클라이 언트에서 주사위를 굴리도록 함

        Debug.Log("Local rolling");
        if (GM.r_count > 0)
        {
            if (!keep[0])
            {
                diceStop[0] = false;
                if (PhotonNetwork.IsMasterClient) {
                    dice1.GetComponent<DiceScript>().Reroll();
                    dice1.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (!keep[1])
            {
                diceStop[1] = false;
                if (PhotonNetwork.IsMasterClient) {
                    dice2.GetComponent<DiceScript>().Reroll();
                    dice2.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (!keep[2])
            {
                diceStop[2] = false;
                if (PhotonNetwork.IsMasterClient) {
                    dice3.GetComponent<DiceScript>().Reroll();
                    dice3.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (!keep[3])
            {
                diceStop[3] = false;
                if (PhotonNetwork.IsMasterClient) {
                    dice4.GetComponent<DiceScript>().Reroll();
                    dice4.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (!keep[4])
            {
                diceStop[4] = false;
                if (PhotonNetwork.IsMasterClient) {
                    dice5.GetComponent<DiceScript>().Reroll();
                    dice5.GetComponent<Rigidbody>().useGravity = true;
                }
            }

            semiResult = true;
            sendPhase();
        }
    }

    void selectPhase()
    {
        
        dice1.GetComponent<Rigidbody>().useGravity = false;
        dice2.GetComponent<Rigidbody>().useGravity = false;
        dice3.GetComponent<Rigidbody>().useGravity = false;
        dice4.GetComponent<Rigidbody>().useGravity = false;
        dice5.GetComponent<Rigidbody>().useGravity = false;

       // Debug.Log("selectPhase");
        avtiveSelectUI();
    }

    void avtiveSelectUI()
    {
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(true);
    }

   public void KeepSelect(GameObject button)
    {
       // Debug.Log("select logic");
        bool state_check = button.GetComponent<selector>().keep;
        if (!state_check)
        {
          //  Debug.Log("keep!");
            button.GetComponent<selector>().keep = true;
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Button>().interactable = true;
            sendPhase();
        }
        else {
        //    Debug.Log("Ready to roll");
            button.GetComponent<selector>().keep = false;
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Button>().interactable = true;
            sendPhase();
        }
    }

    public void sendMessage(string functionName, string message) {
        PhotonView photonview = PhotonView.Get(this);
        photonview.RPC(functionName, RpcTarget.All, message);
    }

    public void sendPhase() {
        PhotonView photonview = PhotonView.Get(this);
        photonview.RPC("syncPhase", RpcTarget.All, start_phase, semiResult, selec_phase, record_phase, rolling_phase ,keep);
    }

    public void sendPoint() {
        PhotonView photonview = PhotonView.Get(this);
        photonview.RPC("syncPoint", RpcTarget.All, diceScore[0], diceScore[1], diceScore[2], diceScore[3], diceScore[4]);
    }

    public void sendRoll() {
       // Debug.Log("RPC ROll");
        photonView.RPC("Rolldice", RpcTarget.All);
    }

    public void sendStop(bool diceStopS,int x) {
        photonView.RPC("syncVelo", RpcTarget.All, diceStopS, x);
    }


    [PunRPC]
    public void syncVelo(bool diceStopS, int x) {
        if (!PhotonNetwork.IsMasterClient) {
            diceStop[x] = diceStopS;
        }
    }
    [PunRPC]
    public void ChangeTurn(string message) {
        //   Debug.Log(message);

        r_count = 3;
        if (myTurn) { 
            myTurn = false;
            GM.record_phase = false;
            GM.start_phase = true;
        }
        else  { 
            myTurn = true;
            GM.record_phase = false;
            GM.start_phase = true;
        }
    }

    [PunRPC]
    public void syncRecord(int[] recordScore) {
        if (myTurn) { return; }
        for (int x = 0; x < 12; x++) {
            GM.scoreRecord[x] = recordScore[x];
        }
    }
    
    [PunRPC]
    public void syncPoint(int dice1, int dice2 , int dice3, int dice4, int dice5) {

        if (myTurn) { return; }

        diceScore[0] = dice1;
        diceScore[1] = dice2;
        diceScore[2] = dice3;
        diceScore[3] = dice4;
        diceScore[4] = dice5;

        Debug.Log("sync point: "+ dice1 + " / " + dice2 + " / " + dice3 + " / " + dice4 + " / " + dice5);
    }

    [PunRPC]
    public void syncPhase(bool startSync, bool semiSync, bool selecSync, bool recordSync,bool rollSync, bool [] keepSync) {
    //    Debug.Log("sync turn state");

        if (myTurn) {
            return;
        }

        start_phase = startSync;
        semiResult = semiSync;
        selec_phase = selecSync;
        record_phase = recordSync;
        rolling_phase = rollSync;

        Debug.Log(startSync + " / " + semiSync + " / " + selecSync + " / " + recordSync + " / " + rollSync);

        for (int x = 0; x < keep.Length; x++) {
            keep[x] = keepSync[x];
        }
    }
}