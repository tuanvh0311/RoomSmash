using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject stickObject;
    public BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }
    public virtual void release()
    {
        SetKinematic(false);
        setParent(GameManager.Instance.map.transform);
        stickObject = null;
    }
    public void SetKinematic(bool value)
    {
        rb.isKinematic = value;
    }
    public void setParent(Transform parent)
    {
        transform.SetParent(parent);
        
    }
}
