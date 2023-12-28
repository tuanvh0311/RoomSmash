using DestroyIt;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public class MollyFlame : MonoBehaviour
{
    public float damageTick = 1f;
    public float damageRadius = 2.5f;
    public float fireHeight = 3f;
    public LayerMask layerMask;
    private float damageTimer = 0f;
    private bool grounded = true;
    public LayerMask groundLayerMask;
    public bool gravityAffected = true;

    private void FixedUpdate()
    {
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f)
        {           
            List<Destructible> damagedList = new List<Destructible>();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius, layerMask);
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<ragdollController>()?.RagdollOnMode();
                Destructible dest = Cache.GetDestructibleFromCollider(hitCollider);              
                if (dest == null || damagedList.Contains(dest)) continue;            
                dest.ApplyDamage(10f);
                
            }
            damageTimer = damageTick;
        }
        if (gravityAffected)
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.position + Vector3.down, fireHeight, groundLayerMask))
            {
                grounded = true;

            }
            else
            {
                grounded = false;
            }
            if (!grounded)
            {
                transform.position += Vector3.down * Time.fixedDeltaTime * 5f;
            }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, fireHeight, groundLayerMask);
            if( hitColliders.Length == 0 ) 
            {
                transform.position += Vector3.down * Time.fixedDeltaTime * 5f;
            }
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * fireHeight);
    }
    

}
