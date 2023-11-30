using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingbitch : MonoBehaviour
{
    public GameObject[] cutRight = new GameObject[4];
    public GameObject[] cutLeft = new GameObject[4];
    public float currentActiveIndex = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < cutRight.Length; i++)
        {
            cutRight[i].SetActive(!cutRight[i].active);
        }

    }
}
