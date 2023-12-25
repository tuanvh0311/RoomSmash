using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollController : MonoBehaviour
{
    private Collider[] maincolliders;
    public GameObject ragdoll;
    private Rigidbody mainRigidbody;
    public Animator animator;
    void Start()
    {
        getRagdollBits();
        RagdollOffMode();
        
    }
    private void Awake()
    {
        maincolliders = GetComponents<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mainRigidbody.velocity.magnitude > 1)
        {
            RagdollOnMode();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7) 
        {
            RagdollOnMode();
        }

    }
    Collider[] ragdollColliders;
    Rigidbody[] ragdollRigidbodies;
    void getRagdollBits()
    {
        ragdollColliders = ragdoll.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();
    }
    void RagdollOnMode()
    {
        foreach (var item in ragdollColliders)
        {
            item.enabled = true;
        }
        foreach (var item in ragdollRigidbodies)
        {
            item.isKinematic = false;
        }
        foreach (var item in maincolliders)
        {
            item.enabled = false;
        }
        animator.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void RagdollOffMode()
    {
        foreach (var item in ragdollColliders)
        {
            item.enabled = false;
        }
        foreach (var item in ragdollRigidbodies)
        {
            item.isKinematic = true;
        }
        foreach (var item in maincolliders)
        {
            item.enabled = true;
        }
        animator.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
