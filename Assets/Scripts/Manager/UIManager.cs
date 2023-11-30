using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GridLayoutGroup weaponSelection = null;
    public WeaponTypeSlot[] weaponTypeSlots;
    public Image[] graphicsButtons = null;
    public Image[] gravityButtons = null;
    public Image[] camModeButtons = null;
    //public GameObject audioModeButton;
    public GameObject camJoystick = null;
    public GameObject movementJoystick = null;
    public GameObject ADSCam = null;
    public GameObject weaponTypeArrow = null;
    public GameObject mainMenu = null;

    public UnityEngine.UI.Button mapButtonPrefab;
    public RectTransform mapButtonContainer;

    public Color pressedButtonColor;
    public Color defaultButtonColor;
    
    private Camera mainCamera;
    private float fixedDeltaTime;


    void OnInit()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        mainCamera = Camera.main;
        LoadSetting();
    }
    private void Start()
    {        
        GameManager.Instance.reloadScene += OnInit;
        int index = 0;
        foreach (ScriptableMap map in GameManager.Instance.scriptableMaps)
        {
            int temp = index;
            UnityEngine.UI.Button mapButton = Instantiate(mapButtonPrefab, mapButtonContainer);
            mapButton.GetComponent<Image>().sprite = GameManager.Instance.scriptableMaps[temp].Background;
            mapButton.onClick.AddListener(delegate
            {
                GameManager.Instance.LoadMap(temp);
            });
            index++;
        }

        
        OnInit();
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
        foreach (var item in weaponTypeSlots)
        {
            item.gameObject.transform.Find("Background").GetComponent<Image>().color = Color.white;            
            if (item.weaponType == GameManager.Instance.currentWeaponType)
                item.gameObject.transform.Find("Background").GetComponent<Image>().color = Color.yellow;
        }
        weaponTypeArrow.gameObject.SetActive(false);
        if(GameManager.Instance.currentWeaponType != WeaponType.NONE) 
            weaponTypeArrow.SetActive(true);
    }
    
    public void onFreeCamEnable()
    {
        camJoystick.SetActive(false);
        movementJoystick.SetActive(false);
        ADSCam.SetActive(false);
        mainCamera.fieldOfView = 60;
    }
    public void onFpsCamEnable()
    {
        camJoystick.SetActive(true);
        movementJoystick.SetActive(true);
        ADSCam.SetActive(false);
        mainCamera.fieldOfView = 60;
    }
    public void onAdsCamEnable()
    {
        camJoystick.SetActive(true);
        movementJoystick.SetActive(false);
        ADSCam.SetActive(true);
        mainCamera.fieldOfView = 10;
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
        GameManager.Instance.ReloadMap();
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
    //public void onAudioButtonPress()
    //{
    //    if (PlayerPrefs.GetInt("Audio") == 0)
    //    {
    //        PlayerPrefs.SetInt("Audio", 1);

    //    }
    //    else PlayerPrefs.SetInt("Audio", 0);
    //    GameManager.Instance.LoadSetting();
    //    if (PlayerPrefs.GetInt("Audio") == 0)
    //    {
    //        audioModeButton.GetComponent<Image>().color = pressedButtonColor;
    //        audioModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound On";
    //    }
    //    else
    //    {
    //        audioModeButton.GetComponent<Image>().color = defaultButtonColor;
    //        audioModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound Off";
    //    }
    //}
    public void onBackToMenuButtonPress()
    {
        mainMenu.SetActive(true);
        GameManager.Instance.ReloadMap();
        
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
        //if(PlayerPrefs.GetInt("Audio") == 0)
        //{
        //    audioModeButton.GetComponent<Image>().color = pressedButtonColor;
        //    audioModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound On";
        //}
        //else
        //{
        //    audioModeButton.GetComponent<Image>().color = defaultButtonColor;
        //    audioModeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sound Off";
        //}
        Time.timeScale = 1f;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        onGravityButtonPress(0);
        onCammodeButtonPress(0);
    }

}
