﻿using DestroyIt;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Cache
{

    private static Dictionary<Collider, Rigidbody> rigidDict = new Dictionary<Collider, Rigidbody>();
    private static Dictionary<Collider, Destructible> destructibleDict = new Dictionary<Collider, Destructible>();
    //private static Dictionary<GameObject, Rigidbody> goDict = new Dictionary<GameObject, Rigidbody>();

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

    public static Destructible GetDestructibleFromCollider(Collider collider)
    {
        if (!destructibleDict.ContainsKey(collider))
        {
            Destructible dest = collider.transform.parent.GetComponent<Destructible>();
            if(!dest)
                dest = collider.GetComponent<Destructible>();
            destructibleDict.Add(collider, dest);
        }

        return destructibleDict[collider];
    }




    public static bool IsPointerOverUIObject()
    {
        var eventSystem = EventSystem.current;
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        return results.Count > 0;
    }

}
