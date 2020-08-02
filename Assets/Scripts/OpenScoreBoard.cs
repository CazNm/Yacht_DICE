using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenScoreBoard : MonoBehaviour
{
    Button btn;
    RectTransform rectTransform;
    public bool PIn;

    Vector3 outside = new Vector3(-9000, 0, 0);
    //Vector3 inside = new Vector3(286.2f, 634.8f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(LookPedigree);

        GameObject sboard = new GameObject();
        sboard = GameObject.Find("Pedigree");
        rectTransform = sboard.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector3(-9000,0,0);

        PIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PIn)
        {
            rectTransform.anchoredPosition = outside;
        }
        else 
        {
            rectTransform.anchoredPosition = Vector3.zero;
        }
    }

    public void LookPedigree()
    {
        if (PIn)
        {
            PIn = false;
        }
        else
        {
            PIn = true;
        }
    }
}