using DestroyIt;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MollyFlame : MonoBehaviour
{
    public float maxScaleMultiplier = 2f;
    public float minScaleMultiplier = 0.5f;
    public float damageTick = 1f;
    public float damageRadius = 2.5f;
    public LayerMask layerMask;
    private float damageTimer = 0f;

    private Tween currentTween;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * 0.1f;
        currentTween = transform.DOScale(Vector3.one * maxScaleMultiplier, 2f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one * minScaleMultiplier, 1f).SetLoops(-1, LoopType.Yoyo);
        });
        currentTween.Play();
    }

    private void OnDisable()
    {
        currentTween.Pause();
    }

    private void FixedUpdate()
    {
        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0f)
        {
            damageTimer = damageTick;
            List<Destructible> damagedList = new List<Destructible>();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius, layerMask);
            foreach (var hitCollider in hitColliders)
            {
                Destructible dest = Cache.GetDestructibleFromCollider(hitCollider);              
                if (dest == null || damagedList.Contains(dest)) continue;            
                dest.ApplyDamage(10f);
            }
        }
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    

}
