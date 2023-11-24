using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public float radius = 1f;

    public LayerMask layerMask;

    
    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = Cache.GetRigidbodyFromCollider(hitCollider);
            if (rb == null) continue;
            Vector3 direction = transform.position - rb.position;
            float distance = Vector3.Distance(rb.position, transform.position);
            if(distance < 1) distance = 1;
            //rb.AddForce(force.normalized * 1000 * 1/distance / rb.mass, ForceMode.Force);
            addForceToRigid(rb,direction, distance);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destructible des = collision.gameObject.GetComponent<Destructible>();
        if (!des)
        {
            ObjectPool.Instance.PoolObject(collision.gameObject, true);
        }
        
    }
    void addForceToRigid(Rigidbody rb, Vector3 direction, float distance)
    {
        rb.AddForce((3000f / distance / 3) * direction, ForceMode.Force);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
