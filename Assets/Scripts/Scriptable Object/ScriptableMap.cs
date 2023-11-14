using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Scriptable Map", menuName = "Scriptable Object/Scriptable Map")]
public class ScriptableMap : ScriptableObject
{
    public GameObject MapPrefab;
    public Sprite Background;
    public string MapName;
    public string MapDescription;
    public bool CanMove;

}
