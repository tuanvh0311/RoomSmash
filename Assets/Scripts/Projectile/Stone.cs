using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPool.Instance.Spawn(particle, collision.contacts[0].point, Quaternion.identity, transform.parent);
        ObjectPool.Instance.PoolObject(gameObject, true);
        
    }
}
