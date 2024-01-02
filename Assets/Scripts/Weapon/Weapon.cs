using API.Sound;
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
    public GameObject background;
    [SerializeField] Sprite sprite;
    public GameObject projectilePrefab;
    public bool isHoldToShoot = false;
    [SerializeField] int shootAudioIndex;
    public bool isShooting = false; 
    public virtual void Shoot(Vector3 vec, GameObject shootPos, Transform parent)
    {
        isShooting = true;
        playShootSound();
        setCooldown();
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
    }
    public void onWeaponSelect()
    {
        GameManager.Instance.OnChangeWeapon(this);
        

    }
    
}

