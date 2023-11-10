using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Image background;
    public GameObject panel;
    private bool isToggle = false;
    // Start is called before the first frame update
    public void onToggle()
    {
        if (isToggle)
        {
            isToggle = false;
            background.color = Color.black;
            panel.SetActive(false);
        }
        else
        {
            isToggle = true;
            background.color = Color.yellow;
            panel.SetActive(true);
        }
    }
}
