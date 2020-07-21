using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{

    Button BT;
    // Start is called before the first frame update
    void Start()
    {
        BT = this.transform.GetComponent<Button>();
        BT.onClick.AddListener(Rolldice);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Rolldice() {
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
    }
}
