using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon
{
    private GameObject flame;
    public LayerMask layermask;



    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {

        if (!canShoot()) return;
        GameManager.Instance.cooldown += cooldown;
        GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity, parent);
        newObject.transform.LookAt((vec) * 100000);
        RaycastHit hit;
        if(Physics.Raycast(s.transform.position,vec * 100000,out hit, 13f))
        {           
            float distance = Vector3.Distance(s.transform.position, hit.point);
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(30f/distance);           
        }


    }

    


}
