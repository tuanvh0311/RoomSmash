using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTypeSlot : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.THROWABLE;
    [SerializeField] GameObject Type;
    [SerializeField] Sprite sprite;

    private void Start()
    {
        if (sprite == null) Type.SetActive(false);
        else 
        {  
            Type.SetActive(true);
            Type.GetComponent<Image>().sprite = sprite;
        }
    }
    public void getWeaponType()
    {
        GameManager.Instance.checkWeaponType(weaponType); 
    }
}
