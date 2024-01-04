using API.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Ins.PlaySFX(25, false);
    }
}
