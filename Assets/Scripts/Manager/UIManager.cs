using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GridLayoutGroup weaponSelection = null;
    public GameObject camJoystick = null;
    public GameObject movementJoystick = null;
    public GameObject ADSCam = null;
    public GameObject weaponTypeArrow = null;

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

}
