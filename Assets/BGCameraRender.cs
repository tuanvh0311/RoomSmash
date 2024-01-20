using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGCameraRender : MonoBehaviour
{
    public Vector3 endValue;
    public float duration;
    void Start()
    {
        transform.DORotate(endValue, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    
}
