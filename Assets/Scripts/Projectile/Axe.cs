using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : AttachableObject
{
    bool isContacted;
    private void OnEnable()
    {
        isContacted = false;
    }
    private void FixedUpdate()
    {
        if(!isContacted)
            transform.Rotate(Vector3.right * 480f * Time.fixedDeltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.parent = other.transform;
        GetComponent<Rigidbody>().isKinematic = true;
        ToggleCollider(false);
        isContacted = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        isContacted = true;
    }
    public override void release()
    {
        base.release();
        ToggleCollider(true); 
    }

    void ToggleCollider(bool value)
    {
        foreach (var item in GetComponentsInChildren<BoxCollider>())
        {
            item.enabled = value;
        }
    }
}
