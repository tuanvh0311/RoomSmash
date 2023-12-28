using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingbitch : MonoBehaviour
{
    public GameObject wing1;
    public GameObject wing2;
    private Rigidbody rbody;
    private bool locked;
    private Vector3 dir;
    public void OnEnable()
    {
        wing2.transform.DORotate(new Vector3(0f, 0f, 90f), 0.05f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        wing1.transform.DORotate(new Vector3(0f, 0f, -90f), 0.05f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        rbody = GetComponent<Rigidbody>();
        locked = false;
    }

    void FixedUpdate()
    {        
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
