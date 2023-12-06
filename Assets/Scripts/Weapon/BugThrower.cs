using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugThrower : Weapon
{
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {

        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity, parent);
        newObject.transform.LookAt((vec) * 100000);
        newObject.GetComponent<Flyingbitch>().lockFlyDirection(vec);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (!rb) rb = newObject.AddComponent<Rigidbody>();
        rb.solverIterations = 255;
        base.Shoot(vec, s, parent);
    }
}
