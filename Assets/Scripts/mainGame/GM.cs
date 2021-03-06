﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using System;

public class GM : MonoBehaviourPunCallbacks
{
    public static int playerIndex;
    public static int otherIndex;
    
    public static GameObject[] dices = new GameObject[5];
    //public GameObject player;

    public static int r_count;
    public static int round;
    public static bool myTurn;
    public static bool p2Turn;
    public static Vector3[] rotation = { new Vector3(90, 0, 0), new Vector3(0, 0, 90), new Vector3(0, 0, 0), new Vector3(180, 0, 0), new Vector3(0, 90, -90), new Vector3(-90, 0, 90) };
    public static int[] diceScore = { 0, 0, 0, 0, 0 };
    public static int[] scoreRecord = new int[15];
    public static int? [] p2scoreRec = new int? [15];
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };
    public static GameObject[] s_ui;

    public static bool start_game;
    public static bool start_phase = true;
    public static bool semiResult = false;
    public static bool selec_phase = false;
    public static bool record_phase = false;
    public static bool rolling_phase = false;
    public static bool protect = false;
    public static bool disActive = true;
    public static bool p2Leave = false;
    public static bool waiting = true;
    public static bool timeOver = false;

    public static float timer;
    public static float endTime;

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
        start_phase = true;
        semiResult = false;
        selec_phase = false;
        record_phase = false; 
        rolling_phase = false;
        protect = false;
        disActive = true;
        p2Leave = false;
        waiting = true;
        timeOver = false;

        timer = 0.0f;
        endTime = 30.0f;

        for (int x = 0; x < scoreRecord.Length; x++) {
            scoreRecord[x] = 0;
            p2scoreRec[x] = null;
        }

        /*
        scoreRecord[0] = 4;
        scoreRecord[1] = 8;
        scoreRecord[2] = 12;
        scoreRecord[3] = 16;
        scoreRecord[4] = 20;
        scoreRecord[7] = 28;
        scoreRecord[8] = 29;
        scoreRecord[9] = 20;
        scoreRecord[10] = 30;
        scoreRecord[11] = 50;
        */
        //테스트용 점수
        rollButton = GameObject.Find("Canvas").transform.Find("RollButton").GetComponent<Button>();
        scoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;
        photonview = PhotonView.Get(this);
        r_count = 3;   
    }
    // Update is called once per frame
    void Update()
    {
        if (waiting && PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            GameObject.Find("Canvas").transform.Find("WT").gameObject.SetActive(true);
            timer += Time.deltaTime;
            GameObject.Find("Canvas").transform.Find("WT").transform.Find("TM").GetComponent<Text>().text = "매칭중... "+ ((int) timer).ToString()+ "s";
            start_game = true;

            if (timer > 5.0f) {
                GameObject.Find("Canvas").transform.Find("WT").transform.Find("Button").gameObject.SetActive(true);
            }
            return;
        }
        

      //  Debug.Log(keep[0] + "/" + keep[1] + "/" + keep[2] + "/" + keep[3] + "/" + keep[4] + "/" );

        if ( myTurn ) { sendPhase(); }

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
       

        if (timeOver) {
            GameObject.Find("Canvas").transform.Find("OT").gameObject.SetActive(true);
            Invoke("gotoLobby", 2f);
            return;
        }

        if (GM.scoreRecord[14] != 0 && p2scoreRec[14] != null)
        {

            GameObject.Find("Canvas").transform.Find("RS").gameObject.SetActive(true);
            if (scoreRecord[14] > p2scoreRec[14])
            {
                GameObject.Find("Canvas").transform.Find("RS").transform.Find("WL").GetComponent<Text>().text = "YOU WIN";

            }
            else if (scoreRecord[14] == p2scoreRec[14])
            {
                GameObject.Find("Canvas").transform.Find("RS").transform.Find("WL").GetComponent<Text>().text = "DRAW";

            }
            else
            {
                GameObject.Find("Canvas").transform.Find("RS").transform.Find("WL").GetComponent<Text>().text = "YOU LOSE";
            }

            GameObject.Find("Canvas").transform.Find("RS").transform.Find("PS").GetComponent<Text>().text = scoreRecord[14].ToString();
            GameObject.Find("Canvas").transform.Find("RS").transform.Find("OS").GetComponent<Text>().text = p2scoreRec[14].ToString();
            Invoke("gotoLobby", 3f);
            return;
        }//게임 종료

        if (p2Leave && PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            GameObject.Find("Canvas").transform.Find("SW").gameObject.SetActive(true);
            Invoke("gotoLobby", 2f);
            return;
        } // 서렌 버튼시 강종

        if (!waiting && PhotonNetwork.CurrentRoom.PlayerCount != 2) {
            GameObject.Find("Canvas").transform.Find("SW").gameObject.SetActive(true);
            Invoke("gotoLobby", 2f);
            return;
        } // 강종시 메뉴로 


        if (start_game) {
            timer = 0.0f;
            waiting = false;
            rollButton.interactable = true;
            spawnDice();
            setTurn();
            Debug.Log(playerIndex);
            round = 1;
            start_game = false;
        } //여기서 호스트가 먼저 생성해야될 것들을 먼저 생성한다.

        timeOut();

        //동기화 되어야 하는 물체가 다 로딩이 되었는지 체크하는 로직 생성이나 동기화가 덜 되어서 게임 로직으로 넘어가는 것을 방지
        if (dice1 != null && dice2 != null && dice3 != null && dice4 != null && dice5 != null)
        {
            GameObject.Find("Canvas").transform.Find("NL").gameObject.SetActive(false);
        }
        else {
            Debug.Log($"{dices[0].name}(Clone)");
            dice1 = GameObject.Find($"{dices[0].name}(Clone)");
            dice2 = GameObject.Find($"{dices[1].name}(Clone)");
            dice3 = GameObject.Find($"{dices[2].name}(Clone)");
            dice4 = GameObject.Find($"{dices[3].name}(Clone)");
            dice5 = GameObject.Find($"{dices[4].name}(Clone)");
            GameObject.Find("Canvas").transform.Find("NL").gameObject.SetActive(true);
            return; 
        }

        GameObject.Find("Canvas").transform.Find("WT").gameObject.SetActive(false);

        if (!myTurn) {
            rollButton.interactable = false;
            scoreBoard.GetComponent<Button>().interactable = true;
        }
        // 마스터 클라이언트라면 마스터 클라이언트에서 계속 턴을 진행 하는 로직을 진행... 문제는 이제 UI나 다른것들에 대한 판정을 RPC로 전달해야됨.
        if (start_phase) { 
            StartPhase();
            timer += Time.deltaTime;

            return;
        }

        if (record_phase) {
            scoreBoard.GetComponent<OpenScoreBoard>().PIn = true;
            timer += Time.deltaTime;
            return;
        }

        if (myTurn && diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            timer += Time.deltaTime;
            rolling_phase = false;
            Score.myCal_sequence();
            if (r_count != 0)
            {
                allKeep();
                GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(true);
                scoreBoard.GetComponent<Button>().interactable = true;
                selec_phase = true;
            }
            else {
                Invoke("recPhaseChange", 0.5f);
                rollButton.interactable = false;
            }
        }
        // mainLogic();
    }

    void timeOut() {
        if (myTurn && timer < 10.0f) {
            GameObject.Find("Canvas").transform.Find("Timer").gameObject.SetActive(false);
        }
        if (myTurn && timer >= 10.0f) {
            GameObject.Find("Canvas").transform.Find("Timer").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Timer").transform.Find("Timer").GetComponent<Text>().text = $"남은시간 \n {30 - (int)timer}";
        }
        if (myTurn && timer > 30.0f) {
            timeOver = true;
        }
    }

    public void gotoLobby() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    void recPhaseChange() {
        timer = 0.0f;
        Score.myCal_sequence();
        record_phase = true;
    }
    void spawnDice() {
        for (int x = 0; x < 5; x++)
        {
            dices[x] = GameObject.Find("DiceManager").GetComponent<dice>().dices[x];  
        }
        // PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.Euler(0, 0, 0));
        if (!PhotonNetwork.IsMasterClient) { return; }
        for (int x = 0; x < 5; x++) {
            PhotonNetwork.Instantiate(dices[x].name, dices[x].GetComponent<DiceScript>().resultPos, Quaternion.Euler(0, 0, 0));
        }
    }
    void setTurn() {

        playerIndex =  PhotonNetwork.LocalPlayer.ActorNumber - 1;
        Debug.Log(playerIndex + "!!!");
        if (playerIndex == 1){ myTurn = false; }
        else{ myTurn = true; }
    }
    void StartPhase() {

        Debug.Log("start phase");

        r_count = 3;
        semiResult = false;
        selec_phase = false;
        record_phase = false;

        diceStop = new bool[5] { false, false, false, false, false};
        keep = new bool[5]{ false, false , false, false, false};
        
        if (PhotonNetwork.IsMasterClient) {
            dice1.GetComponent<Rigidbody>().useGravity = false;
            dice2.GetComponent<Rigidbody>().useGravity = false;
            dice3.GetComponent<Rigidbody>().useGravity = false;
            dice4.GetComponent<Rigidbody>().useGravity = false;
            dice5.GetComponent<Rigidbody>().useGravity = false;
        }

        if (disActive)
        {
            scoreBoard.GetComponent<OpenScoreBoard>().PIn = false;
            disActive = false;
        }
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("SelectUI").GetChild(2).GetComponent<selector>().keep = false;
        GameObject.Find("Canvas").transform.Find("SelectUI").GetChild(3).GetComponent<selector>().keep = false;
        GameObject.Find("Canvas").transform.Find("SelectUI").GetChild(4).GetComponent<selector>().keep = false;
        GameObject.Find("Canvas").transform.Find("SelectUI").GetChild(5).GetComponent<selector>().keep = false;
        GameObject.Find("Canvas").transform.Find("SelectUI").GetChild(6).GetComponent<selector>().keep = false;
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(true);
        
        if (!myTurn) { 
            rollButton.interactable = false;
            scoreBoard.GetComponent<Button>().interactable = true;
        }
        else { 
            rollButton.interactable = true;
            scoreBoard.GetComponent<Button>().interactable = true;
            GameObject.Find("Canvas").transform.Find("RollButton").gameObject.SetActive(true);
        }
        //scoreBoard.GetComponent<Button>().interactable = false;
    }
    void allKeep() {
        if (keep[0] && keep[1] && keep[2] && keep[3] && keep[4]) rollButton.interactable = false;
        else rollButton.interactable = true;
    }

    public void leaveRoom() {
        photonView.RPC("p2LeaveRoom", RpcTarget.All);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
        
    }

    [PunRPC]
    public void p2LeaveRoom()
    {
        p2Leave = true;
    }
    
    [PunRPC]
    public void Rolldice()
    {
        timer = 0.0f;
        selec_phase = false;
        start_phase = false;
        rolling_phase = true;
        
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(false);
        scoreBoard.GetComponent<Button>().interactable = false;

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

            r_count -= 1;
            semiResult = true;
            sendPhase();
        }
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

    public void sendSB(int scoreType, int score) {
        photonView.RPC("syncSBtext", RpcTarget.Others, scoreType, score);
    }

    public void syncResultPhase() {
        photonView.RPC("resultPhase", RpcTarget.All);
    }

    public void syncSound(int diceNo) {
        photonView.RPC("soundPlay", RpcTarget.All, diceNo);
    }

    [PunRPC]
    public void resultPhase() {
        scoreBoard.GetComponent<OpenScoreBoard>().PIn = true;
    }
    [PunRPC]
    public void syncSBtext(int scoreType, int score)
    {
        p2scoreRec[scoreType] = score;
        
    }

    [PunRPC]
    public void syncVelo(bool diceStopS, int x) {
        if (!PhotonNetwork.IsMasterClient) {
            diceStop[x] = diceStopS;
        }
    }
    [PunRPC]
    public void ChangeTurn(string message) {
        timer = 0.0f;
        disActive = true;
        GM.protect = false;
        Debug.Log(message);
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
    public void soundPlay(int diceNo)
    {
        Debug.Log("sound?");
        if (rolling_phase)
        {
            if (diceNo == 0) GameObject.Find("SoundManager").GetComponent<soundManager>().roll1.Play();
            else if (diceNo == 1) GameObject.Find("SoundManager").GetComponent<soundManager>().roll2.Play();
            else if (diceNo == 2) GameObject.Find("SoundManager").GetComponent<soundManager>().roll1.Play();
            else if (diceNo == 3) GameObject.Find("SoundManager").GetComponent<soundManager>().roll2.Play();
            else GameObject.Find("SoundManager").GetComponent<soundManager>().roll1.Play();
        }
    }

    [PunRPC]
    public void syncRecord(int[] recordScore) {
        if (myTurn) { return; }
        for (int x = 0; x < 13; x++) {
            GM.scoreRecord[x] = recordScore[x];
        }
    }
    
    [PunRPC]
    public void syncPoint(int dice1, int dice2 , int dice3, int dice4, int dice5) {

        if (PhotonNetwork.IsMasterClient) { return; }

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