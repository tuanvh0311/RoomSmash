using System.Collections.Generic;
using UnityEngine;

public static class Cache
{

    private static Dictionary<Collider, Rigidbody> rigidDict = new Dictionary<Collider, Rigidbody>();
    public static int currentMap = 0;

    public static Rigidbody GetRigidbodyFromCollider(Collider collider)
    {
        if (!rigidDict.ContainsKey(collider))
        {
            Rigidbody rb = collider.attachedRigidbody;
            rigidDict.Add(collider, rb);
        }

        return rigidDict[collider];
    }

}
