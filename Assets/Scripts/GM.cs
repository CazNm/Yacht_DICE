using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    public static int r_count;
    public static int round;
    public static bool playerTurn;
    public static bool compTurn;
    public static Vector3[] rotation = { new Vector3(40, 0, 0), new Vector3(0, -89, -39), new Vector3(0, -90, 51), new Vector3(0, -90, -127), new Vector3(0, -90, 141), new Vector3(220, 0, 0) };
    public static bool[] diceStop = { false, false, false, false, false };
    public static bool[] keep = { false, false, false, false, false };

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
        if (diceStop[0] && diceStop[1] && diceStop[2] && diceStop[3] && diceStop[4])
        {
            timer += Time.deltaTime;
            if (timer > wating_time) { 
                selectPhase();
                timer = 0;
            }
            
        }

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

        if (com_roll > 0) {
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
            if (!keep[0]) { 
                dice1.GetComponent<DiceScript>().Reroll();
                dice1.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[1]) { 
                dice2.GetComponent<DiceScript>().Reroll();
                dice2.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[2]) { 
                dice3.GetComponent<DiceScript>().Reroll();
                dice3.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[3]) { 
                dice4.GetComponent<DiceScript>().Reroll();
                dice4.GetComponent<Rigidbody>().useGravity = true;
            }
            if (!keep[4]) { 
                dice5.GetComponent<DiceScript>().Reroll();
                dice5.GetComponent<Rigidbody>().useGravity = true;
            } 

            GM.r_count -= 1;
        }
    }

    void selectPhase() {

        dice1.transform.position = dice1.GetComponent<DiceScript>().resultPos;
        dice2.transform.position = dice2.GetComponent<DiceScript>().resultPos;
        dice3.transform.position = dice3.GetComponent<DiceScript>().resultPos;
        dice4.transform.position = dice4.GetComponent<DiceScript>().resultPos;
        dice5.transform.position = dice5.GetComponent<DiceScript>().resultPos;

        dice1.GetComponent<Rigidbody>().useGravity = false;
        dice2.GetComponent<Rigidbody>().useGravity = false;
        dice3.GetComponent<Rigidbody>().useGravity = false;
        dice4.GetComponent<Rigidbody>().useGravity = false;
        dice5.GetComponent<Rigidbody>().useGravity = false;


        Debug.Log("selectPhase");
        
    }

}
