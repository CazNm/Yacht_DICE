using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public static int r_count;
    public static int round;
    public static bool myTurn;
    public static bool p2Turn;
    public static Vector3[] rotation = { new Vector3(90, 0, 0), new Vector3(0, 90, -90), new Vector3(0, 0, 0), new Vector3(180, 0, 0), new Vector3(0, 0, 90), new Vector3(-90, 0, 0) };
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };
    public static GameObject[] s_ui;

    public static bool start_phase = true;
    public static bool semiResult = false;
    public static bool selec_phase = false;
    public static bool record_phase = false;


    private static int com_roll;

    private static float timer;
    private static float wating_time;
    private static bool selecting = true;

    GameObject dice1;
    GameObject dice2;
    GameObject dice3;
    GameObject dice4;
    GameObject dice5;

    public static GameObject scoreBoard;
    Button rollButton;
    

    //시스템적으로 선공 후공 결정해야됨, 일단은 사람 vs 컴으로 구성 사람이 선공인 상황을 가정
    // Start is called before the first frame update

    void Start()
    {

        dice1 = GameObject.Find("dice1");
        dice2 = GameObject.Find("dice2");
        dice3 = GameObject.Find("dice3");
        dice4 = GameObject.Find("dice4");
        dice5 = GameObject.Find("dice5");

        timer = 0.0f;
        wating_time = 1f;

        r_count = 3;
        com_roll = 0;
        round = 1;
        myTurn = true;
        p2Turn = false;

        rollButton = GameObject.Find("Canvas").transform.Find("RollButton").GetComponent<Button>();
        scoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (start_phase) { StartPhase(); } //시작페이즈 진입 코드

        if (diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            selec_phase = true;
            timer += Time.deltaTime;
            allKeep();
            if (timer > wating_time)
            {
                if (semiResult) {

                    Score.myCal_sequence();
                    Debug.Log("count checker");
                    semiResult = false;
                }

                if (r_count > 0) {
                    selectPhase();
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

        if (record_phase) { 
        
        }//기록 페이즈 여기서 족보 기록 종료 후 상대 시작 페이즈 진입
    }

    private void FixedUpdate()
    {
        //여기 항목 일단 삭제함 페이즈 별로 나눴는데 위에 주석 좀 더 자세하게 보면 좋을듯
    }

    void StartPhase() {

        dice1.GetComponent<Rigidbody>().useGravity = false;
        dice2.GetComponent<Rigidbody>().useGravity = false;
        dice3.GetComponent<Rigidbody>().useGravity = false;
        dice4.GetComponent<Rigidbody>().useGravity = false;
        dice5.GetComponent<Rigidbody>().useGravity = false;

        activeStartUI();
        
    }

    void activeStartUI() {
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(true);
        scoreBoard.GetComponent<Button>().interactable = false;
    }

    void RecordPhase() { 
    
    }

    void comPlay()
    {
        InvokeRepeating("compRollDice", 1f, 4f);
    }

    void compRollDice()
    {

        Rolldice();
        com_roll += 1;
        Debug.Log(com_roll);

        if (com_roll > 0)
        {
            com_roll = 0;
            myTurn = true;
            r_count = 3;
            p2Turn = false;
            GameObject.Find("Canvas").transform.Find("RollButton").gameObject.SetActive(true);
            CancelInvoke("compRollDice");
        }
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
        }
        else {
            Debug.Log("Ready to roll");
            button.GetComponent<selector>().keep = false;
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Button>().interactable = true;
        }
        
    }
}