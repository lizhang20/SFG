using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://discussions.unity.com/t/urp-cameras-create-a-custom-projection-matrix-given-a-bounding-rectangle/232604
public class Scissor : MonoBehaviour
{
    [SerializeField] private Camera mainCam, leftCam, rightCam;
    [SerializeField] private float leftPortion = 0.4f;

    // Rect(x-axis start, y-axis start, x-axis length, y-axis length)
    private Rect leftScissorRect = new Rect(0, 0, 1, 1);
    private Rect rightScissorRect = new Rect(0, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        // one-shot setup
        leftScissorRect.Set(0, 0, leftPortion, 1);
        rightScissorRect.Set(leftPortion, 0, 1 - leftPortion, 1);

        if (leftCam != null)
            SetScissorRect(mainCam, leftCam, leftScissorRect);
        if (rightCam != null)
            SetScissorRect(mainCam, rightCam, rightScissorRect);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: monitor leftScissorRect/rightScissorRect change and update cam 
    }

    // void LateUpdate()
    // {
    //     leftCam.transform.position = mainCam.transform.position;
    //     rightCam.transform.position = mainCam.transform.position;
    //     leftCam.transform.rotation = mainCam.transform.rotation;
    //     rightCam.transform.rotation = mainCam.transform.rotation;
    // }

    // the real util function
    public static void SetScissorRect(Camera origin, Camera target, Rect r)
    {
        // boundary checking: make sure the rectangle stays within the screen
        if (r.x < 0)
        {
            r.width += r.x;
            r.x = 0;
        }
        if (r.y < 0)
        {
            r.height += r.y;
            r.y = 0;
        }

        // boundary checking: make sure the rectangle bottom-right corner 
        // does not go off the screen, as r.width/r.height may be larger than 1
        r.width = Mathf.Min(1 - r.x, r.width);
        r.height = Mathf.Min(1 - r.y, r.height);
        target.rect = r;

        origin.rect = new Rect(0, 0, 1, 1);
        origin.ResetProjectionMatrix();
        Matrix4x4 m = origin.projectionMatrix;
        Matrix4x4 m1 = Matrix4x4.TRS(new Vector3(r.x, r.y, 0), Quaternion.identity, new Vector3(r.width, r.height, 1));
        Matrix4x4 m2 = Matrix4x4.TRS(new Vector3((1 / r.width - 1), (1 / r.height - 1), 0), Quaternion.identity, new Vector3(1 / r.width, 1 / r.height, 1));
        Matrix4x4 m3 = Matrix4x4.TRS(new Vector3(-r.x * 2 / r.width, -r.y * 2 / r.height, 0), Quaternion.identity, Vector3.one);
        target.projectionMatrix = m3 * m2 * m;
        // origin.projectionMatrix = m3 * m2 * m;
        // origin.rect = r;
    }

    public static void SetScissorRectInplace(Camera cam, Rect r)
    {
        // boundary checking: make sure the rectangle stays within the screen
        if (r.x < 0)
        {
            r.width += r.x;
            r.x = 0;
        }
        if (r.y < 0)
        {
            r.height += r.y;
            r.y = 0;
        }

        // boundary checking: make sure the rectangle bottom-right corner 
        // does not go off the screen, as r.width/r.height may be larger than 1
        r.width = Mathf.Min(1 - r.x, r.width);
        r.height = Mathf.Min(1 - r.y, r.height);

        cam.rect = new Rect(0, 0, 1, 1);
        cam.ResetProjectionMatrix();
        Matrix4x4 m = cam.projectionMatrix;
        cam.rect = r;
        Matrix4x4 m1 = Matrix4x4.TRS(new Vector3(r.x, r.y, 0), Quaternion.identity, new Vector3(r.width, r.height, 1));
        Matrix4x4 m2 = Matrix4x4.TRS(new Vector3((1 / r.width - 1), (1 / r.height - 1), 0), Quaternion.identity, new Vector3(1 / r.width, 1 / r.height, 1));
        Matrix4x4 m3 = Matrix4x4.TRS(new Vector3(-r.x * 2 / r.width, -r.y * 2 / r.height, 0), Quaternion.identity, Vector3.one);
        cam.projectionMatrix = m3 * m2 * m;
    }
}
