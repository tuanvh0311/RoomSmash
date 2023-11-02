using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Weapon
{
    public float bulletDamage = 30;
    public float bulletForce = 200;
    

    // Start is called before the first frame update
    public override void Shoot(Vector3 vec, GameObject s)
    {
        if (!canShoot()) return;
        for (int i = 0; i < 6; i++)
        {
            GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity);
            Vector3 randomVector = new Vector3(vec.x * Random.Range(0.8f, 1f),
                                                vec.y * Random.Range(0.8f, 1f),
                                                vec.z * Random.Range(0.8f, 1f));
            newObject.transform.LookAt(randomVector * 100000);
            newObject.GetComponent<Bullet>().bulletDamage = bulletDamage;
            newObject.GetComponent<Bullet>().bulletForce = bulletForce;
        }
        
        base.Shoot(vec, s);
    }
}
