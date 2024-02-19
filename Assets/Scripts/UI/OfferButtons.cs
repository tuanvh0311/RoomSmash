using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferButtons : MonoBehaviour
{
    public UnityEngine.UI.Image ButtonImage;
    public Sprite yellow;
    public Sprite red;
    private float timer;
    public float SwtichColorDuration = 0.5f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(PlayerPrefs.GetInt("Packboughted") == 1)
        {
            ButtonImage.sprite = yellow;
        }
        else
        {
            if (timer < 0) 
            {
                timer = SwtichColorDuration;
                if (ButtonImage.sprite == red) ButtonImage.sprite = yellow;
                else ButtonImage.sprite = red;
            }
            
        }
    }

}
