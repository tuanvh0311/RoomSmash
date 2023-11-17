using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : AttachableObject
{
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(!stickObject)
        rb.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }
    
    public override void release()
    {
        base.release();
        col.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rb == null) return;
        if (rb.isKinematic == true) return;
        if (stickObject) return;
                
        stickObject = collision.gameObject;
        SetKinematic(true);
        setParent(collision.transform);
        col.enabled = false;
    }



}
