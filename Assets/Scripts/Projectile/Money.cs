using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPool.Instance.Spawn(particle, collision.contacts[0].point,Quaternion.identity, transform.parent);
        ObjectPool.Instance.PoolObject(gameObject, true);
    }
}
