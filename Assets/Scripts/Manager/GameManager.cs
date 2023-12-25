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
    public Detonator detonator;
    public LayerMask shootLayerMask;
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
    private int currentGraphics;
    private float currentAudioRatio;
    public float disableShootTimer = 0f;
    public static int currentMapIndex = 0;
    private float originWidth = Screen.width;
    private float originHeight = Screen.height;
    private float widthOnHeight;
    void Awake()
    {
        GameManager.Instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30000;
        widthOnHeight = (float)Display.main.renderingWidth / (float)Display.main.renderingHeight;
        //if (PlayerPrefs.HasKey(GRAPHICS_KEY))
        //{
        //    float mul = 1f;
        //    switch (PlayerPrefs.GetInt(GRAPHICS_KEY))
        //    {
        //        case 0:
        //            mul = 2f;
        //            break;
        //        case 1:
        //            mul = 1.5f;
        //            break;
        //        case 2:
        //            mul = 1f;
        //            break;
        //    }
        //    originHeight = originHeight * mul;
        //    originWidth = originWidth * mul;
        //    
        //}       
        for (int i = 0; i < weapons.Length; i++)
        {
            Weapon weapon = Instantiate(weapons[i]);
            UIManager.addWeapon(weapon);
            weaponsList.Add(weapon);
            
        }
        LoadMap(currentMapIndex);
    }
    private void Start()
    {
        LoadSetting();
    }


    private int LoadGraphics()
    {
        if (!PlayerPrefs.HasKey(GRAPHICS_KEY))
        {
            SaveSetting(0, currentAudioRatio);
        }       
        return PlayerPrefs.GetInt(GRAPHICS_KEY);
    }
    private float LoadAudioMode()
    {
        if(!PlayerPrefs.HasKey(AUDIO_KEY))
        {
            SaveSetting(currentGraphics, 1);
        }
        return PlayerPrefs.GetFloat(AUDIO_KEY);
    }
    public void LoadSetting()
    {
        currentGraphics = LoadGraphics();
        currentAudioRatio = LoadAudioMode();
        SetGraphics(LoadGraphics());
    }
    private void SaveSetting(int graphics, float audio)
    {
        PlayerPrefs.SetInt(GRAPHICS_KEY, graphics);
        PlayerPrefs.SetFloat(AUDIO_KEY, audio);
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
        if (Input.GetMouseButtonDown(0) && !Cache.IsPointerOverUIObject())
        {
            startHold = true;
            holdPosition = Input.mousePosition;          
        }
        
        if (Input.GetMouseButtonUp(0) && startHold)
        {         
            if (disableShootTimer <= 0)
            {
                if (currentWeapon)
                {
                    if(!currentWeapon.isHoldToShoot)
                    isShooting = (holdTime < 0.15f);
                }
                
            }                                 
            startHold = false;
            holdTime = 0f;
        }
        if (!isShooting)
        {
            foreach (ParticleSystem child in shootPos.GetComponentsInChildren<ParticleSystem>())
                child.Stop();
            foreach (LineRenderer child in shootPos.GetComponentsInChildren<LineRenderer>())
                child.enabled = false;
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
            currentWeapon.background.GetComponent<Image>().color = Color.white;
            cooldown = 0;
            currentWeapon = weapon;
            weapon.background.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            currentWeapon.background.GetComponent<Image>().color = Color.white;
            currentWeapon = null;
        }
    }
    
    public void SetGraphics(int setting)
    {
        switch (setting)
        {
            case 0:
                Screen.SetResolution(960, (int) (960/widthOnHeight), true);
                mainLight.shadows = LightShadows.None;
                break;
            case 1:
                Screen.SetResolution(1280, (int)(1280 / widthOnHeight), true);
                mainLight.shadows = LightShadows.Hard;
                break;
            case 2:
                Screen.SetResolution(1920, (int)(1920 / widthOnHeight), true);
                mainLight.shadows = LightShadows.Soft;
                break;
        }
        SaveSetting(setting, currentAudioRatio);
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
        currentWeapon.background.GetComponent<Image>().color = Color.white;
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
            currentWeapon.background.GetComponent<Image>().color = Color.white;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, shootLayerMask))
            {
                shootPos.transform.LookAt(hit.point);
                weapon.Shoot(shootPos.transform.forward, shootPos, map.transform);
            }
            else
                weapon.Shoot(ray.direction, shootPos, map.transform);

        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, shootLayerMask))
            {
                shootPos.transform.LookAt(hit.point);
                Debug.Log(hit.transform.name);
                weapon.Shoot(shootPos.transform.forward, shootPos, map.transform);
            }
            else
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
