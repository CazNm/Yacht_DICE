using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public string usrID;
    public string email;
    public string pw;
    public bool myTurn;
    public int [] scores = new int[9];
    public bool[] fixedScore = new bool[3]; //index 0 smallS , 1 largeS, 2 yacht

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
