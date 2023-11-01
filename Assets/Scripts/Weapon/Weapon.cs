using System.Collections;
using System.Collections.Generic;
using UltimateFracturing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float cooldown = 30f;
    public WeaponType weaponType = WeaponType.THROWABLE;
    [SerializeField] GameObject weaponSprite;
    [SerializeField] GameObject background;
    [SerializeField] Sprite sprite;
    public GameObject projectilePrefab;
    public bool isHoldToShoot = false;

    public virtual void Shoot(Vector3 vec, GameObject shootPos)
    {
        setCooldown();
    }
    public bool canShoot()
    {
        if (!projectilePrefab) return false;
        if (GameManager.Instance.cooldown > 0) return false;
        return true;
    }
    
    public void AddSprite()
    {
        if (sprite == null) weaponSprite.SetActive(false);
        else
            {
                weaponSprite.SetActive(true);
                weaponSprite.GetComponent<Image>().sprite = sprite;
            }
    }
    public void setCooldown()
    {
        GameManager.Instance.cooldown = cooldown;
    }
    public void hideWeapon()
    {
        gameObject.SetActive(false);
    }
    public void showWeapon()
    {
        gameObject.SetActive(true);
    }
    public void onWeaponSelect()
    {
        GameManager.Instance.cooldown = 0;
        GameManager.Instance.currentWeapon = this;
    }
    
}

