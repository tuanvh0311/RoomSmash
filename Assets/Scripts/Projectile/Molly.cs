using API.Sound;
using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molly : Bomb
{
    public LayerMask terrainLayerMask;
    public GameObject mollyPrefab;
    public override void Explode()
    {
        base.Explode();       
        RaycastHit hit;
        GameObject explosion = ObjectPool.Instance.Spawn(explosionPrefab, transform.position, Quaternion.identity);      
        GameObject molly = ObjectPool.Instance.Spawn(mollyPrefab, transform.position, Quaternion.identity);
        SoundManager.Ins.PlaySFXWithouPooling(12, molly, true);
        

        ObjectPool.Instance.PoolObject(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        SoundManager.Ins.PlaySFXWithouPooling(11, mollyPrefab, false);
    }
}
