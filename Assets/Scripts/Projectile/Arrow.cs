using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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
        if (stickObject) return;
        if(collision.gameObject.GetComponent<Destructible>())
        {
            ImpactDamage impactDamage = new ImpactDamage { DamageAmount = 20f, AdditionalForce = 0f, AdditionalForcePosition = collision.contacts[0].point, AdditionalForceRadius = .5f };
            collision.gameObject.GetComponent<Destructible>().ApplyDamage(impactDamage);
        }        
        stickObject = collision.gameObject;
        SetKinematic(true);
        setParent(collision.transform);
        col.enabled = false;
    }



}
