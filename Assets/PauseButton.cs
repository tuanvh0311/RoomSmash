using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject resumeButton;
    public GameObject backToMenuButton;
    public void onClickFromMenu()
    {
        resumeButton.SetActive(true);
        backToMenuButton.SetActive(false);
    }
    public void onClickFromIngame()
    {
        resumeButton.SetActive(true);
        backToMenuButton.SetActive(true);
    }
}
