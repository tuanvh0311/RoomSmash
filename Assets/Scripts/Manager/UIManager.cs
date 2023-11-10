using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    public GridLayoutGroup weaponSelection = null;
    public WeaponTypeSlot[] weaponTypeSlots;
    public Image[] graphicsButtons = null;
    public GameObject camJoystick = null;
    public GameObject movementJoystick = null;
    public GameObject ADSCam = null;
    public GameObject weaponTypeArrow = null;
    public GameObject mainMenu = null;
    
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void addWeapon(Weapon weapon)
    {
        weapon.AddSprite();
        weapon.gameObject.transform.SetParent(weaponSelection.transform, false);
        weapon.gameObject.SetActive(false);
        
    }
    public void onWeaponTypeChange()
    {
        foreach (var item in weaponTypeSlots)
        {
            item.gameObject.transform.Find("Background").GetComponent<Image>().color = Color.black;            
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
        mainCamera.fieldOfView = 30;
    }
    public void onPlayButtonPress()
    {
        mainMenu.SetActive(false);
    }
    public void onGraphicsButtonPress(int key)
    {
        foreach (var item in graphicsButtons)
        {
            item.color = Color.white;
        }
        graphicsButtons[key].color = Color.yellow;
        GameManager.Instance.SetGraphics(key);
    }
    

}
