using API.Sound;
using DestroyIt;
using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : Weapon
{
    private GameObject lightningBolt;
    private GameObject hitEffect;
    private AudioSource lightningAudio;
    public LayerMask layermask;
    public GameObject particlePrefab;
    public float maxRange = 50f;

    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        if (!lightningBolt)
            lightningBolt = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        if (lightningAudio == null)
        {
            lightningAudio = SoundManager.Ins.PlaySFXWithouPooling(7,lightningBolt, false);

        }
        else
        {
            if (!lightningAudio.isPlaying)
                lightningAudio.Play();
        }
        LineRenderer lightningLineRender = lightningBolt.GetComponent<LineRenderer>();
        lightningLineRender.enabled = true;
        lightningLineRender.SetPosition(0, s.transform.position);
        lightningLineRender.SetPosition(1, vec * 100000f);

        LineRenderer lightningLineRenderChild = lightningLineRender.transform.GetChild(0).GetComponent<LineRenderer>();
        lightningLineRenderChild.SetPosition(0, s.transform.position);
        lightningLineRenderChild.SetPosition(1, vec * 100000f);

        //lightningLineRenderChild.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(s.transform.position, vec * 100000f, out hit, maxRange,layermask))
        {
            if (!hitEffect)
                hitEffect = ObjectPool.Instance.Spawn(particlePrefab, hit.point, Quaternion.identity, s.transform);
            else hitEffect.transform.position = hit.point;
            lightningLineRender.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            lightningLineRenderChild.SetPosition(1, hit.point);
            hitEffect.GetComponent<ParticleSystem>().Play();
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(10f*Time.timeScale);
            hit.rigidbody?.AddForce(s.transform.forward * 5f, ForceMode.Impulse);
            hit.transform.GetComponent<ragdollController>()?.RagdollOnMode();
        }
        else
        {         
            hitEffect?.GetComponent<ParticleSystem>().Stop();
        }
        isShooting = true;
    }
    public override void stopShooting()
    {
        if (isShooting)
        {
            lightningAudio.Stop();
            base.stopShooting();
        }

    }
}
