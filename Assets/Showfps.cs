using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Showfps : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    public TextMeshProUGUI ResolutionText;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;


    void Update()
    {
        // Update time.
        time += Time.deltaTime;

        // Count this frame.
        frameCount++;

        if (time >= pollingTime)
        {
            // Update frame rate.
            int frameRate = Mathf.RoundToInt((float)frameCount / time);
            FpsText.text = frameRate.ToString() + " fps";

            // Reset time and frame count.
            time -= pollingTime;
            frameCount = 0;
        }
        ResolutionText.text = Screen.width + "x" + Screen.height;
    }
}
