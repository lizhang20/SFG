using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;

namespace UnityStandardAssets.Utility
{
    [RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "{0} FPS";
        private Text m_GuiText;

        private string FPSLogFilePath;


        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            m_GuiText = GetComponent<Text>();

            // assign FPSLogFilePath
            FPSLogFilePath = Path.Combine(Application.persistentDataPath, $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_fps.txt");
            if (!File.Exists(FPSLogFilePath))
            {
                Debug.Log($"FPS Log File Path: {FPSLogFilePath}");
                File.Create(FPSLogFilePath).Dispose();
            }
        }


        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int) (m_FpsAccumulator/fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_GuiText.text = string.Format(display, m_CurrentFps);

                // append FPS to file
                AppendFPSToFile(m_CurrentFps);
            }
        }

        private async void AppendFPSToFile(int FPS)
        {
            // epoch and FPS in a string
            string content = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()} {FPS}\n";
            await Task.Run(() => File.AppendAllText(FPSLogFilePath, content));
        }
    }
}
