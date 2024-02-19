using API.Sound;
using DestroyIt;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{
    bool IsLocked = false;
    [SerializeField] bool BuyToUnlock = false;
    [SerializeField] bool WatchAdsToUnlock = false;
    [SerializeField] int NumberOfAds = 1;
    public float cooldown = 30f;
    public WeaponType weaponType = WeaponType.THROWABLE;
    [SerializeField] GameObject weaponSprite;
    public GameObject background;
    [SerializeField] Sprite sprite;
    public GameObject projectilePrefab;
    public bool isHoldToShoot = false;
    [SerializeField] int shootAudioIndex;
    public bool isShooting = false;
    public string WeaponName;
    private GameObject LockIcon;
    private Sprite LockIconImage;
    private UIManager uimanager;
    public virtual void Shoot(Vector3 vec, GameObject shootPos, Transform parent)
    {
        isShooting = true;
        playShootSound();
        setCooldown();
    }
    private void Awake()
    {
        uimanager = GameManager.Instance.UIManager;
        WeaponName = gameObject.name.Replace("(Clone)", "").Trim(); ;
        if (BuyToUnlock)
        {          
            IsLocked = true;
            LockIconImage = uimanager.LockIcon;
        }
        if(WatchAdsToUnlock)
        {
            IsLocked = true;
            LockIconImage = uimanager.WatchAdsIcon;
        }
        AddLockIcon();
        CheckIsLocked();
    }
    public string GetWeaponName()
    {
        return WeaponName;
    }
    public Sprite GetWeaponIcon()
    {
        return sprite;
    }
    public int GetNumberOfAds()
    {
        return NumberOfAds;
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
    public void AddLockIcon()
    {
        LockIcon = new GameObject("Lock Icon");
        LockIcon.transform.parent = gameObject.transform;
        LockIcon.transform.localPosition = new Vector2(40f, 40f);
        LockIcon.transform.localScale = Vector3.one;
        Image lockIconImage = LockIcon.AddComponent<Image>();
        lockIconImage.preserveAspect = true;
        lockIconImage.sprite = LockIconImage;
        LockIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
          
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
        CheckIsLocked();
    }
    public void onWeaponSelect()
    {
        if (BuyToUnlock)
        {
            if(PlayerPrefs.GetInt("PackBoughted") == 0) 
            {
                GameManager.Instance.UIManager.OpenBuyPackPanel();
            }
        }
        if(WatchAdsToUnlock)
        {
            if(CheckIsLocked())
            {
                
                GameManager.Instance.AdsShower.ShowPanel(this);
                return;
            }
            
        }
        if(IsLocked) return;
        GameManager.Instance.OnChangeWeapon(this);        
    }
    public void changeColor()
    {
        if (IsLocked)
        {
            background.GetComponent<Image>().color = uimanager.LockedWeaponColor;
            weaponSprite.GetComponent<Image>().color = uimanager.LockedWeaponColor;
        }
        else
        {
            background.GetComponent<Image>().color = uimanager.UnlockedWeaponColor;
            weaponSprite.GetComponent<Image>().color = uimanager.UnlockedWeaponColor;
        }
        

    }
    public bool CheckIsLocked()
    {
        
        if (BuyToUnlock)
        {
            IsLocked = PlayerPrefs.GetInt("PackBoughted") == 0;
        }

        if (WatchAdsToUnlock)
        {           
            if (!PlayerPrefs.HasKey("IsLocked" + WeaponName))
            {
                PlayerPrefs.SetInt("IsLocked" + WeaponName, 0);
            }
            else
            {
                IsLocked = PlayerPrefs.GetInt("IsLocked" + WeaponName) < NumberOfAds;
            }           
        }
        changeColor();
        LockIcon.SetActive(IsLocked);
        return IsLocked;

    }
    
}

