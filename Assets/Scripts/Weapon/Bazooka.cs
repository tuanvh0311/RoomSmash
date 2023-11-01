using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Weapon
{

    // Start is called before the first frame update
    public override void Shoot(Vector3 vec, GameObject s)
    {
        
        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity);
        newObject.transform.LookAt(vec * 100);
        base.Shoot(vec, s);
    }
}
