using DestroyIt;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedRocket : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0,0, 5, Space.Self);
    }
}
