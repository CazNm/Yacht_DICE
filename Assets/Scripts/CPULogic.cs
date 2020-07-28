using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPULogic : MonoBehaviour
{
    public static int Dnum1 = Score.Dnum1;
    public static int Dnum2 = Score.Dnum2;
    public static int Dnum3 = Score.Dnum3;
    public static int Dnum4 = Score.Dnum4;
    public static int Dnum5 = Score.Dnum5;

    public static int[] check = new int[6];
    public static int[] ischeck = Score.check;
    public static int[] houseChecker = new int[6] { 0, 0, 0, 0, 0, 0 };

    public static int[] keepnumber = new int[6] { 0, 0, 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void cpuCal_sequence()
    {
        int donecheck = 0;
        numberUpdate();
        baseSet();

        if (checkyacht() && ischeck[11] == 0)
        {
            Debug.Log("Choose Yacht");
            GameObject.Find("CYacht").GetComponent<Text>().color = Color.gray;
            ischeck[11] = 1;
            GM.r_count = 0;
            GM.isdone = true;
            //yacht 선택
        }
        else if (checklargestraight() && ischeck[10] == 0)
        {
            Debug.Log("Choose Large Straight");
            GameObject.Find("CLargestraight").GetComponent<Text>().color = Color.gray;
            ischeck[10] = 1;
            GM.r_count = 0;
            GM.isdone = true;
            //largeStraight 선택
        }
        else if (checksmallstraight() && ischeck[9] == 0)
        {
            if (ischeck[10] == 0 && GM.r_count > 0)
            {
                Debug.Log("Lock Small Straight, ReRoll");
                lockSmallstraight();
                GM.cr_rolled = false;
                //smallStraight 고정
                //ReRoll 선택
            }
            else
            {
                Debug.Log("Choose Small Straight");
                GameObject.Find("CSmallstraight").GetComponent<Text>().color = Color.gray;
                ischeck[9] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //smallStraight 선택
            }
        }
        else if (checkfourofkind() && ischeck[8] == 0)
        {
            Debug.Log("Choose Four of Kind");
            GameObject.Find("CFourofkind").GetComponent<Text>().color = Color.gray;
            ischeck[8] = 1;
            GM.r_count = 0;
            GM.isdone = true;
            //fourOfKind 선택
        }
        else if (checkfullhouse() && ischeck[7] == 0)
        {
            Debug.Log("Choose Full House");
            GameObject.Find("CFullhouse").GetComponent<Text>().color = Color.gray;
            ischeck[7] = 1;
            GM.r_count = 0;
            GM.isdone = true;
            //fullHouse 선택
        }
        else if (checksubstraight() && GM.r_count > 0)
        {
            Debug.Log("Lock Possible Small Straight, ReRoll");
            lockStraight();
            GM.cr_rolled = false;
            //smallStraight 가능한 것 고정
            //ReRoll 선택
        }
        else
        {
            if (check[0] >= 1 && ischeck[0] == 0)
            {
                Debug.Log("Choose Single");
                GameObject.Find("CSingle").GetComponent<Text>().color = Color.gray;
                ischeck[0] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //single 선택
            }
            else if (check[1] >= 1 && ischeck[1] == 0)
            {
                Debug.Log("Choose Double");
                GameObject.Find("CDouble").GetComponent<Text>().color = Color.gray;
                ischeck[1] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //double 선택
            }
            else if (check[2] >= 1 && ischeck[2] == 0)
            {
                Debug.Log("Choose Three");
                GameObject.Find("CThree").GetComponent<Text>().color = Color.gray;
                ischeck[2] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //three 선택
            }
            else if (check[3] >= 1 && ischeck[3] == 0)
            {
                Debug.Log("Choose Four");
                GameObject.Find("CFour").GetComponent<Text>().color = Color.gray;
                ischeck[3] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //four 선택
            }
            else if (check[4] >= 1 && ischeck[4] == 0)
            {
                Debug.Log("Choose Fives");
                GameObject.Find("CFives").GetComponent<Text>().color = Color.gray;
                ischeck[4] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //fives 선택
            }
            else if (check[5] >= 1 && ischeck[5] == 0)
            {
                Debug.Log("Choose Sixes");
                GameObject.Find("CSixes").GetComponent<Text>().color = Color.gray;
                ischeck[5] = 1;
                GM.r_count = 0;
                GM.isdone = true;
                //sixes 선택
            }
            else if (GM.r_count > 0)
            {
                Debug.Log("ReRoll");
                //reRoll 선택
                GM.cr_rolled = false;
            }
            else
            {
                if (ischeck[6] == 0)
                {
                    Debug.Log("Choose Chance");
                    GameObject.Find("CChane").GetComponent<Text>().color = Color.gray;
                    ischeck[6] = 1;
                    GM.isdone = true;
                    //chance 선택
                }
                else
                {
                    if (ischeck[11] == 0)
                    {
                        Debug.Log("Choose Yacht 0");
                        GameObject.Find("CYacht").GetComponent<Text>().color = Color.gray;
                        ischeck[11] = 1;
                        GM.isdone = true;
                        //yacht에 0 입력
                    }
                    else if (ischeck[10] == 0)
                    {
                        Debug.Log("Choose LargeStraight 0");
                        GameObject.Find("CLargestraight").GetComponent<Text>().color = Color.gray;
                        ischeck[10] = 1;
                        GM.isdone = true;
                        //largeStraight 0 입력
                    }
                    else if (ischeck[9] == 0)
                    {
                        Debug.Log("Choose SmallStraight 0");
                        GameObject.Find("CSmallstraight").GetComponent<Text>().color = Color.gray;
                        ischeck[9] = 1;
                        GM.isdone = true;
                        //smallStraight 0 입력
                    }
                    else if (ischeck[8] == 0)
                    {
                        Debug.Log("Choose FourofKind 0");
                        GameObject.Find("CFourofkind").GetComponent<Text>().color = Color.gray;
                        ischeck[8] = 1;
                        GM.isdone = true;
                        //fourOfKind 0 입력
                    }
                    else if (ischeck[7] == 0)
                    {
                        Debug.Log("Choose FullHouse 0");
                        GameObject.Find("CFullhouse").GetComponent<Text>().color = Color.gray;
                        ischeck[7] = 1;
                        GM.isdone = true;
                        //fullHouse 0 입력
                    }
                    else if (ischeck[5] == 0)
                    {
                        Debug.Log("Choose Sixes 0");
                        GameObject.Find("CSixes").GetComponent<Text>().color = Color.gray;
                        ischeck[5] = 1;
                        GM.isdone = true;
                        //sixes 0 입력
                    }
                    else if (ischeck[4] == 0)
                    {
                        Debug.Log("Choose Fives 0");
                        GameObject.Find("CFives").GetComponent<Text>().color = Color.gray;
                        ischeck[4] = 1;
                        GM.isdone = true;
                        //fives 0 입력
                    }
                    else if (ischeck[3] == 0)
                    {
                        Debug.Log("Choose Four 0");
                        GameObject.Find("CFour").GetComponent<Text>().color = Color.gray;
                        ischeck[3] = 1;
                        GM.isdone = true;
                        //four 0 입력
                    }
                    else if (ischeck[2] == 0)
                    {
                        Debug.Log("Choose Three 0");
                        GameObject.Find("CThree").GetComponent<Text>().color = Color.gray;
                        ischeck[2] = 1;
                        GM.isdone = true;
                        //three 0 입력
                    }
                    else if (ischeck[1] == 0)
                    {
                        Debug.Log("Choose Double 0");
                        GameObject.Find("CDouble").GetComponent<Text>().color = Color.gray;
                        ischeck[1] = 1;
                        GM.isdone = true;
                        //double 0 입력
                    }
                    else if (ischeck[0] == 0)
                    {
                        Debug.Log("Choose Single 0");
                        GameObject.Find("CSingle").GetComponent<Text>().color = Color.gray;
                        ischeck[0] = 1;
                        GM.isdone = true;
                        //single 0 입력
                    }
                }
            }
        }
        for (int i = 0; i < 12; i++)
        {
            if (ischeck[i] == 1)
            {
                donecheck += 1;
            }
        }
        if (donecheck == 12)
        {
            Debug.Log("DoneChecked");
            GM.myTurn = false;
            GM.p2Turn = false;
            GM.isdone = false;
            //행동 끝
        }
        else
        {
            donecheck = 0;
        }
    }

    static void lockStraight()
    {
        int checks = 0;
        bool checksb = false;
        if (check[0] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 1; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3)
                    {
                        checksb = true;
                        keepnumber = new int[6] { 1, 1, 1, 0, 0, 0 };
                    }
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[1] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 2; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3)
                    {
                        checksb = true;
                        keepnumber = new int[6] { 0, 1, 1, 1, 0, 0 };
                    }
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[2] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 3; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3)
                    {
                        checksb = true;
                        keepnumber = new int[6] { 0, 0, 1, 1, 1, 0 };
                    }
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[3] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 4; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3)
                    {
                        checksb = true;
                        keepnumber = new int[6] { 0, 0, 0, 1, 1, 1 };
                    }
                }
                else
                {
                    checks = 0;
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (keepnumber[i] == 1)
            {
                if (Dnum1 == i + 1 && !GM.keep[0])
                {
                    keepnumber[i] = 0;
                    GM.keep[0] = true;
                }
                else if (Dnum2 == i + 1 && !GM.keep[1])
                {
                    keepnumber[i] = 0;
                    GM.keep[1] = true;
                }
                else if (Dnum3 == i + 1 && !GM.keep[2])
                {
                    keepnumber[i] = 0;
                    GM.keep[2] = true;
                }
                else if (Dnum4 == i + 1 && !GM.keep[3])
                {
                    keepnumber[i] = 0;
                    GM.keep[3] = true;
                }
                else if (Dnum5 == i + 1 && !GM.keep[4])
                {
                    keepnumber[i] = 0;
                    GM.keep[4] = true;
                }
            }
        }
    }

    static void lockSmallstraight()
    {
        int checkss = 0;
        bool checkssb = false;
        if (check[0] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 1; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4)
                    {
                        checkssb = true;
                        keepnumber = new int[6] { 1, 1, 1, 1, 0, 0};
                    }
                }
                else
                {
                    checkss = 0;
                }
            }
        }
        if (check[1] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 2; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4)
                    {
                        checkssb = true;
                        keepnumber = new int[6] { 0, 1, 1, 1, 1, 0 };
                    }
                }
                else
                {
                    checkss = 0;
                }
            }
        }
        if (check[2] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 3; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4)
                    {
                        checkssb = true;
                        keepnumber = new int[6] { 0, 0, 1, 1, 1, 1 };
                    }
                }
                else
                {
                    checkss = 0;
                }
            }
        }

        for(int i=0; i<6; i++)
        {
            if(keepnumber[i] == 1)
            {
                if(Dnum1 == i+1 && !GM.keep[0])
                {
                    keepnumber[i] = 0;
                    GM.keep[0] = true;
                }
                else if (Dnum2 == i+1 && !GM.keep[1])
                {
                    keepnumber[i] = 0;
                    GM.keep[1] = true;
                }
                else if (Dnum3 == i+1 && !GM.keep[2])
                {
                    keepnumber[i] = 0;
                    GM.keep[2] = true;
                }
                else if (Dnum4 == i+1 && !GM.keep[3])
                {
                    keepnumber[i] = 0;
                    GM.keep[3] = true;
                }
                else if (Dnum5 == i+1 && !GM.keep[4])
                {
                    keepnumber[i] = 0;
                    GM.keep[4] = true;
                }
            }
        }
    }

    static void baseSet()
    {
        for (int i = 0; i < 6; i++)
        {
            check[i] = 0;
        }
        check[Dnum1 - 1] += 1;
        check[Dnum2 - 1] += 1;
        check[Dnum3 - 1] += 1;
        check[Dnum4 - 1] += 1;
        check[Dnum5 - 1] += 1;
    }

    static void numberUpdate()
    {
        Dnum1 = DiceNumberTextScript.diceNumbers[0];
        Dnum2 = DiceNumberTextScript.diceNumbers[1];
        Dnum3 = DiceNumberTextScript.diceNumbers[2];
        Dnum4 = DiceNumberTextScript.diceNumbers[3];
        Dnum5 = DiceNumberTextScript.diceNumbers[4];
    }

    static bool checkfullhouse()
    {
        for (int x = 0; x < 6; x++)
        {
            houseChecker[x] = 0;
        }

        int count2 = 0;
        int count3 = 0;

        houseChecker[Dnum1 - 1] += 1;


        for (int x = 0; x < 4; x++)
        {
            if (DiceNumberTextScript.diceNumbers[0] == DiceNumberTextScript.diceNumbers[x + 1])
            {
                houseChecker[DiceNumberTextScript.diceNumbers[0] - 1] += 1;
            }
            else
            {
                houseChecker[DiceNumberTextScript.diceNumbers[x + 1] - 1] += 1;
            }
        }

        for (int x = 0; x < 6; x++)
        {

            if (houseChecker[x] == 2) { count2 += 1; }
            else if (houseChecker[x] == 3) { count3 += 1; }

        }

        if (count2 == 1 && count3 == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool checkfourofkind()
    {
        for (int i = 0; i < 6; i++)
        {
            if (check[i] >= 4)
            {
                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }

    static bool checksubstraight()
    {
        int checks = 0;
        bool checksb = false;
        if (check[0] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 1; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3) checksb = true;
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[1] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 2; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3) checksb = true;
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[2] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 3; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3) checksb = true;
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (check[3] >= 1 && checksb == false)
        {
            checks = 1;
            for (int i = 4; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checks += 1;
                    if (checks == 3) checksb = true;
                }
                else
                {
                    checks = 0;
                }
            }
        }
        if (checksb)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool checksmallstraight()
    {
        int checkss = 0;
        bool checkssb = false;
        if (check[0] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 1; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4)
                    {
                        checkssb = true;
                    }
                }
                else
                {
                    checkss = 0;
                }
            }
        }
        if (check[1] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 2; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4)
                    {
                        checkssb = true;
                    }
                }
                else
                {
                    checkss = 0;
                }
            }
        }
        if (check[2] >= 1 && checkssb == false)
        {
            checkss = 1;
            for (int i = 3; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkss += 1;
                    if (checkss == 4) checkssb = true;
                }
                else
                {
                    checkss = 0;
                }
            }
        }
        if (checkssb)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool checkyacht()
    {
        for (int i = 0; i < 6; i++)
        {
            if (check[i] == 5)
            {
                return true;
            }
            else
            {
                continue ;
            }
        }
        return false;
    }

    static bool checklargestraight()
    {
        int checkls = 0;
        bool checklsb = false;
        if (check[0] >= 1 && checklsb == false)
        {
            checkls = 1;
            for (int i = 1; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkls += 1;
                    if (checkls == 5) checklsb = true;
                }
                else
                {
                    checkls = 0;
                }
            }
        }
        if (check[1] >= 1 && checklsb == false)
        {
            checkls = 1;
            for (int i = 2; i < 6; i++)
            {
                if (check[i] >= 1 && check[i - 1] >= 1)
                {
                    checkls += 1;
                    if (checkls == 5) checklsb = true;
                }
                else
                {
                    checkls = 0;
                }
            }
        }
        if (checklsb)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

/*
얏찌 우선
smallstraight 만들기
smallstaright에서 largestraight 노리기

*/