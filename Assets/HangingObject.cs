using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingObject : MonoBehaviour
{
    public GameObject[] objectToRelease;
    public Vector3 angularVelocity;
    public Vector3 forceToAdd;

    private Vector3 _velocityLastUpdate;

    public void Release()
    {
        
        foreach (var item in objectToRelease)
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().WakeUp();
        }

    }
}
