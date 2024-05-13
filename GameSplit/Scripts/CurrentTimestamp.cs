using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTimestamp : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        text.text = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
    }
}
