using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
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
            if(background)
            originalColor = background.GetComponent<Image>().color;
            OnInit();
        }
        public void OnInit()
        {
            if (!isReverse)
            {
                isToggle = false;
                if (background)
                    background.color = originalColor;
                if (panel) 
                {
                    if (panel.GetComponentInChildren<ButtonsTweener>())
                    {
                        panel.GetComponentInChildren<ButtonsTweener>().OnClose().OnComplete(() => {
                            panel.SetActive(false);
                        });
                    }
                    else if (panel.GetComponentInChildren<PanelTweener>())
                    {
                        panel.GetComponentInChildren<PanelTweener>().OnClose().OnComplete(() => {
                            panel.SetActive(false);
                        });
                    }
                    else panel.SetActive(false);

                }
                    
            }
            else
            {
                isToggle = false;
                if (background)
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
                if (background)
                    background.color = Color.yellow;
                panel.SetActive(true);
            }
            else
            {
                isToggle = false;
                if (background)
                    background.color = originalColor;
                panel.SetActive(false);
            }
        }
        public void onToggleAndPanelAndDisableShoot()
        {

            if (!isToggle)
            {
                isToggle = true;
                if (background)
                    background.color = Color.yellow;
                panel.SetActive(true);
                GameManager.Instance.disableShootTimer = 1000000000f;
            }
            else
            {
                isToggle = false;
                if (background)
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
                if (background)
                    background.color = Color.yellow;
                panel.SetActive(false);
            }
            else
            {
                isToggle = false;
                if (background)
                    background.color = originalColor;
                panel.SetActive(true);
            }
        }
        public void onToggle()
        {
            if (isToggle)
            {
                isToggle = false;
                if (background)
                    background.color = originalColor;
            }
            else
            {
                isToggle = true;
                if (background)
                    background.color = Color.yellow;
            }
        }
        public void onToggleTweeningPanelAndDisableShoot()
        {
            if (!isToggle)
            {
                isToggle = true;
                if (background)
                    background.color = Color.yellow;
                panel.SetActive(true);
                
            }
            else
            {
                isToggle = false;
                GameManager.Instance.disableShootTimer = Time.deltaTime * 2f;
                if (background)
                    background.color = originalColor;
                    panel.GetComponentInChildren<PanelTweener>().OnClose().OnComplete(() => {                 
                    panel.SetActive(false);           
                });
                
            }
        }

        public void onToggleTweeningButtonListAndDisableShoot()
        {
            if (!isToggle)
            {
                
                isToggle = true;
                if (background)
                    background.color = Color.yellow;
                panel.SetActive(true); 
            }
            else
            {
                GameManager.Instance.disableShootTimer = Time.deltaTime * 2f;
                isToggle = false;
                if (background)
                    background.color = originalColor;
                panel.GetComponentInChildren<ButtonsTweener>().OnClose().OnComplete(() => {
                    panel.SetActive(false);                  
                });

            }
        }
        public void onInterButtonClick()
        {
            
                //inter 
                //ApplovinBridge.instance.ShowInterAdsApplovin(null);
            
        }
        public void onLogEventButtonClick()
        {
            //logevent 
            Debug.Log(gameObject.name);
            //SkygoBridge.instance.LogEvent("Use_"+ gameObject.name);
        }
    }
}

