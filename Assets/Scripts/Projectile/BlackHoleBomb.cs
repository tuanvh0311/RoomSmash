using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBomb : Bomb
{
    public override void Explode()
    {
        base.Explode();       
        Debug.Log("explose");
        GameObject blackhole = ObjectPool.Instance.Spawn(explodeObject, transform.position, Quaternion.identity);
        ObjectPool.Instance.PoolObject(gameObject);       
    }
    

}
