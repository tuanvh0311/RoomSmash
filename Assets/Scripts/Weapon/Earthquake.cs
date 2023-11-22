using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Weapon
{

    private GameObject shakeObject;

    // Update is called once per frame
    
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {

        if (!canShoot()) return;
        if (!shakeObject)
        {
            shakeObject = ObjectPool.Instance.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity,parent);
        }
        shakeObject.GetComponent<EarthquakeGO>().startShaking();
        base.Shoot(vec, s, parent);

    }
}
