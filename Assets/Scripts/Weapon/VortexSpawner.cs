using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : Weapon
{
    public LayerMask layerMask;

    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {

        if (!canShoot()) return;
        
        Ray ray = new Ray();
        ray.direction = vec;
        RaycastHit hit;
        if (Physics.Raycast(s.transform.position,ray.direction, out hit, Mathf.Infinity))
        {
            Vector3 spawnPosition = hit.point;           
                RaycastHit anotherRaycastHit;
                if (Physics.Raycast(hit.point, Vector3.down, out anotherRaycastHit, Mathf.Infinity, layerMask))
                {
                        
                     RaycastHit[] hits = Physics.RaycastAll(hit.point, Vector3.down, Mathf.Infinity, layerMask);
                     spawnPosition = hits[0].point;
                }
            
            GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, spawnPosition, Quaternion.identity, parent);
            CameraController.Instance.startShakeCamera(10f, 1f);
        }
        base.Shoot(vec, s, parent);

    }

}
