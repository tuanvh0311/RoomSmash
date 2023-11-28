using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprayer : Weapon
{
    private GameObject spray;    
    public LayerMask layermask;
    public bool doDamageable = false;
    private float damageTick = 0f;


    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        damageTick -= Time.fixedDeltaTime;
        GameManager.Instance.cooldown += cooldown;
        if(!spray)
        spray = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        spray.GetComponent<ParticleSystem>().Play();
        spray.transform.LookAt((vec) * 100000);
        RaycastHit hit;
        if(doDamageable && damageTick <= 0 && Physics.Raycast(s.transform.position,vec * 100000,out hit, 13f))
        {   
            damageTick = 0.5f;
            float distance = Vector3.Distance(s.transform.position, hit.point);
            distance = distance < 5 ? distance : 5;
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(300f/distance);           
        }
    }

    


}
