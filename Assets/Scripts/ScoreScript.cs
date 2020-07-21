using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Button btn;
    RectTransform rectTransform;
    Transform transform;
    bool PIn;

    Vector3 outside = new Vector3(-356.3f, 634.8f, 0.0f);
    Vector3 inside = new Vector3(286.2f, 634.8f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(LookPedigree);

        transform = GetComponent<Transform>();
        rectTransform = transform.GetChild(1).GetComponent<RectTransform>();

        PIn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LookPedigree()
    {
        //Debug.Log("Button Clicked");
        //Debug.Log(rectTransform.position);
        if(PIn)
        {
            rectTransform.position = outside;
            PIn = false;
        }
        else
        {
            rectTransform.position = inside;
            PIn = true;
        }
    }
}