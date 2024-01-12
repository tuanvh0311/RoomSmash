using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 0);
            PlayerPrefs.SetInt("PackBoughted", 0);
            PlayerPrefs.SetInt("AdsRemove", 0);
        }

    }
}
