using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Detonator : MonoBehaviour
{
    public List<GameObject> explosives = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.reloadScene += OnInit;
        
    }
    void OnInit()
    {
        gameObject.SetActive(false);
        explosives.Clear();

    }
    public void ExploseAllExplosives()
    {
        foreach (var item in explosives)
        {
            item.GetComponent<Grenade>().Explode();
        }
        explosives.Clear();
        gameObject.SetActive(false);
        GameManager.Instance.disableShootTimer = Time.deltaTime * 2f;
    }
    
}
