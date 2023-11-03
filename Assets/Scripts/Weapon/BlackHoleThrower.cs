using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleThrower : Weapon
{
    // Start is called before the first frame update
    public float Force;
    public override void Shoot(Vector3 vec, GameObject s)
    {
        
        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity);
        newObject.transform.LookAt((vec) * 100000);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.mass = 1;
        rb.AddForce(vec * Force, ForceMode.Impulse);
        //rb.AddTorque(vec * projectileSpeed, ForceMode.Force);
        rb.solverIterations = 255;
        base.Shoot(vec, s);
        //newObject.GetComponent<Rigidbody>().AddForceAtPosition(ray.direction * 5, hit.point, ForceMode.VelocityChange);

    }
}
