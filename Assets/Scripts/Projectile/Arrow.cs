using DestroyIt;
using UnityEngine;

public class Arrow : AttachableObject
{

    private bool collied = false;
    

    // Update is called once per frame
    void Update()
    {
        if(!collied)
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
        collied = true;
    }



}
