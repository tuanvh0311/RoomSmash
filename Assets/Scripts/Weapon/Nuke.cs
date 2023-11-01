using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : Weapon
{
    public GameObject groundChurnPrefab = null;
    public float groundChurnDistance = 110;
    public float nukeDistance = 100;
    public GameObject windZone = null;
    public GameObject dustWallPrefab = null;
    public GameObject shockWallPrefab = null;
    public float shockwaveSpeed = 800;
    public float dustWallDistance = 150;

    public override void Shoot(Vector3 vec, GameObject s)
    {
            
            if (!canShoot()) return;
            FadeIn flashEffect = gameObject.AddComponent<FadeIn>();
            flashEffect.startColor = Color.white;
            flashEffect.fadeLength = 5f;

            // position the nuke 2500m in front of where the player is facing.
            Transform player = s.transform;
            Vector3 nukeForwardPos = player.position + player.forward * nukeDistance;
            Vector3 nukePos = new Vector3(nukeForwardPos.x, 0f, nukeForwardPos.z);
            if (groundChurnPrefab != null)
            {
                GameObject groundChurn = Instantiate(groundChurnPrefab, nukePos, Quaternion.identity) as GameObject;
                Follow followScript = groundChurn.AddComponent<Follow>();
                followScript.isPositionFixed = true;
                followScript.objectToFollow = player;
                followScript.facingDirection = FacingDirection.FixedPosition;
                followScript.fixedFromPosition = nukePos;
                followScript.fixedDistance = groundChurnDistance;
            }
            GameObject nuke = Instantiate(projectilePrefab, nukePos, Quaternion.Euler(Vector3.zero)) as GameObject;
            nuke.transform.LookAt(player);

            // Configure Wind Zone
            if (windZone != null)
            {
                windZone.transform.position = nukeForwardPos;
                windZone.transform.LookAt(player);
                Invoke("EnableWindZone", 5f);
                DisableAfter disableAfter = windZone.GetComponent<DisableAfter>() ?? windZone.AddComponent<DisableAfter>();
                disableAfter.seconds = 25f;
                disableAfter.removeScript = true;
            }

            // Configure Dust Wall
            if (dustWallPrefab != null)
            {
                GameObject dustWall = Instantiate(dustWallPrefab, nukeForwardPos, Quaternion.Euler(Vector3.zero)) as GameObject;
                dustWall.transform.LookAt(player);
                dustWall.transform.position += (dustWall.transform.forward * dustWallDistance);
                dustWall.GetComponent<Rigidbody>().AddForce(dustWall.transform.forward * shockwaveSpeed, ForceMode.Force);
                DustWall dwScript = dustWall.GetComponent<DustWall>();
                dwScript.fixedFromPosition = nukePos;
            }

            // Configure Shock Wall
            if (shockWallPrefab != null)
            {
                GameObject shockWall = Instantiate(shockWallPrefab, nukeForwardPos, Quaternion.Euler(Vector3.zero)) as GameObject;
                shockWall.transform.LookAt(player);
                shockWall.GetComponent<Rigidbody>().AddForce(shockWall.transform.forward * shockwaveSpeed, ForceMode.Force);
                ShockWall swScript = shockWall.GetComponent<ShockWall>();
                swScript.origin = nukePos;
            }

        base.Shoot(vec, s);

    }
    }
