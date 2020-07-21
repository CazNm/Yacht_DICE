using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    public static int r_count;
    public static int round;
    public static bool playerTurn;
    public static bool compTurn;

    private static int com_roll;

    private static float timer;
    private static float wating_time;



    //시스템적으로 선공 후공 결정해야됨, 일단은 사람 vs 컴으로 구성 사람이 선공인 상황을 가정
    // Start is called before the first frame update


    void Start()
    {
        timer = 0.0f;

        r_count = 3;
        com_roll = 0;
        round = 1;
        playerTurn = true;
        compTurn = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        
        if (r_count == 0) {
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

    void compRollDice() {

        Rolldice();
        com_roll += 1;

        Debug.Log(com_roll);

        if (com_roll > 2) {
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

            GameObject dice1 = GameObject.Find("dice1");
            GameObject dice2 = GameObject.Find("dice2");
            GameObject dice3 = GameObject.Find("dice3");
            GameObject dice4 = GameObject.Find("dice4");
            GameObject dice5 = GameObject.Find("dice5");


            dice1.GetComponent<DiceScript>().Reroll();
            dice2.GetComponent<DiceScript>().Reroll();
            dice3.GetComponent<DiceScript>().Reroll();
            dice4.GetComponent<DiceScript>().Reroll();
            dice5.GetComponent<DiceScript>().Reroll();

            GM.r_count -= 1;
        }


    }


}
