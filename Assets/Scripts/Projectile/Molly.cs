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
        if (Physics.Raycast(transform.position,Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            GameObject molly = ObjectPool.Instance.Spawn(mollyPrefab, hit.point, Quaternion.identity);
        }
        ObjectPool.Instance.PoolObject(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
