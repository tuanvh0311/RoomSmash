using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingbitch : MonoBehaviour
{
    public GameObject[] cutRight = new GameObject[4];
    public GameObject[] cutLeft = new GameObject[4];
    public float currentActiveIndex = 0;
    private Rigidbody rbody;
    private bool locked;
    private Vector3 dir;
    public void OnEnable()
    {
        rbody = GetComponent<Rigidbody>();
        locked = false;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < cutRight.Length; i++)
        {
            cutRight[i].SetActive(!cutRight[i].active);
            
        }
        rbody.velocity = dir * 10f;   
    }
    public void lockFlyDirection(Vector3 vec)
    {
        if (!locked)
        {
            dir = vec;
            locked = true;
        }       
    }
}
