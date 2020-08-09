using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPULogic : MonoBehaviour
{
    public int Dnum1;
    public int Dnum2;
    public int Dnum3;
    public int Dnum4;
    public int Dnum5;

    public int count;

    int[] check = new int[6];
    public int[] ischeck = new int[12];

    // Start is called before the first frame update
    void Start()
    {
        Dnum1 = 1;
        Dnum2 = 2;
        Dnum3 = 3;
        Dnum4 = 4;
        Dnum5 = 5;

        for (int i = 0; i < 12; i++)
        {
            ischeck[i] = 0;
        }

        count = 1;
    }

    // Update is called once per frame
    void Update()
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

        if (checkyacht() && ischeck[11] == 0)
        {
            Debug.Log("Choose Yacht");
            //yacht 선택
        }
        else if (checklargestraight() && ischeck[10] == 0)
        {
            Debug.Log("Choose Large Straight");
            //largeStraight 선택
        }
        else if (checksmallstraight() && ischeck[9] == 0)
        {
            Debug.Log("Choose Small Straight");
            //smallStraight 선택
        }
        else if (checkfourofkind() && ischeck[8] == 0)
        {
            Debug.Log("Choose Four of Kind");
            //fourOfKind 선택
        }
        else if (checkfullhouse() && ischeck[7] == 0) //이론 없어서 넘어감
        {
            Debug.Log("Choose Full House");
            //fullHouse 선택
        }
        else if (checksubstraight())
        {
            Debug.Log("Lock Possible Small Straight, ReRoll");
            //smallStraight 가능한 것 고정 및 ReRoll 선택
        }
        else
        {
            if (check[0] >=1 && ischeck[0] == 0)
            {
                Debug.Log("Choose Single");
                //single 선택
            }
            else if (check[1] >=1 && ischeck[1] == 0)
            {
                Debug.Log("Choose Double");
                //double 선택
            }
            else if (check[2] >= 1 && ischeck[2] == 0)
            {
                Debug.Log("Choose Three");
                //three 선택
            }
            else if (check[3] >= 1 && ischeck[3] == 0)
            {
                Debug.Log("Choose Four");
                //four 선택
            }
            else if (check[4] >= 1 && ischeck[4] == 0)
            {
                Debug.Log("Choose Fives");
                //fives 선택
            }
            else if (check[5] >= 1 && ischeck[5] == 0)
            {
                Debug.Log("Choose Sixes");
                //sixes 선택
            }
            else if(count > 0)
            {
                Debug.Log("ReRoll");
                //reRoll 선택
            }
            else
            {
                if(ischeck[6] == 0)
                {
                    Debug.Log("Choose Chance");
                    //chance 선택
                }
                else
                {
                    if(ischeck[11] == 0)
                    {
                        Debug.Log("Choose Yacht 0");
                        //yacht에 0 입력
                    }
                    else if (ischeck[10] == 0)
                    {
                        Debug.Log("Choose LargeStraight 0");
                        //largeStraight 0 입력
                    }
                    else if (ischeck[9] == 0)
                    {
                        Debug.Log("Choose SmallStraight 0");
                        //smallStraight 0 입력
                    }
                    else if (ischeck[8] == 0)
                    {
                        Debug.Log("Choose FourofKind 0");
                        //fourOfKind 0 입력
                    }
                    else if (ischeck[7] == 0)
                    {
                        Debug.Log("Choose FullHouse 0");
                        //fullHouse 0 입력
                    }
                    else if (ischeck[5] == 0)
                    {
                        Debug.Log("Choose Sixes 0");
                        //sixes 0 입력
                    }
                    else if (ischeck[4] == 0)
                    {
                        Debug.Log("Choose Fives 0");
                        //fives 0 입력
                    }
                    else if (ischeck[3] == 0)
                    {
                        Debug.Log("Choose Four 0");
                        //four 0 입력
                    }
                    else if (ischeck[2] == 0)
                    {
                        Debug.Log("Choose Three 0");
                        //three 0 입력
                    }
                    else if (ischeck[1] == 0)
                    {
                        Debug.Log("Choose Double 0");
                        //double 0 입력
                    }
                    else if (ischeck[0] == 0)
                    {
                        Debug.Log("Choose Single 0");
                        //single 0 입력
                    }
                }
            }
        }
    }

    bool checkfullhouse()
    {
        return false;
    }

    bool checkfourofkind()
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

    bool checksubstraight()
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

    bool checksmallstraight()
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
                    if (checkss == 4) checkssb = true;
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
                    if (checkss == 4) checkssb = true;
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

    bool checkyacht()
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

    bool checklargestraight()
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