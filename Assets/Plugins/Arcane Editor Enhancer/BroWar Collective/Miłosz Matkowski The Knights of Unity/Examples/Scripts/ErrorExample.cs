using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorExample : MonoBehaviour
{
    [SerializeField] BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box.gameObject.SetActive(false);
        Debug.Log(box);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
