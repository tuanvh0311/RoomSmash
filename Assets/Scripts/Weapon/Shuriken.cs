using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UltimateFracturing;
using UnityEngine;

public class Shuriken : Weapon
{
    public float projectileSpeed = 10f;

    public override void Shoot(Vector3 vec, GameObject s)
    {       
        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity);
        newObject.transform.LookAt(vec*100);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.AddForce(vec * projectileSpeed, ForceMode.VelocityChange);
        rb.angularVelocity = new Vector3 (0, 180, 0);
        rb.solverIterations = 255;
        base.Shoot(vec, s);
        //newObject.GetComponent<Rigidbody>().AddForceAtPosition(ray.direction * 5, hit.point, ForceMode.VelocityChange);

    }
}
