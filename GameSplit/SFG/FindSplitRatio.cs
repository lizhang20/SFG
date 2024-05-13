using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindSplitRatio : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Text splitRatioText;

    [SerializeField] private float leftRatio = 0.1f;
    private Rect leftScissorRect = new Rect(0, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        leftScissorRect.Set(0, 0, leftRatio, 1);
        Scissor.SetScissorRectInplace(cam, leftScissorRect);
        splitRatioText.text = leftRatio.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        leftRatio += 0.1f;
        leftScissorRect.Set(0, 0, leftRatio, 1);
        Scissor.SetScissorRectInplace(cam, leftScissorRect);
        splitRatioText.text = leftRatio.ToString();
    }
}
