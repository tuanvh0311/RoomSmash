using DestroyIt;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : Weapon
{
    private GameObject lightningBolt;
    private GameObject hitEffect;
    public LayerMask layermask;
    public GameObject particlePrefab;
    public float maxRange = 50f;

    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        if (!lightningBolt)
            lightningBolt = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        LineRenderer lightningLineRender = lightningBolt.GetComponent<LineRenderer>();
        LightningBoltScript lightningBoltScript = lightningBolt.GetComponent<LightningBoltScript>();
        lightningLineRender.enabled = true;
        lightningBoltScript.EndObject.transform.position = (vec * maxRange) + (Vector3.up * 1.5f);       
        RaycastHit hit;
        if (Physics.Raycast(s.transform.position, vec * 100000, out hit, maxRange))
        {
            if (!hitEffect)
                hitEffect = ObjectPool.Instance.Spawn(particlePrefab, hit.point, Quaternion.identity, s.transform);
            else hitEffect.transform.position = hit.point;
            lightningBoltScript.EndObject.transform.position = hit.point;
            hitEffect.GetComponent<ParticleSystem>().Play();
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(10f*Time.timeScale);
        }
        else
        {         
            hitEffect?.GetComponent<ParticleSystem>().Stop();
        }
    }
}
