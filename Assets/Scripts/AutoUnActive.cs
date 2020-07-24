using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUnActive : MonoBehaviour
{

    public float timer;
    public float wait_time;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        wait_time = 0.7f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > wait_time) {
            gameObject.SetActive(false);
        }
    }
}
