using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public static int r_count;
    public static int round;
    public static bool playerTurn;
    public static bool compTurn;
    public static Vector3[] rotation = { new Vector3(90, 0, 0), new Vector3(0, 90, -90), new Vector3(0, 0, 0), new Vector3(180, 0, 0), new Vector3(0, 0, 90), new Vector3(-90, 0, 0) };
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };
    public static GameObject[] s_ui;
    public static bool selec_phase = false;

    private static int com_roll;

    private static float timer;
    private static float wating_time;
    private static bool selecting = true;

    GameObject dice1;
    GameObject dice2;
    GameObject dice3;
    GameObject dice4;
    GameObject dice5;





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
        wating_time = 2.0f;

        r_count = 3;
        com_roll = 0;
        round = 1;
        playerTurn = true;
        compTurn = false;

    }

    // Update is called once per frame
    void Update()
    {


        for (int x = 0; x < keep.Length; x++)
        {
            Debug.Log((x+1) + " dice" +keep[x]);
        }        

        if (diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            selec_phase = true;
            timer += Time.deltaTime;
            if (timer > wating_time)
            {
                selectPhase();
                GameObject.Find("RollButton").GetComponent<Button>().interactable = true;
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (r_count == 0)
        {
            playerTurn = false;
            compTurn = true;
            r_count = 3;
            comPlay();
        }
        //여기에 족보 기록하는거 넣어주셈 끝나면 상대턴으로 넘어감 자동으로 중간에 족보 넣는건 제외
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
            playerTurn = true;
            r_count = 3;
            compTurn = false;
            GameObject.Find("Canvas").transform.Find("RollButton").gameObject.SetActive(true);
            CancelInvoke("compRollDice");
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
        bool state_check = button.GetComponent<selector>().keep;

        if (!state_check)
        {
            button.GetComponent<selector>().keep = true;
        }
        else {
            button.GetComponent<selector>().keep = false;
        }
        
    }
}