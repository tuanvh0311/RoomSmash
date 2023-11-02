using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    GameObject stickObject;
    BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stickObject)
        rb.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }
    private void SetKinematic(bool value)
    {
        rb.isKinematic = value;
        col.isTrigger = value;
    }
    public void setParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    public void release()
    {
        Debug.Log("release");
        SetKinematic(false );
        setParent(null);
        col.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rb == null) return;
        if (rb.isKinematic == true) return;
        if (stickObject) return;
                
        stickObject = collision.gameObject;
        SetKinematic(true);
        setParent(collision.transform);
        col.enabled = false;
    }



}
