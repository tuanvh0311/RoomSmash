using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public float bulletDamage = 30;
    public float bulletForce = 200;
    // Start is called before the first frame update
    public override void Shoot(Vector3 vec, GameObject s)
    {       
        if (!canShoot()) return;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity);
        newObject.transform.LookAt(vec * 100000);
        newObject.GetComponent<Bullet>().bulletDamage = bulletDamage;
        newObject.GetComponent<Bullet>().bulletForce = bulletForce;
        base.Shoot(vec, s);
    }
}
