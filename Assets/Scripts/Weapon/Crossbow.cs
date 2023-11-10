using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public float projectileSpeed;
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        
        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity, parent);
        newObject.transform.LookAt((vec)* 100000);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.AddForce(vec * projectileSpeed, ForceMode.Impulse);
        //rb.AddTorque(vec * projectileSpeed, ForceMode.Force);
        rb.solverIterations = 255;
        base.Shoot(vec, s, parent);
        //newObject.GetComponent<Rigidbody>().AddForceAtPosition(ray.direction * 5, hit.point, ForceMode.VelocityChange);

    }
}
