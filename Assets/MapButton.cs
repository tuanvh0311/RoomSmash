using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public Image MapImage;
    public TextMeshProUGUI MapName;
    public bool WatchAdsToUnlock;
    public int NumberOfAds;
    public int NumberOfAdsWatched;
    public GameObject lockGO;
    public Image SelectedBackground;
    public TextMeshProUGUI AdsText;
    public string mapName;
    


    public void LoadMapData(Sprite ImageForMap, string NameForMap, bool isWatchAdsToUnlock, int nunberOfAds)
    {
        MapName.text = NameForMap;
        MapImage.sprite = ImageForMap;
        mapName = NameForMap;
        WatchAdsToUnlock = isWatchAdsToUnlock;
        NumberOfAds = nunberOfAds;

        CheckLock();
    }
    
    public void SetSelected(bool ditme)
    {
        SelectedBackground.gameObject.SetActive(ditme);
    }
    public bool CheckLock()
    {
        bool IsLocked = false;
        AdsText.text = "Watch Ads" + NumberOfAdsWatched+"/"+NumberOfAds;
        if (WatchAdsToUnlock)
        {
            if (PlayerPrefs.HasKey(mapName))
            {
                NumberOfAdsWatched = PlayerPrefs.GetInt(mapName);
            }
            else PlayerPrefs.SetInt(mapName, 0);
            if (NumberOfAds <= NumberOfAdsWatched)
            {
                IsLocked = false;
            }
            else IsLocked = true;
        }
        lockGO.SetActive(IsLocked);
        return IsLocked;
        
    }
    public void onButtonClick()
    {
        if (WatchAdsToUnlock)
        {
            //reward
            NumberOfAdsWatched++;
            PlayerPrefs.SetInt(mapName, NumberOfAdsWatched);
            CheckLock();
        }
    }
}
