using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuyPack : MonoBehaviour
{
    public static CheckBuyPack Instance; 
    public GameObject BuyButton;
    public GameObject SoldOutButton;

    private void Awake()
    {
        CheckBuyPack.Instance = this; 
    }
    private void OnEnable()
    {
        checkBuyPack(); 
    }
    public void checkBuyPack()
    {
        if (PlayerPrefs.GetInt("PackBoughted") == 0)
        {
            BuyButton.SetActive(true);
            SoldOutButton.SetActive(false);
        }
        else
        {
            BuyButton.SetActive(false);
            SoldOutButton.SetActive(true);
        }
    }
}
