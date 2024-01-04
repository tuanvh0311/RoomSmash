using API.Sound;
using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Weapon
{
    private GameObject lazer;
    private GameObject lazerHitEffect;
    private AudioSource lazerSound;
    private AudioSource shootLazerSound;
    public LayerMask layermask;
    public GameObject particlePrefab;
    
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        if (!lazer)
            lazer = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        if (!shootLazerSound) shootLazerSound = SoundManager.Ins.PlaySFXWithouPooling(8, lazer, false);
        if(!isShooting) shootLazerSound.Play();
        if (lazerSound == null)
        {
            lazerSound = SoundManager.Ins.PlaySFXWithouPooling(20, lazer, false);
        }
        else
        {
            if (!lazerSound.isPlaying)
                lazerSound.Play();
        }
        LineRenderer lazerLineRender = lazer.GetComponent<LineRenderer>();
        lazerLineRender.SetPosition(0, s.transform.position);
        lazerLineRender.SetPosition(1, vec * 100000f);
        LineRenderer lazerLineRendererChild = lazerLineRender.transform.GetChild(0).GetComponent<LineRenderer>();
        lazerLineRendererChild.SetPosition(0, s.transform.position);
        lazerLineRendererChild.SetPosition(1, vec * 100000f);
        lazerLineRender.enabled = true;
        lazerLineRendererChild.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(s.transform.position, vec * 100000, out hit, 3000f, layermask))
        {
            if (!lazerHitEffect)
                lazerHitEffect = ObjectPool.Instance.Spawn(particlePrefab, hit.point, Quaternion.identity, s.transform);
            else lazerHitEffect.transform.position = hit.point;
            lazerLineRender.SetPosition(1, hit.point);
            lazerLineRendererChild.SetPosition(1,hit.point);
            lazerHitEffect.GetComponent<ParticleSystem>().Play();
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(10f*Time.timeScale);
            hit.rigidbody?.AddForce(s.transform.forward * 5f, ForceMode.Impulse);
            hit.transform.GetComponent<ragdollController>()?.RagdollOnMode();
        }
        else
        {
            lazerHitEffect?.GetComponent<ParticleSystem>().Stop();
        }
        isShooting = true;
    }
    public override void stopShooting()
    {
        if (isShooting)
        {
            lazerSound.Stop();
            base.stopShooting();
        }

    }

}
