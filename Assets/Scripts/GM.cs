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
    public GameObject player;

    public static int r_count;
    public static int round;
    public static bool myTurn;
    public static bool p2Turn;
    public static Vector3[] rotation = { new Vector3(90, 0, 0), new Vector3(0, 90, -90), new Vector3(0, 0, 0), new Vector3(180, 0, 0), new Vector3(0, 0, 90), new Vector3(-90, 0, 0) };
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };
    public static GameObject[] s_ui;

    public static bool start_game;
    public static bool start_phase = true;
    public static bool semiResult = false;
    public static bool selec_phase = false;
    public static bool record_phase = false;


    private static int com_roll;

    private static float timer;
    private static float wating_time;
 
    GameObject dice1;
    GameObject dice2;
    GameObject dice3;
    GameObject dice4;
    GameObject dice5;

    public static GameObject scoreBoard;
    public static GameObject playerOb;
    public static GameObject otherOb;
    Button rollButton;
    

    //시스템적으로 선공 후공 결정해야됨, 일단은 사람 vs 컴으로 구성 사람이 선공인 상황을 가정
    // Start is called before the first frame update

    void Start()
    {
        start_game = true;
        timer = 0.0f;
        wating_time = 0.4f;
        rollButton = GameObject.Find("Canvas").transform.Find("RollButton").GetComponent<Button>();
        scoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        
        /*if (PhotonNetwork.CurrentRoom.PlayerCount != 2) {
            GameObject.Find("Canvas").transform.Find("WTimg").gameObject.SetActive(true);
            start_game = true;
            return; 
        }*/

        if (start_game) {
            spawnDice();
            setTurn();
            Debug.Log(playerIndex);
            round = 1;
            r_count = 3;
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
            

        } //내 턴이 아니라면 유저 로직으로 가는게 아닌 관찰/ 비활성화 로직 작성 예정
        userLogic();
        
    }


    void spawnDice() {
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.Euler(0, 0, 0));
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
            p2Turn = true;
            otherIndex = 0;
        }
        else
        {
            myTurn = true;
            p2Turn = false;
            otherIndex = 1;
        }
    }

    void userLogic() {
        if (start_phase) { StartPhase(); } //시작페이즈 진입 코드
        Debug.Log("checkr 1");
        if (diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            Debug.Log("checkr 2");
            selec_phase = true;
            sendPhase();

            timer += Time.deltaTime;
            allKeep();

            if (timer > wating_time)
            {
                if (semiResult)
                {
                    Score.myCal_sequence();
                    Debug.Log("count checker");
                  
                    semiResult = false;
                    sendPhase();
                }

                if (r_count > 0)
                {
                    selectPhase();
                    GM.scoreBoard.GetComponent<Button>().interactable = true;
                    timer = 0;
                }
                else //기록 페이즈 진입
                {
                    for (int x = 0; x < 5; x++) { diceStop[x] = false; keep[x] = false; }

                    selec_phase = false;
                    record_phase = true;
                    scoreBoard.GetComponent<Button>().interactable = false;
                    scoreBoard.GetComponent<OpenScoreBoard>().PIn = false;
                    scoreBoard.GetComponent<OpenScoreBoard>().LookPedigree();
                }

            }
        }


        // 주사위 굴리기가 끝나고 선택페이즈로 진입 선택 페이즈에서 
        //족보 기록은 언제든지 가능 족보 기록 후 상대 턴 시작페이즈 진입
    }
    void StartPhase() {

        r_count = 3;
        semiResult = false;
        selec_phase = false;
        record_phase = false;

        diceStop = new bool[5] { false, false, false, false, false};

        dice1.GetComponent<Rigidbody>().useGravity = false;
        dice2.GetComponent<Rigidbody>().useGravity = false;
        dice3.GetComponent<Rigidbody>().useGravity = false;
        dice4.GetComponent<Rigidbody>().useGravity = false;
        dice5.GetComponent<Rigidbody>().useGravity = false;

        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);

        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(true);
        scoreBoard.GetComponent<Button>().interactable = false;

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

    public void Rolldice()
    {

        if (GM.r_count > 0)
        {
            if (!keep[0])
            {
                dice1.GetComponent<DiceScript>().Reroll();
                diceStop[0] = false;
                dice1.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[1])
            {
                dice2.GetComponent<DiceScript>().Reroll();
                diceStop[1] = false;
                dice2.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[2])
            {
                dice3.GetComponent<DiceScript>().Reroll();
                diceStop[2] = false;
                dice3.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[3])
            {
                dice4.GetComponent<DiceScript>().Reroll();
                diceStop[3] = false;
                dice4.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[4])
            {
                dice5.GetComponent<DiceScript>().Reroll();
                diceStop[4] = false;
                dice5.GetComponent<Rigidbody>().useGravity = true;
            }

            semiResult = true;
            sendPhase();
            GM.r_count -= 1;
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
        Debug.Log("select logic");
        bool state_check = button.GetComponent<selector>().keep;
        if (!state_check)
        {
            Debug.Log("keep!");
            button.GetComponent<selector>().keep = true;
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Button>().interactable = true;
            sendPhase();
        }
        else {
            Debug.Log("Ready to roll");
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
        photonview.RPC("syncPhase", RpcTarget.All, start_phase, semiResult, selec_phase, record_phase, keep);
    }

    [PunRPC]
    public void ChangeTurn(string message) {
        Debug.Log(message);

        if (myTurn) { myTurn = false; }
        else  { myTurn = true; }
    }

    [PunRPC]
    public void syncPoint(string message) {
        Debug.Log(message);
    }

    [PunRPC]
    public void syncPhase(bool startSync, bool semiSync, bool selecSync, bool recordSync, bool [] keepSync) {
        Debug.Log("sync turn state");

        if (myTurn) {
            return;
        }

        start_phase = startSync;
        semiResult = semiSync;
        selec_phase = selecSync;
        record_phase = recordSync;

        for (int x = 0; x < keep.Length; x++) {
            keep[x] = keepSync[x];
        }
    }
}