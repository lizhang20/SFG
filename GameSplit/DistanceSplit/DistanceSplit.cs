using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSplit : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Text splitDistanceText;

    private float farClipPlane = 0f;
    // Start is called before the first frame update
    void Start()
    {
        farClipPlane = cam.nearClipPlane;
        cam.farClipPlane = farClipPlane + 0.1f;
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        farClipPlane += 0.2f;
        cam.farClipPlane = farClipPlane;
        splitDistanceText.text = farClipPlane.ToString();
    }
}
