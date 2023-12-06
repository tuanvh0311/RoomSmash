using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private Rigidbody rbody;
    private Vector3 lastVelocity;
    public void OnEnable()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        
                rbody.velocity = rbody.velocity.normalized * 30f;
                rbody.angularVelocity = new Vector3(0, 180, 0);
                
        }
    }

