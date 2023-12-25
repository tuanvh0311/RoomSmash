using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            transform.Rotate(Vector3.right * 800f * Time.fixedDeltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.parent = other.transform;
        GetComponent<Rigidbody>().isKinematic = true;
        ToggleCollider(false);
        stickObject = other.gameObject;
        isContacted = true;
        if (stickObject.layer == 15)
        {
            setParent(stickObject.transform.GetChild(0));
        }
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
