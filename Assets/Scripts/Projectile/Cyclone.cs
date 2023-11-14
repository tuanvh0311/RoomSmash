using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cyclone : MonoBehaviour
{
    private float VortexStrength = 1500f;
    private float SwirlStrength = 20f;
    private float radius = 10f;
    private Vector3 direction = Vector3.zero;
    private float changeDirectionTimer = 0f;

    public LayerMask layerMask;

    private void OnEnable()
    {
        
    }
    void FixedUpdate()
    {
        changeDirectionTimer -= Time.deltaTime;
        if(changeDirectionTimer <= 0)
        {
            direction = new Vector3(Random.Range(-0.1f, 0.1f),
                                            0,
                                            Random.Range(-0.1f, 0.1f));
            changeDirectionTimer = 5f;
        }
        
        transform.position += direction * Time.fixedDeltaTime * 5f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = Cache.GetRigidbodyFromCollider(hitCollider);
            if (rb == null) continue;

            Vector3 direction = transform.position - rb.position;
            direction = new Vector3(direction.x,0f, direction.z);
            float distance = Vector3.Distance(rb.position, transform.position);
            if (distance < 1) distance = 1;           
            addForceToRigid(rb, direction, distance);
            var tangent = Vector3.Cross(direction, Vector3.up).normalized * SwirlStrength;
            rb.velocity = tangent;

        }
    }
    void addForceToRigid(Rigidbody rb, Vector3 direction, float distance)
    {
        float random = Random.Range(-100f, 150f);
        rb.AddForce(direction * VortexStrength /distance/ rb.mass / 5, ForceMode.Force);
        rb.AddForce(Vector3.up * random, ForceMode.Force);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
