using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderNear : MonoBehaviour
{
    [SerializeField] private float splitDistance = 10f;
    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam.farClipPlane = splitDistance;
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
