using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeGO : MonoBehaviour
{
    bool isShaking = false;
    int shakeCount = 0;
    float shakeTimer = 0f;
    float shakePerSec = 0.5f;
    public LayerMask layerMask;



    // Update is called once per frame
    void FixedUpdate()
    {
        shakeTimer -= Time.deltaTime;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 500f, layerMask);
        if(isShaking && shakeTimer <= 0)
        {
            shakeTimer = shakePerSec;
            if (shakeCount > 0)
            {
                float mass = 1;
                foreach (var hitCollider in hitColliders)
                {
                    Rigidbody rb = Cache.GetRigidbodyFromCollider(hitCollider);
                    Vector3 randomForce = new Vector3(Random.Range(-5f, 5f),
                                                            Random.Range(-2f, 3f),
                                                            Random.Range(-5f, 5f));
                    if (rb)
                    {
                        mass = rb.mass < 0 ? 1 : rb.mass;

                        rb.AddForce(randomForce * 80 / mass, ForceMode.Acceleration);
                        rb.AddTorque(randomForce);
                    }
                    
                }
                shakeCount--;
                
            }
            else
            {
                isShaking = false;
            }
        }

        
        
    }
    public void startShaking()
    {
        isShaking = true;
        shakeCount = 10;
        CameraController.Instance.startShakeCamera(shakeCount * shakePerSec, 0.5f);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 500f);
    }
}
