using API.Sound;
using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Weapon
{
    public float bulletDamage = 30;
    public float bulletForce = 200;

    private float range = 0.05f;
    // Start is called before the first frame update
    public override void Shoot(Vector3 vec, GameObject s, Transform parent)
    {
        if (!canShoot()) return;
        for (int i = 0; i < 6; i++)
        {
            GameObject newObject = ObjectPool.Instance.Spawn(projectilePrefab, s.transform.position, Quaternion.identity, parent);
            Vector3 randomVector = new Vector3(vec.x + Random.Range(-range, range),
                                                vec.y + Random.Range(-range, range),
                                                vec.z + Random.Range(-range, range));
            // Test:
            //Vector3 randomVector = vec;
            newObject.transform.LookAt(randomVector * 1000);
            newObject.GetComponent<Bullet>().bulletDamage = bulletDamage;
            newObject.GetComponent<Bullet>().bulletForce = bulletForce;
        }
        //float randomSoundNumber = Random.Range(0f, 1);
        //if(randomSoundNumber < 0.5f)
        //    SoundManager.Ins.PlaySFXRandomPitch(0, false);
        //else
        //    SoundManager.Ins.PlaySFXRandomPitch(1, false);
        
        base.Shoot(vec, s, parent);
    }
    public override void stopShooting()
    {
        if (isShooting)
        {
            SoundManager.Ins.PlaySFX(3, false);
            base.stopShooting();
        }
          
    }
}
