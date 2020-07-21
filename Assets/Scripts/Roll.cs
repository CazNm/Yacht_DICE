using System.Collections;
using System.Collections.Generic;
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
            gameObject.SetActive(false);
        }
        else {
            Debug.Log("true");
            gameObject.SetActive(true);
        }
    }

    void Rolling() {
        GameObject.Find("GameManager").GetComponent<GM>().Rolldice();
    }
   
}
