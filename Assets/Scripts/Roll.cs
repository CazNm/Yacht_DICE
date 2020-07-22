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
        BT = this.transform.GetComponent<Button>();
        BT.onClick.AddListener(Rolling);

        buttonText = GetComponentInChildren<Text>();

        buttonText.text = "Roll! (" + GM.r_count + ")";

    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = "Roll! (" + GM.r_count + ")";

        if (!GM.playerTurn)
        {
            Debug.Log("false");
            BT.interactable = false;
        }
    }

    void Rolling() {
        GM.diceStop[0] = false;
        GM.diceStop[1] = false;
        GM.diceStop[2] = false;
        GM.diceStop[3] = false;
        GM.diceStop[4] = false;


        GameObject.Find("GameManager").GetComponent<GM>().Rolldice();
        BT.interactable = false;
    }
   
}
