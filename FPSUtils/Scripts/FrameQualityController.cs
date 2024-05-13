using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameQualityController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(2560, 1440, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
