using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsTweener : MonoBehaviour
{
    public GameObject ButtonList;
    public GameObject Arrow;
    private void OnEnable()
    {

        ButtonList.transform.localScale = Vector3.zero;
        ButtonList.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        Arrow.transform.localScale = Vector3.zero;
        Arrow.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
    }
    public Sequence OnClose()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(ButtonList.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)).SetUpdate(true);
        sequence.Join(Arrow.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)).SetUpdate(true);
        return sequence;
    }
}
