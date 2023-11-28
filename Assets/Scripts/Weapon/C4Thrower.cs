using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Thrower : Weapon
{
    public float Force;
    public Detonator detonator;

    private void OnEnable()
    {
        detonator = GameManager.Instance.detonator;
    }
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {

        if (!canShoot()) return;
        detonator.gameObject.SetActive(true);
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity, parent);
        detonator.explosives.Add(newObject);
        newObject.transform.LookAt((vec) * 100000);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        if (!rb) rb = newObject.AddComponent<Rigidbody>();
        rb.AddForce(vec * Force, ForceMode.Impulse);
        rb.AddTorque(new Vector3(30f, 0, 0) * Force, ForceMode.Impulse);
        rb.solverIterations = 255;
        base.Shoot(vec, s, parent);

    }
}
