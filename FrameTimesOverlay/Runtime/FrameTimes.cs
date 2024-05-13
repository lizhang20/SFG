using System;
using System.Text;
using UnityEngine;
using TMPro;

public class FrameTimes : MonoBehaviour
{
    private FrameTiming[] frameTimings;
    [SerializeField, Range(1, 240)] private int updateInterval = 1;
    [SerializeField, Range(10, 200)] private int mainTextSize = 32;
    [SerializeField, Range(10, 200)] private int secondaryTextSize = 26;

    private bool hasTextfield = false;
    [SerializeField] private TextMeshProUGUI textField;

    private int frameCounter;
    private StringBuilder stringBuilder;
    
    void Start()
    {
        frameTimings = new FrameTiming[updateInterval];
        frameCounter = 0;
        stringBuilder = new StringBuilder(1024);

        hasTextfield = textField != null;
        if(!hasTextfield)
            Debug.LogError("No textfield provided, frame times will be logged instead.", this);
    }

    // void Update()
    // {
    //     FrameTimingManager.CaptureFrameTimings();
    //     frameCounter++;
    //     if (frameCounter < updateInterval)
    //         return;
        
    //     frameCounter = 0;
    //     var frameTimingsCount = FrameTimingManager.GetLatestTimings((uint)frameTimings.Length, frameTimings);
    //     if (frameTimingsCount <= 0) 
    //         return;
            
    //     double cpuMin, cpuMax, cpuAvg, gpuMin, gpuMax, gpuAvg;
    //     cpuMin = cpuMax = cpuAvg = frameTimings[0].cpuFrameTime;
    //     // cpuMin = cpuMax = frameTimings[0].cpuFrameTime;
    //     // cpuAvg = (frameTimings[0].cpuTimePresentCalled - frameTimings[0].cpuTimeFrameComplete) / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
    //     gpuMin = gpuMax = gpuAvg = frameTimings[0].gpuFrameTime;

    //     // Unity 2022
    //     // wait = mainThreadPresentWaitTime
    //     double waitMin, waitMax, waitAvg;
    //     waitMin = waitMax = waitAvg = frameTimings[0].cpuMainThreadPresentWaitTime;
            
    //     for (int i = 1; i < frameTimingsCount; i++)
    //     {
    //         var frame = frameTimings[i];
    //         cpuMin = Math.Min(cpuMin, frame.cpuFrameTime);
    //         cpuMax = Math.Max(cpuMax, frame.cpuFrameTime);
    //         cpuAvg += frame.cpuFrameTime;

    //         gpuMin = Math.Min(gpuMin, frame.gpuFrameTime);
    //         gpuMax = Math.Max(gpuMax, frame.gpuFrameTime);
    //         gpuAvg += frame.gpuFrameTime;

    //         // Unity 2022
    //         waitMin = Math.Min(waitMin, frame.cpuMainThreadPresentWaitTime);
    //         waitMax = Math.Max(waitMax, frame.cpuMainThreadPresentWaitTime);
    //         waitAvg += frame.cpuMainThreadPresentWaitTime;
    //     }
            
    //     // Remove min and max values from the average calculation
    //     if (frameTimingsCount > 2)
    //     {
    //         cpuAvg -= cpuMin;
    //         cpuAvg -= cpuMax;
    //         gpuAvg -= gpuMin;
    //         gpuAvg -= gpuMax;

    //         // Unity 2022
    //         waitAvg -= waitMin;
    //         waitAvg -= waitMax;

    //         frameTimingsCount -= 2;
    //     }
            
    //     cpuAvg /= frameTimingsCount;
    //     gpuAvg /= frameTimingsCount;
    //     waitAvg /= frameTimingsCount;

    //     stringBuilder.Clear();
    //     stringBuilder.Append($"<size={mainTextSize}><b>CPU:</b> {cpuAvg:00.00}</size><size={secondaryTextSize}>ms <i>min:{cpuMin:00.00} max:{cpuMax:00.00}</i></size>\n");
    //     stringBuilder.Append($"<size={mainTextSize}><b>GPU:</b> {gpuAvg:00.00}</size><size={secondaryTextSize}>ms <i>min:{gpuMin:00.00} max:{gpuMax:00.00}</i></size>\n");
    //     stringBuilder.Append($"<size={mainTextSize}><b>Wait:</b> {waitAvg:00.00}</size><size={secondaryTextSize}>ms <i>min:{waitMin:00.00} max:{waitMax:00.00}</i></size>");

    //     if (hasTextfield)
    //         textField.text = stringBuilder.ToString();
    //     else
    //         Debug.Log(stringBuilder.ToString());
    // }

    void Update()
    {
        FrameTimingManager.CaptureFrameTimings();
        frameCounter++;
        if (frameCounter < updateInterval)
            return;
        
        frameCounter = 0;
        var frameTimingsCount = FrameTimingManager.GetLatestTimings((uint)frameTimings.Length, frameTimings);
        Debug.Log($"frameTimingsCount: {frameTimingsCount}");
        if (frameTimingsCount <= 0) 
            return;

        double gpuMin, gpuMax, gpuAvg;
        // cpuMin = cpuMax = frameTimings[0].cpuFrameTime;
        // cpuAvg = (frameTimings[0].cpuTimePresentCalled - frameTimings[0].cpuTimeFrameComplete) / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
        gpuMin = gpuMax = gpuAvg = frameTimings[0].gpuFrameTime;
            
        // Unity 2022
        // wait = mainThreadPresentWaitTime
        double waitMin, waitMax, waitAvg;
        waitMin = waitMax = waitAvg = frameTimings[0].cpuMainThreadPresentWaitTime;
            
        for (int i = 1; i < frameTimingsCount; i++)
        {
            var frame = frameTimings[i];

            gpuMin = Math.Min(gpuMin, frame.gpuFrameTime);
            gpuMax = Math.Max(gpuMax, frame.gpuFrameTime);
            gpuAvg += frame.gpuFrameTime;
            // Unity 2022
            waitMin = Math.Min(waitMin, frame.cpuMainThreadPresentWaitTime);
            waitMax = Math.Max(waitMax, frame.cpuMainThreadPresentWaitTime);
            waitAvg += frame.cpuMainThreadPresentWaitTime;
        }
            
        // Remove min and max values from the average calculation
        // if (frameTimingsCount > 2)
        // {
        //     cpuAvg -= cpuMin;
        //     cpuAvg -= cpuMax;
        //     gpuAvg -= gpuMin;
        //     gpuAvg -= gpuMax;

        //     // Unity 2022
        //     waitAvg -= waitMin;
        //     waitAvg -= waitMax;

        //     frameTimingsCount -= 2;
        // }
            
        waitAvg /= frameTimingsCount;

        stringBuilder.Clear();
        stringBuilder.Append($"<size={mainTextSize}><b>GPU:</b> {gpuAvg:00.00}</size><size={secondaryTextSize}>ms <i>min:{gpuMin:00.00} max:{gpuMax:00.00}</i></size>\n");
        stringBuilder.Append($"<size={mainTextSize}><b>Wait:</b> {waitAvg:00.00}</size><size={secondaryTextSize}>ms <i>min:{waitMin:00.00} max:{waitMax:00.00}</i></size>");

        if (hasTextfield)
            textField.text = stringBuilder.ToString();
        else
            Debug.Log(stringBuilder.ToString());
    }
}
