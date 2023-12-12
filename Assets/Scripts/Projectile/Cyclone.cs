using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cyclone : MonoBehaviour
{
    private float VortexStrength = 1500f;
    private float SwirlStrength = 50f;
    private float radius = 5f;
    private Vector3 direction = Vector3.zero;
    private float changeDirectionTimer = 0f;

    public LayerMask layerMask;

    
    void FixedUpdate()
    {
        changeDirectionTimer -= Time.deltaTime;
        if(changeDirectionTimer <= 0)
        {
            direction = new Vector3(Random.Range(-1f, 1f),
                                            0,
                                            Random.Range(-1f, 1f));
            changeDirectionTimer = 0.5f;
        }
        
        GetComponent<Rigidbody>().velocity = direction * 1f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            Rigidbody rb = Cache.GetRigidbodyFromCollider(hitCollider);
            //Rigidbody rb = hitCollider.attachedRigidbody;
            if (rb == null) continue;

            Vector3 direction = transform.position - rb.position;
            direction = new Vector3(direction.x,0f, direction.z);
            float distance = Vector3.Distance(rb.position, transform.position);
            float distanceY = Vector3.Distance(new Vector3(0,rb.position.y,0), new Vector3(0, transform.position.y, 0));
            distanceY = (distanceY < 1) ? 1 : distanceY;
            if (distance < 1) distance = 1;           
            addForceToRigid(rb, direction, distance);
            float mass = rb.mass / 2;
            if (mass < 1f) mass = 1f;
            var tangent = (SwirlStrength / mass * (distanceY / 10f)) * Vector3.Cross(direction, Vector3.up).normalized;
            rb.velocity = tangent;

        }
    }
    void addForceToRigid(Rigidbody rb, Vector3 direction, float distance)
    {
        float random = Random.Range(-100f, 150f);
        rb.AddForce((VortexStrength /distance / 2 / rb.mass) * direction, ForceMode.Force);
        rb.AddForce(Vector3.up * random, ForceMode.Force);
        rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f)));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
