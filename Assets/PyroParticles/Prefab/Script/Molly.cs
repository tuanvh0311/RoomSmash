using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Molly : MonoBehaviour
{
    public float maxScaleMultiplier = 2f;
    public float minScaleMultiplier = 0.5f;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * 0.1f;
        transform.DOScale(Vector3.one * maxScaleMultiplier, 2f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one * minScaleMultiplier, 1f).SetLoops(-1, LoopType.Yoyo);
        });
    }
    // Update is called once per frame
    
}
