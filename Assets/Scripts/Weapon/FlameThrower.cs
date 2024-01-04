using API.Sound;
using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlameThrower : Weapon
{
    // Start is called before the first frame update
    private GameObject spray;
    private AudioSource audioSource;
    public LayerMask layermask;
    public bool doDamageable = false;
    private float damageTick = 0f;
    private float burnTick = 0.5f;
    public GameObject smallFire;



    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        damageTick -= Time.fixedDeltaTime;
        burnTick -= Time.fixedDeltaTime;
        GameManager.Instance.cooldown += cooldown;
        if (!spray)
            spray = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, s.transform);
        spray.GetComponent<ParticleSystem>().Emit(1);
        spray.transform.LookAt((vec) * 100000);
        if (audioSource == null)
        {
            audioSource = SoundManager.Ins.PlaySFXWithouPooling(22,spray, false);
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        RaycastHit hit;
        if (doDamageable && damageTick <= 0 && Physics.Raycast(s.transform.position, vec * 100000, out hit, 13f, layermask))
        {
            damageTick = 0.5f;
            float distance = Vector3.Distance(s.transform.position, hit.point);
            distance = distance < 5 ? distance : 5;
            hit.transform.GetComponent<Destructible>()?.ApplyDamage(300f / distance);
            hit.transform.GetComponent<ragdollController>()?.RagdollOnMode();
            if(burnTick <= 0)
            {
                GameObject burnEffect = ObjectPool.Instance.Spawn(smallFire, hit.point, Quaternion.identity, GameManager.Instance.map.transform);               
                burnTick = 0.5f;
                AudioSource burnSound = SoundManager.Ins.PlaySFXWithouPooling(12, burnEffect, false);
                burnSound.volume = SoundManager.Ins.volume / 2f;
            }
                

        }
        isShooting = true;
    }
    public override void stopShooting()
    {
        if (isShooting)
        {
            audioSource.Stop();
            base.stopShooting();
        }

    }
}
