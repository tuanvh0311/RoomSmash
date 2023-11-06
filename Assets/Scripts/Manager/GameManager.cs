using DestroyIt;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UIManager = null;
    public LayerMask cameraRaycast;
    public GameObject shootPos = null;
    public float cooldown = 0;
    public Light mainLight;
    public GameObject mapPrefab = null;
    public UnityAction reloadScene;

    private float holdTime;
    private Vector3 holdPosition;
    private bool startHold;
    private bool isShooting;


    public enum Mode
     
    {
        freeCam,
        fpsCam,
        adsCam
    };
    
    public Weapon[] weapons;
    public Weapon currentWeapon;
    public WeaponType currentWeaponType = WeaponType.NONE;
    List<Weapon> weaponsList = new List<Weapon>();
    public Mode mode = Mode.freeCam;
    public GameObject camJoystick = null;
    public GameObject movementJoystick = null;

    private const string SETTING_KEY = "Setting";
    private int currentSetting;
    
    void Awake()
    {
        GameManager.Instance = this;
        ReloadScene();
        currentSetting = LoadSetting();
        SetSetting(currentSetting);
        for (int i = 0; i < weapons.Length; i++)
        {
            Weapon weapon = Instantiate(weapons[i]);
            UIManager.addWeapon(weapon);
            weaponsList.Add(weapon);
            
        }
        
    }

    private int LoadSetting()
    {
        if (!PlayerPrefs.HasKey(SETTING_KEY))
        {
            SaveSetting(0);
        }
        return PlayerPrefs.GetInt(SETTING_KEY);
    }

    private void SaveSetting(int setting)
    {
        PlayerPrefs.SetInt(SETTING_KEY, setting);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (startHold)
        {
            holdTime += Time.deltaTime;
            if (currentWeapon && !EventSystem.current.IsPointerOverGameObject())
                isShooting = currentWeapon.isHoldToShoot;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            startHold = true;
            holdPosition = Input.mousePosition;          
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            startHold = false;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isShooting = (holdTime < 0.15f);
            }
            holdTime = 0f;
        }
        

        checkCamMode();
    }
    private void FixedUpdate()
    {
        if (isShooting)
        {
            CheckShooting(currentWeapon);
            isShooting = false;
        }
    }

    public void OnSettingButtonPress(int setting)
    {
        currentSetting += 1;
        if (currentSetting > 2) currentSetting = 0;
        SetSetting(currentSetting);
    }

    
    private void SetSetting(int setting)
    {

        switch (setting)
        {
            case 0:
                Screen.SetResolution(640, 360, true);
                mainLight.shadows = LightShadows.None;
                break; 
            case 1:
                Screen.SetResolution(1280, 720, true);
                mainLight.shadows = LightShadows.Hard;
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                mainLight.shadows = LightShadows.Soft;
                break;
        }
        SaveSetting(setting);
        
    }
    public void ReloadScene()
    {
        reloadScene?.Invoke();
        GameObject map = GameObject.Find("Map");
        if (map)
        {
            map.GetComponent<PoolAfter>()._timeLeft = 0;
        }
        ObjectPool.Instance.Spawn(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("#hEnvironment").transform);
       
    }
    void checkCamMode()
    {
        if (mode == Mode.freeCam)
        {
            UIManager.onFreeCamEnable();
        }
        if (mode == Mode.fpsCam)
        {
            UIManager.onFpsCamEnable();
        }
        if (mode == Mode.adsCam)
        {
            UIManager.onAdsCamEnable();
        }
    }
    public void checkWeaponType(WeaponType weaponType)
    {
        currentWeaponType = currentWeaponType == weaponType ? WeaponType.NONE :  weaponType;
        UIManager.onWeaponTypeChange();
        foreach (var weapon in weaponsList)
        {
            if (weapon.weaponType == currentWeaponType)
                weapon.showWeapon();
            else weapon.hideWeapon();
        }

    }
    private void CheckShooting(Weapon weapon)
    {

        if (!weapon) return;

        if(mode == Mode.fpsCam || mode == Mode.freeCam)
        {
            //RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            weapon.Shoot(ray.direction, shootPos);

        }
        else
        {
            weapon.Shoot(Camera.main.transform.forward, shootPos);
        }
        
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(ray.origin, ray.direction * 100f);
        
        
    }
}
