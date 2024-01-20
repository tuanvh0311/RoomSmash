using API.Sound;
using Cinemachine;
using DG.Tweening;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GridLayoutGroup weaponSelection;
    public Sprite LockIcon;
    public Sprite WatchAdsIcon;
    public WeaponTypeSlot[] weaponTypeSlots;
    public Image[] graphicsButtons;
    public Image[] gravityButtons;
    public Image[] camModeButtons;
    public Slider audioSlider;
    public GameObject camJoystick;
    public GameObject movementJoystick;
    public GameObject ADSCam;
    public GameObject mainMenu;
    public CinemachineVirtualCamera MainCinemachineCamera;
    public GameObject BackgroundCameraRenderer;
    public Button mapButtonPrefab;
    public Button ToggleBuyPackPanel;
    public GameObject RemoveAdsButton;
    public RectTransform mapButtonContainer;
    public Color pressedButtonColor;
    public Color defaultButtonColor; 
    public Color LockedWeaponColor;
    public Color UnlockedWeaponColor;
    private float fixedDeltaTime;
    public List<MapButton> MapButtons = new List<MapButton>();

    void OnInit()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        LoadSetting();
    }
    private void Start()
    {        
        GameManager.Instance.reloadScene += OnInit;
        int index = 0;
        foreach (ScriptableMap map in GameManager.Instance.scriptableMaps)
        {
            int temp = index;
            Button mapButton = Instantiate(mapButtonPrefab, mapButtonContainer);
            mapButton.name = map.MapName;
            mapButton.GetComponent<MapButton>().LoadMapData(map.Background, map.MapName, map.WatchAdsToUnlock,map.NumberOfAds);
            MapButtons.Add(mapButton.GetComponent<MapButton>());
            mapButton.onClick.AddListener(delegate
            {
                if (mapButton.GetComponent<MapButton>().CheckLock())
                {
                    mapButton.GetComponent<MapButton>().onButtonClick();
                }
                else 
                {
                    GameManager.Instance.LoadMap(temp);                 
                } 
                                 
            });
            index++;
            
        }
        RemoveAdsButton.SetActive(PlayerPrefs.GetInt("AdsRemove") == 0);
        
        OnInit();
    }
    
    private void Awake()
    {
        MainCinemachineCamera.m_Lens.FarClipPlane = 0.02f;
    }
    public void addWeapon(Weapon weapon)
    {
        weapon.AddSprite();
        weapon.gameObject.transform.SetParent(weaponSelection.transform, false);
        weapon.gameObject.SetActive(false);
        
    }
    public void onTimeButtonPress()
    {
        if(Time.timeScale == 1f)
            Time.timeScale = 0.1f;
        else Time.timeScale = 1f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
    public void onGravityButtonPress(int key)
    {
        foreach(Image image in gravityButtons)
        {
            image.color = defaultButtonColor;
        }
        gravityButtons[key].color = pressedButtonColor;
        Rigidbody[] rbs = GameManager.Instance.map.GetComponentsInChildren<Rigidbody>();
        switch (key)
        {
            case 0:
                foreach (Rigidbody rb in rbs)
                {
                    rb.AddForce(Vector3.down * 3f, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f)));
                }
                Physics.gravity = new Vector3 (0, -20f, 0);
                break;
            case 1:
                foreach(Rigidbody rb in rbs)
                {
                    rb.velocity = rb.velocity.normalized * 3;
                    rb.AddForce(Vector3.up * 0.2f, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-10f, 10f), 
                                                    Random.Range(-10f, 10f), 
                                                    Random.Range(-10f, 10f)));
                }
                Physics.gravity = new Vector3(0f, 0f, 0f);
                break;
            case 2:       
                foreach (Rigidbody rb in rbs)
                {
                    rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f)));
                }
                Physics.gravity = new Vector3(0, 20f, 0);
                break;
            case 3:
                foreach (Rigidbody rb in rbs)
                {
                    rb.AddForce(Vector3.left * 3f, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f)));
                }
                Physics.gravity = new Vector3(-20f, 0, 0);
                break;
            case 4:
                foreach (Rigidbody rb in rbs)
                {
                    rb.AddForce(Vector3.right * 3f, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f),
                                                    Random.Range(-10f, 10f)));
                }
                Physics.gravity = new Vector3(20f, 0, 0);
                break;
        }
    }
    public void onWeaponTypeChange()
    {
        WeaponType currentWeaponType = GameManager.Instance.currentWeaponType;
        foreach (var item in weaponTypeSlots)
        {
            item.gameObject.transform.Find("Background").GetComponent<Image>().color = Color.white;
            if (item.weaponType == currentWeaponType)
                item.gameObject.transform.Find("Background").GetComponent<Image>().color = Color.yellow;
        }       
        if(currentWeaponType != WeaponType.NONE)          
        {
            weaponSelection.transform.parent.gameObject.SetActive(true);
            foreach (var weapon in GameManager.Instance.GetWeapons())
            {
                if (weapon.weaponType == currentWeaponType)
                {
                    weapon.showWeapon();
                    weapon.CheckIsLocked();
                }
                else weapon.hideWeapon();
            }
        }
        else
        {
            weaponSelection.transform.parent.GetComponent<ButtonsTweener>().OnClose().OnComplete(() => {
                weaponSelection.transform.parent.gameObject.SetActive(false);
                foreach (var weapon in GameManager.Instance.GetWeapons())
                {
                    if (weapon.weaponType == currentWeaponType)
                    {
                        weapon.showWeapon();
                        weapon.CheckIsLocked();
                    }
                    else weapon.hideWeapon();
                }
            });
            
        }
    }
    public void onMapSelected(int index)
    {       
        for (int i = 0; i < MapButtons.Count; i++)
        {
            MapButtons[i].SetSelected(i == index);
            MapButtons[i].CheckLock();
        }
    }
    public void checkMap()
    {
        for (int i = 0; i < MapButtons.Count; i++)
        {
            MapButtons[i].CheckLock();
        }
    }
    public void onFreeCamEnable()
    {
        camJoystick.SetActive(false);
        movementJoystick.SetActive(false);
        ADSCam.SetActive(false);
        MainCinemachineCamera.m_Lens.FieldOfView = 60;
        foreach (var item in camModeButtons)
        {
            item.color = defaultButtonColor;
        }
        int mode = ((int)GameManager.Instance.mode);
        camModeButtons[mode].color = pressedButtonColor;
    }
    public void onFpsCamEnable()
    {
        camJoystick.SetActive(true);
        movementJoystick.SetActive(true);
        ADSCam.SetActive(false);
        MainCinemachineCamera.m_Lens.FieldOfView = 60;
        foreach (var item in camModeButtons)
        {
            item.color = defaultButtonColor;
        }
        int mode = ((int)GameManager.Instance.mode);
        camModeButtons[mode].color = pressedButtonColor;
    }
    public void onAdsCamEnable()
    {
        camJoystick.SetActive(true);
        movementJoystick.SetActive(false);
        ADSCam.SetActive(true);
        MainCinemachineCamera.m_Lens.FieldOfView = 10;
        foreach (var item in camModeButtons)
        {
            item.color = defaultButtonColor;
        }
        int mode = ((int)GameManager.Instance.mode);
        camModeButtons[mode].color = pressedButtonColor;
    }

    public void onCammodeButtonPress(int key) 
    {
        GameManager.Instance.changeCamMode(key);
        foreach (var item in camModeButtons)
        {
            item.color = defaultButtonColor;
        }
        int mode = ((int)GameManager.Instance.mode);
        camModeButtons[mode].color = pressedButtonColor;
    }
    public void onPlayButtonPress()
    {
        mainMenu.SetActive(false);      
        BackgroundCameraRenderer.SetActive(false);
        MainCinemachineCamera.m_Lens.FarClipPlane = 100f;
    }
    public void onGraphicsButtonPress(int key)
    {
        PlayerPrefs.SetInt("Graphics", key);
        GameManager.Instance.LoadSetting();
        Color imageColor;
        foreach (var item in graphicsButtons)
        {
            imageColor = item.color;
            imageColor.a = 0f;
            item.color = imageColor;
        }
        imageColor = graphicsButtons[PlayerPrefs.GetInt("Graphics")].color;
        imageColor.a = 1f;
        graphicsButtons[PlayerPrefs.GetInt("Graphics")].color = imageColor;
    }
    public void onAudioChange()
    {
        PlayerPrefs.SetFloat("Audio", audioSlider.value);
        SoundManager.Ins.onAudioChange();
    }
    public void onBackToMenuButtonPress()
    {
        //inter
        mainMenu.SetActive(true);
        GameManager.Instance.ReloadMapNoAds();
        BackgroundCameraRenderer.SetActive(true);
        MainCinemachineCamera.m_Lens.FarClipPlane = 0.02f;
        //
    }
    private void LoadSetting()
    {
        GameManager.Instance.LoadSetting();
        Color imageColor;
        foreach (var item in graphicsButtons)
        {
            imageColor = item.color;
            imageColor.a = 0f;
            item.color = imageColor;
        }
        imageColor = graphicsButtons[PlayerPrefs.GetInt("Graphics")].color;
        imageColor.a = 1f;
        graphicsButtons[PlayerPrefs.GetInt("Graphics")].color = imageColor;
        audioSlider.value = PlayerPrefs.GetFloat("Audio");
        SoundManager.Ins.onAudioChange();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        onGravityButtonPress(0);
        onCammodeButtonPress(0);
    }
    public void BuyPack()
    {
        //purchase
        PlayerPrefs.SetInt("PackBoughted", 1);
        GameManager.Instance.checkCurrentWeaponType();
        CheckBuyPack.Instance.checkBuyPack();
    }
    public void BuyAdsRemove()
    {
        //purchase
        PlayerPrefs.SetInt("AdsRemove", 1);
        GameManager.Instance.checkCurrentWeaponType();
        RemoveAdsButton.SetActive(false);
    }
    public void OpenBuyPackPanel()
    {
        ToggleBuyPackPanel.onClick.Invoke();
    }

}
