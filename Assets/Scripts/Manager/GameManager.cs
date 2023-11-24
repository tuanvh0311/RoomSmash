using DestroyIt;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UIManager = null;
    public LayerMask cameraRaycast;
    public GameObject shootPos = null;
    public float cooldown = 0;
    public Light mainLight;
    public ScriptableMap[] scriptableMaps;
    public UnityAction reloadScene;
    public GameObject map;

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
    public List<PoolAfter> remainObject = new List<PoolAfter>();

    private const string GRAPHICS_KEY = "Graphics";
    private const string AUDIO_KEY = "Audio";
    private int currentGraphics = 0;
    private int currentAudioMode = 0;
    public float disableShootTimer = 0f;
    private int currentMapIndex;
    void Awake()
    {
        GameManager.Instance = this;
        // Disable vSync

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30000;
        LoadSetting();
        for (int i = 0; i < weapons.Length; i++)
        {
            Weapon weapon = Instantiate(weapons[i]);
            UIManager.addWeapon(weapon);
            weaponsList.Add(weapon);
            
        }
        LoadMap(0);
    }

    

    private int LoadGraphics()
    {
        if (!PlayerPrefs.HasKey(GRAPHICS_KEY))
        {
            SaveSetting(0, currentAudioMode);
        }
        return PlayerPrefs.GetInt(GRAPHICS_KEY);
    }
    private int LoadAudioMode()
    {
        if(!PlayerPrefs.HasKey(AUDIO_KEY))
        {
            SaveSetting(currentGraphics, 0);
        }
        return PlayerPrefs.GetInt(AUDIO_KEY);
    }
    public void LoadSetting()
    {

        currentGraphics = LoadGraphics();
        currentAudioMode =LoadAudioMode();
        SetGraphics(currentGraphics);
    }
    private void SaveSetting(int graphics, int audio)
    {
        PlayerPrefs.SetInt(GRAPHICS_KEY, graphics);
        PlayerPrefs.SetInt(AUDIO_KEY, audio);
    }

    // Update is called once per frame
    void Update()
    {
        disableShootTimer -= Time.deltaTime;
        cooldown -= Time.deltaTime;
        if (startHold)
        {
            holdTime += Time.deltaTime;
            if (currentWeapon && !Cache.IsPointerOverUIObject() && disableShootTimer <= 0)
                isShooting = currentWeapon.isHoldToShoot;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            startHold = true;
            holdPosition = Input.mousePosition;          
        }
        
        if (Input.GetMouseButtonUp(0) )
        {
            startHold = false;
            if (!Cache.IsPointerOverUIObject() && disableShootTimer <= 0)
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


    public void OnChangeWeapon(Weapon weapon)
    {
        if(currentWeapon != weapon)
        {
            if(currentWeapon)
            currentWeapon.background.GetComponent<Image>().color = Color.black;
            cooldown = 0;
            currentWeapon = weapon;
            weapon.background.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            currentWeapon.background.GetComponent<Image>().color = Color.black;
            currentWeapon = null;
        }
    }
    
    public void SetGraphics(int setting)
    {
        float widthOnHeight = Screen.width/Screen.height;
        switch (setting)
        {
            case 0:
                Screen.SetResolution(640, (int)(640 / widthOnHeight), true);
                mainLight.shadows = LightShadows.None;
                break; 
            case 1:
                Screen.SetResolution(1280, (int)(1280/ widthOnHeight), true);
                mainLight.shadows = LightShadows.Hard;
                break;
            case 2:
                Screen.SetResolution(1920, (int)(1920 / widthOnHeight), true);
                mainLight.shadows = LightShadows.Soft;
                break;
        }
        SaveSetting(setting, currentAudioMode);
        
    }

    
    public void LoadMap(int mapIndex)
    {
        reloadScene?.Invoke();
        foreach (var item in remainObject)
        {
            item.timeLeft = 0f;
        }
        remainObject.Clear();
        if (map)           
            map.GetComponent<PoolAfter>().timeLeft = 0;
        map = ObjectPool.Instance.Spawn(scriptableMaps[mapIndex].MapPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("#hEnvironment").transform);
        currentMapIndex = mapIndex;
        mode = Mode.freeCam;
        if(currentWeapon)
        currentWeapon.background.GetComponent<Image>().color = Color.black;
        currentWeapon = null;
        currentWeaponType = WeaponType.NONE;
        disableShootTimer = 0;
        checkWeaponType(currentWeaponType);   
    }
    public void ReloadMap()
    {
        reloadScene?.Invoke();
        foreach (var item in remainObject)
        {
            item.timeLeft = 0f;
        }
        remainObject.Clear();
        if (map)
        {
            map.GetComponent<PoolAfter>().timeLeft = 0;
        }
        map = ObjectPool.Instance.Spawn(scriptableMaps[currentMapIndex].MapPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("#hEnvironment").transform);
        mode = Mode.freeCam;
        if (currentWeapon)
            currentWeapon.background.GetComponent<Image>().color = Color.black;
        currentWeapon = null;
        currentWeaponType = WeaponType.NONE;
        disableShootTimer = 0;
        checkWeaponType(currentWeaponType);
    }
    void checkCamMode()
    {
        switch (mode)
        {
            case Mode.freeCam:
                UIManager.onFreeCamEnable();
                break;
            case Mode.fpsCam:
                UIManager.onFpsCamEnable();
                break;
            case Mode.adsCam:
                UIManager.onAdsCamEnable();
                break;
        }
    }
    public void changeCamMode(int camMode)
    {
        switch(camMode) 
        {
            case 0:
                mode = Mode.freeCam;
                break;
            case 1:
                mode = Mode.fpsCam;
                break;
            case 2:
                mode = Mode.adsCam;
                break;
        }
        checkCamMode();
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
            weapon.Shoot(ray.direction, shootPos, map.transform);

        }
        else
        {
            weapon.Shoot(Camera.main.transform.forward, shootPos, map.transform);
        }
        
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(ray.origin, ray.direction * 100f);
        
        
    }
}
