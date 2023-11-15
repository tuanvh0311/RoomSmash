using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
