using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFar : MonoBehaviour
{
    [SerializeField] private float splitdistance = 10f;
    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam.nearClipPlane = splitdistance;
        cam.clearFlags = CameraClearFlags.Skybox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
