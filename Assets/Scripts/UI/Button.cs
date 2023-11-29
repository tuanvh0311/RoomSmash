using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Image background;
    public GameObject panel;
    private bool isToggle = false;
    private Color originalColor;

    public bool isReverse = false;
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.reloadScene += OnInit;
        originalColor = background.GetComponent<Image>().color;
        OnInit();
    }
    void OnInit()
    {
        if(!isReverse)
        {
            isToggle = false;
            background.color = originalColor;
            if (panel)
            panel.SetActive(false);
        }
        else
        {
            isToggle = false;
            background.color = originalColor;
            if (panel)
            panel.SetActive(true);
        }
        
    }
    public void onToggleAndPanel()
    {
        if (!isToggle)
        {
            isToggle = true;
            background.color = Color.yellow;
            panel.SetActive(true);
        }
        else
        {
            isToggle = false;
            background.color = originalColor;
            panel.SetActive(false);
        }
    }
    public void onToggleAndPanelAndDisableShoot()
    {
        if (!isToggle)
        {
            isToggle = true;
            background.color = Color.yellow;
            panel.SetActive(true);
            GameManager.Instance.disableShootTimer = 1000000000f;
        }
        else
        {
            isToggle = false;
            background.color = originalColor;
            panel.SetActive(false);
            GameManager.Instance.disableShootTimer = Time.deltaTime * 2f;
        }
    }
    public void onToggleAndPanelReverse()
    {
        if (!isToggle)
        {
            isToggle = true;
            background.color = Color.yellow;
            panel.SetActive(false);
        }
        else
        {
            isToggle = false;
            background.color = originalColor;
            panel.SetActive(true);
        }
    }
    public void onToggle()
    {
        if (isToggle)
        {
            isToggle = false;
            background.color = originalColor;            
        }
        else
        {
            isToggle = true;
            background.color = Color.yellow;           
        }
    }
}
