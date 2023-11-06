using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explodeTimer;
    private float currentTimer;
    private bool hasExploded;

    public virtual void OnEnable()
    {
        hasExploded = false;
        currentTimer = explodeTimer;
    }


    public virtual void Explode()
    {
        hasExploded = true;
    }
    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0 && !hasExploded)
        {
            Explode();
            
        }
    }
}
