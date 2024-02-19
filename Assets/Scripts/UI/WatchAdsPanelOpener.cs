
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WatchAdsPanelOpener : MonoBehaviour
{
    private Weapon currentWeapon;
    public CustomUI.Button customButton;
    public Image WeaponIcon;
    public TextMeshProUGUI tmp;
    public string WeaponName;
    public int NumberOfAdsToUnlock;
    public void LoadPanelData(Weapon weapon) 
    {
        currentWeapon = weapon;
        WeaponIcon.sprite = weapon.GetWeaponIcon();
        WeaponName = weapon.GetWeaponName();
        NumberOfAdsToUnlock = weapon.GetNumberOfAds();

        //tmp.text = "Watch " + PlayerPrefs.GetInt("IsLocked" + WeaponName) + "/" + NumberOfAdsToUnlock + " Ads to unlock weapon";
        tmp.text = $"Watch <color=blue>{PlayerPrefs.GetInt("IsLocked" + WeaponName)}/{NumberOfAdsToUnlock}</color> Ads to unlock weapon";
    }
    public void OnWatchAdsButtonClick()
    {
        UnityEvent e = new UnityEvent();
        e.AddListener(() => {
            //xem quang cao thanh cong thuong gi do
            int NumberOfAdsWatched = PlayerPrefs.GetInt("IsLocked" + WeaponName) + 1;
            PlayerPrefs.SetInt("IsLocked" + WeaponName, NumberOfAdsWatched);
            customButton.onToggleTweeningPanelAndDisableShoot();
            currentWeapon.CheckIsLocked();
            //logevent
            //SkygoBridge.instance.LogEvent("reward_openweapon");
        });
        //reward
        //ApplovinBridge.instance.ShowRewarAdsApplovin(e, null);
    }
    public void ShowPanel(Weapon weapon)
    {
        LoadPanelData(weapon);
        customButton.onToggleTweeningPanelAndDisableShoot();
    }
}
