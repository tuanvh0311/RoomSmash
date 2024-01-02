using API.Sound;
using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBomb : Bomb
{
    public override void Explode()
    {
        base.Explode();       
        GameObject blackhole = ObjectPool.Instance.Spawn(explosionPrefab, transform.position, Quaternion.identity);
        SoundManager.Ins.PlaySFXWithouPooling(15, blackhole, false);
        CameraController.Instance.startShakeCamera(10f, 1f);
        ObjectPool.Instance.PoolObject(gameObject);       
    }
    

}
