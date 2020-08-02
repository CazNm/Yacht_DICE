using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p2scoreInput : MonoBehaviour
{
    public int scoreType;
    public int? score;
    Text text;
    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        
        text = GetComponent<Text>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        score = GM.p2scoreRec[scoreType];
        //text.text = score.ToString();
        if (check && score != null)
        {
            StartCoroutine(Showready());
            check = false;
        }


    }

    IEnumerator Showready() {
        int count = 0;
        while (count < 3) {
            text.text = "";
            yield return new WaitForSeconds(0.5f);
            text.text = score.ToString();
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }
    

    void spaceText() 
    {
        Debug.Log("goto Blank");

        text.text = "";
        
    }

    void realText() {
        Debug.Log("to text");
        text.text = score.ToString();
        
    }

    void checkState() {
        check = false;
    }
}