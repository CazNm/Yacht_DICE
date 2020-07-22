﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameObject sboard;
    public int Dnum1;
    public int Dnum2;
    public int Dnum3;
    public int Dnum4;
    public int Dnum5;
    public int[] check = new int[12];
    int singlesum;
    int doublesum;
    int threesum;
    int foursum;
    int fivessum;
    int sixessum;
    int sumcheck = 0;
    int sum;
    int bonus;
    int chancesum;
    int fullhouse;
    int fourofkind;
    int smallstraight;
    int largestraight;
    int[] checkyacht = new int[6];
    int yacht;
    int totalcheck = 0;
    int total;

    // Start is called before the first frame update
    void Start()
    {
        sboard = GameObject.Find("Pedigree");
        int youtextcount = sboard.transform.GetChild(1).childCount;
        int comtextcount = sboard.transform.GetChild(2).childCount;
        for (int i = 0; i < youtextcount; i++)
        {
            sboard.transform.GetChild(1).GetChild(i).GetComponent<Text>().text = "";
        }
        for (int i = 0; i < comtextcount; i++)
        {
            sboard.transform.GetChild(2).GetChild(i).GetComponent<Text>().text = "";
        }
        for (int i = 0; i < 12; i++)
        {
            check[i] = 0;
        }

        Dnum1 = 1;
        Dnum2 = 2;
        Dnum3 = 3;
        Dnum4 = 4;
        Dnum5 = 5;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        for (int i = 0; i < 6; i++)
        {
            checkyacht[i] = 0;
        }
        checkyacht[Dnum1 - 1] += 1;
        checkyacht[Dnum2 - 1] += 1;
        checkyacht[Dnum3 - 1] += 1;
        checkyacht[Dnum4 - 1] += 1;
        checkyacht[Dnum5 - 1] += 1;

        if ((Dnum1 == 1 || Dnum2 == 1 || Dnum3 == 1 || Dnum4 == 1 || Dnum5 == 1) && check[0] == 0)
        {
            singlesum=0;
            if (Dnum1 == 1) singlesum += Dnum1;
            if (Dnum2 == 1) singlesum += Dnum2; 
            if (Dnum3 == 1) singlesum += Dnum3; 
            if (Dnum4 == 1) singlesum += Dnum4; 
            if (Dnum5 == 1) singlesum += Dnum5;
            sboard.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = singlesum.ToString();
        }
        else if((Dnum1 != 1 && Dnum2 != 1 && Dnum3 != 1 && Dnum4 != 1 && Dnum5 != 1) && check[0] == 0)
        {
            singlesum = 0;
            sboard.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        }
        if ((Dnum1 == 2 || Dnum2 == 2 || Dnum3 == 2 || Dnum4 == 2 || Dnum5 == 2) && check[1] == 0)
        {
            doublesum = 0;
            if (Dnum1 == 2) doublesum += Dnum1;
            if (Dnum2 == 2) doublesum += Dnum2;
            if (Dnum3 == 2) doublesum += Dnum3;
            if (Dnum4 == 2) doublesum += Dnum4;
            if (Dnum5 == 2) doublesum += Dnum5;
            sboard.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = doublesum.ToString();
        }
        else if ((Dnum1 != 2 && Dnum2 != 2 && Dnum3 != 2 && Dnum4 != 2 && Dnum5 != 2) && check[1] == 0)
        {
            doublesum = 0;
            sboard.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "";
        }
        if ((Dnum1 == 3 || Dnum2 == 3 || Dnum3 == 3 || Dnum4 == 3 || Dnum5 == 3) && check[2] == 0)
        {
            threesum = 0;
            if (Dnum1 == 3) threesum += Dnum1;
            if (Dnum2 == 3) threesum += Dnum2;
            if (Dnum3 == 3) threesum += Dnum3;
            if (Dnum4 == 3) threesum += Dnum4;
            if (Dnum5 == 3) threesum += Dnum5;
            sboard.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = threesum.ToString();
        }
        else if ((Dnum1 != 3 && Dnum2 != 3 && Dnum3 != 3 && Dnum4 != 3 && Dnum5 != 3) && check[2] == 0)
        {
            threesum = 0;
            sboard.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "";
        }
        if ((Dnum1 == 4 || Dnum2 == 4 || Dnum3 == 4 || Dnum4 == 4 || Dnum5 == 4) && check[3] == 0)
        {
            foursum = 0;
            if (Dnum1 == 4) foursum += Dnum1;
            if (Dnum2 == 4) foursum += Dnum2;
            if (Dnum3 == 4) foursum += Dnum3;
            if (Dnum4 == 4) foursum += Dnum4;
            if (Dnum5 == 4) foursum += Dnum5;
            sboard.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = foursum.ToString();
        }
        else if ((Dnum1 != 4 && Dnum2 != 4 && Dnum3 != 4 && Dnum4 != 4 && Dnum5 != 4) && check[3] == 0)
        {
            foursum = 0;
            sboard.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "";
        }
        if ((Dnum1 == 5 || Dnum2 == 5 || Dnum3 == 5 || Dnum4 == 5 || Dnum5 == 5) && check[4] == 0)
        {
            fivessum = 0;
            if (Dnum1 == 5) fivessum += Dnum1;
            if (Dnum2 == 5) fivessum += Dnum2;
            if (Dnum3 == 5) fivessum += Dnum3;
            if (Dnum4 == 5) fivessum += Dnum4;
            if (Dnum5 == 5) fivessum += Dnum5;
            sboard.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = fivessum.ToString();
        }
        else if ((Dnum1 != 5 && Dnum2 != 5 && Dnum3 != 5 && Dnum4 != 5 && Dnum5 != 5) && check[4] == 0)
        {
            fivessum = 0;
            sboard.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = "";
        }
        if ((Dnum1 == 6 || Dnum2 == 6 || Dnum3 == 6 || Dnum4 == 6 || Dnum5 == 6) && check[5] == 0)
        {
            sixessum = 0;
            if (Dnum1 == 6) sixessum += Dnum1;
            if (Dnum2 == 6) sixessum += Dnum2;
            if (Dnum3 == 6) sixessum += Dnum3;
            if (Dnum4 == 6) sixessum += Dnum4;
            if (Dnum5 == 6) sixessum += Dnum5;
            sboard.transform.GetChild(1).GetChild(5).GetComponent<Text>().text = sixessum.ToString();
        }
        else if ((Dnum1 != 6 && Dnum2 != 6 && Dnum3 != 6 && Dnum4 != 6 && Dnum5 != 6) && check[5] == 0)
        {
            sixessum = 0;
            sboard.transform.GetChild(1).GetChild(5).GetComponent<Text>().text = "";
        }
        for(int i=0; i<6; i++)
        {
            if (check[i] == 1) sumcheck += 1;
        }
        if(sumcheck == 6)
        {
            sum = singlesum + doublesum + threesum + foursum + fivessum + sixessum;
            sboard.transform.GetChild(1).GetChild(6).GetComponent<Text>().text = sum.ToString();
            if (sum >= 63)
            {
                bonus = 35;
                sboard.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = bonus.ToString();
            }
            else
            {
                bonus = 0;
                sboard.transform.GetChild(1).GetChild(7).GetComponent<Text>().text = bonus.ToString();
            }
        }
        else
        {
            sumcheck = 0;
        }
        if(check[6] == 0)
        {
            chancesum = Dnum1 + Dnum2 + Dnum3 + Dnum4 + Dnum5;
            sboard.transform.GetChild(1).GetChild(8).GetComponent<Text>().text = chancesum.ToString();
        }
        if (check[7] == 0)
        {
            int checkfh = 0;
            for (int i = 0; i < 6; i++)
            {
                if (checkyacht[i] == 2)
                {
                    checkfh += 1;
                }
                if (checkyacht[i] == 3)
                {
                    checkfh += 1;
                }
                if (checkfh == 2)
                {
                    fullhouse = Dnum1 + Dnum2 + Dnum3 + Dnum4 + Dnum5;
                    sboard.transform.GetChild(1).GetChild(9).GetComponent<Text>().text = fullhouse.ToString();
                    break;
                }
                else
                {
                    fullhouse = 0;
                    sboard.transform.GetChild(1).GetChild(9).GetComponent<Text>().text = "";
                }
            }
        }
        if (check[8] == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                if (checkyacht[i] == 4)
                {
                    fourofkind = Dnum1 + Dnum2 + Dnum3 + Dnum4 + Dnum5;
                    sboard.transform.GetChild(1).GetChild(10).GetComponent<Text>().text = fourofkind.ToString();
                    break;
                }
                else
                {
                    fourofkind = 0;
                    sboard.transform.GetChild(1).GetChild(10).GetComponent<Text>().text = "";
                    break;
                }
            }
        }
        if (check[9] == 0)
        {
            int checkss = 0;
            bool checkssb = false;
            if(checkyacht[0] >= 1 && checkssb == false)
            {
                checkss = 1;
                for(int i=1; i<6; i++)
                {
                    if(checkyacht[i]>=1 && checkyacht[i - 1] >= 1)
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
            if (checkyacht[1] >= 1 && checkssb == false)
            {
                checkss = 1;
                for (int i = 2; i < 6; i++)
                {
                    if (checkyacht[i] >= 1 && checkyacht[i - 1] >= 1)
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
            if (checkyacht[2] >= 1 && checkssb == false)
            {
                checkss = 1;
                for (int i = 3; i < 6; i++)
                {
                    if (checkyacht[i] >= 1 && checkyacht[i - 1] >= 1)
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
                smallstraight = 20;
                sboard.transform.GetChild(1).GetChild(11).GetComponent<Text>().text = smallstraight.ToString();
            }
            else
            {
                smallstraight = 0;
                sboard.transform.GetChild(1).GetChild(11).GetComponent<Text>().text = "";
            }
        }
        if (check[10] == 0)
        {
            int checkls = 0;
            bool checklsb = false;
            if (checkyacht[0] >= 1 && checklsb == false)
            {
                checkls = 1;
                for (int i = 1; i < 6; i++)
                {
                    if (checkyacht[i] >= 1 && checkyacht[i - 1] >= 1)
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
            if (checkyacht[1] >= 1 && checklsb == false)
            {
                checkls = 1;
                for (int i = 2; i < 6; i++)
                {
                    if (checkyacht[i] >= 1 && checkyacht[i - 1] >= 1)
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
                largestraight = 30;
                sboard.transform.GetChild(1).GetChild(12).GetComponent<Text>().text = largestraight.ToString();
            }
            else
            {
                largestraight = 0;
                sboard.transform.GetChild(1).GetChild(12).GetComponent<Text>().text = "";
            }
        }
        if (check[11] == 0)
        {
            for (int i=0; i<6; i++)
            {
                if(checkyacht[i] == 5)
                {
                    yacht = 50;
                    sboard.transform.GetChild(1).GetChild(13).GetComponent<Text>().text = yacht.ToString();
                    break;
                }
                else
                {
                    sboard.transform.GetChild(1).GetChild(13).GetComponent<Text>().text = "";
                    break;
                }
            }
        }
        for (int i = 0; i < 12; i++)
        {
            if (check[i] == 1)
            {
                totalcheck += 1;
            }
        }
        if(totalcheck == 12)
        {
            total = sum + bonus + chancesum + fullhouse + fourofkind + smallstraight + largestraight + yacht;
            sboard.transform.GetChild(1).GetChild(14).GetComponent<Text>().text = chancesum.ToString();
        }
        else
        {
            totalcheck = 0;
        }

        /*
        Check Number / Child Number
        0 / 0 = single - 1의 눈의 합
        1 / 1 = double - 2의 눈의 합
        2 / 2 = three - 3의 눈의 합
        3 / 3 = four - 4의 눈의 합
        4 / 4 = fives - 5의 눈의 합
        5 / 5 = sixes - 6의 눈의 합
        - / 6 = sum - sigle 부터 sixes까지의 합
        - / 7 = bonus - sigle 부터 sixes까지의 합이 63점 이상이면, 보너스 점수 +35점을 얻는다.
        6 / 8 = chance - 모든 주사위 눈의 합
        7 / 9 = full house - 주사위 눈이 2개와 3개가 동일할 때의 모든 눈의 합
        8 / 10 = four of kind - 주사위 눈이 4개가 같을 때의 모든 눈의 합
        9 / 11 = small straight - 주사위 눈이 4개가 연속 일 때 20점을 얻는다
        10 / 12 = large straight - 주사위 눈이 5개가 연속 일 때 30점을 얻는다
        11 / 13 = Yacht! - 모든 주사위 눈이 같을 때 50점을 얻는다.
        - / 14 = Total = 총점
        */
=======
        //int num0 = DNum.diceNumbers[0];
       // Debug.Log(num0);
>>>>>>> ef6c9564c264e986ac9a3b5b2ed31f6d94cbfb65
    }
}