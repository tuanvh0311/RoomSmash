using API.Sound;
using System.Collections;
using System.Collections.Generic;
using UltimateFracturing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    bool IsLocked = false;
    [SerializeField] bool BuyToUnlock = false;
    [SerializeField] bool WatchAdsToUnlock = false;
    public float cooldown = 30f;
    public WeaponType weaponType = WeaponType.THROWABLE;
    [SerializeField] GameObject weaponSprite;
    public GameObject background;
    [SerializeField] Sprite sprite;
    public GameObject projectilePrefab;
    public bool isHoldToShoot = false;
    [SerializeField] int shootAudioIndex;
    public bool isShooting = false;
    private string WeaponName;
    public virtual void Shoot(Vector3 vec, GameObject shootPos, Transform parent)
    {
        isShooting = true;
        playShootSound();
        setCooldown();
    }
    private void Start()
    {
        
        if(BuyToUnlock)
        {          
            IsLocked = true;
            
        }
        if(WatchAdsToUnlock)
        {
            IsLocked = true;          
        }
        checkUnlock();
    }
    public virtual void stopShooting()
    {
        isShooting = false;
    }
    public bool canShoot()
    {
        if (GameManager.Instance.cooldown > 0) return false;
        return true;
    }
    public void playShootSound()
    {
        if(shootAudioIndex != 0)
        SoundManager.Ins.PlaySFX(shootAudioIndex);
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
        checkUnlock();
    }
    public void onWeaponSelect()
    {
        if(IsLocked) return;
        GameManager.Instance.OnChangeWeapon(this);        
    }
    public void checkUnlock()
    {
        WeaponName = this.GetType().Name;
        if (BuyToUnlock)
        {
            IsLocked = PlayerPrefs.GetInt("PackBoughted") == 0;
        }

        if (WatchAdsToUnlock)
        {
            if (PlayerPrefs.GetInt("AdsRemove") == 1)
            {
                IsLocked = false;
                return;
            }
            if (!PlayerPrefs.HasKey(WeaponName + "IsLocked"))
            {
                PlayerPrefs.SetInt(WeaponName + "IsLocked", 0);
            }

            else
            {
                IsLocked = PlayerPrefs.GetInt(WeaponName + "IsLocked") == 0;
            }
        }

    }
    
}

