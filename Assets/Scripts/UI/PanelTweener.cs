using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTweener : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one,0.3f).SetEase(Ease.OutBack).SetUpdate(true);
    }
    public Sequence OnClose()
    {
        return DOTween.Sequence().Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)).SetUpdate(true);
    }
}
