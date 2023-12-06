using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Weapon
{
    private GameObject lazer;
    private GameObject lazerHitEffect;
    public LayerMask layermask;
    public GameObject particlePrefab;
    
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        if (!lazer)
            lazer = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        LineRenderer lazerLineRender = lazer.GetComponent<LineRenderer>();
        lazerLineRender.SetPosition(0, s.transform.position);
        lazerLineRender.SetPosition(1, vec * 100000f);
        lazerLineRender.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(s.transform.position, vec * 100000, out hit, 3000f))
        {
            if (!lazerHitEffect)
                lazerHitEffect = ObjectPool.Instance.Spawn(particlePrefab, hit.point, Quaternion.identity, s.transform);
            else lazerHitEffect.transform.position = hit.point;
            lazer.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            lazerHitEffect.GetComponent<ParticleSystem>().Play();
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(1f*Time.timeScale);
        }
        else
        {
            lazerHitEffect?.GetComponent<ParticleSystem>().Stop();
        }
    }
    
}
