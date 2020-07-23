using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{

    Button BT;
    Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
       

        buttonText = GetComponentInChildren<Text>();
        buttonText.text = "Roll! (" + GM.r_count + ")";

    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = "Roll! (" + GM.r_count + ")";

        /*if (!GM.myTurn)
        {
            //Debug.Log("false");
            GetComponent<Button>().interactable = false;
        }*/
    }   
    
    public void Rolling() {
        
        GM.selec_phase = false;
        GM.start_phase = false;
        

        //Debug.Log("rolling sequence");


        GameObject.Find("GameManager").GetComponent<GM>().Rolldice();
        GameObject.Find("Canvas").transform.Find("SelectUI").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("StartUI").gameObject.SetActive(false);
        GetComponent<Button>().interactable = false;
    }
}
