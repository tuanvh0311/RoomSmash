using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public GameObject camPos = null;
    public GameObject shootPos = null;
    private Vector3 m_v3MousePosition;
    public float MouseSpeed = 0.1f;
    public Joystick cameraJoystick;
    private Vector3 originalCamPos;
    private float m_cameraSpeed;
    private float shakeTimer = 0f;
    private float shakeForce = 0f;
    private float shakeDelay = 0f;
    private bool isShaking = false;

    Quaternion rot;

    private Camera m_Camera;
    void Awake()
    {
        GameManager.Instance.reloadScene += OnInit;
        originalCamPos = camPos.transform.localPosition;
        OnInit();
    }

    void OnInit()
    {
        CameraController.Instance = this;
        shakeTimer = 0f;
        shakeForce = 0f;
        isShaking = false;
        camPos.transform.localPosition = originalCamPos;
        camPos.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_Camera = Camera.main;
        rot = camPos.transform.rotation;
        m_Camera.transform.position = camPos.transform.position;
    }
    public void startShakeCamera(float duration, float startForce)
    {
        duration -= 0.5f;
        isShaking = true;
        if(shakeTimer < duration)
            shakeTimer = duration;
        if(shakeForce < startForce) 
            shakeForce = startForce;
    }
    

    public void shakeCamera(float time)
    {
        shakeTimer -= time;
        shakeDelay -= time;
        if (isShaking && shakeTimer > 0f)
        {
            shakeForce += time / 5;
            if (shakeForce > 1f) shakeForce = 1.25f;
            if (shakeDelay <= 0f)
            {
                shakeDelay = Time.deltaTime;
                camPos.transform.position = originalCamPos + new Vector3(Random.Range(-1f, 1f),
                                                            Random.Range(-0.3f, 0.3f),
                                                            Random.Range(-1f, 1f)) * shakeForce;
            }
        }
        else
        {
            shakeForce -= time;
            if (shakeForce < 0f)
            {
                camPos.transform.position = originalCamPos;
                shakeForce = 0f;
                isShaking = false;
            }
            if (isShaking && shakeDelay <= 0f)
            {
                shakeDelay = Time.deltaTime;
                camPos.transform.position = originalCamPos + new Vector3(Random.Range(-1f, 1f),
                                                    Random.Range(-0.3f, 0.3f),
                                                    Random.Range(-1f, 1f)) * shakeForce;
            }


        }
    }
    public void shakeCamera2(float time)
    {     
        shakeTimer -= time;
        shakeDelay -= time;
        float lerpDuration = time * 2f;
        if (isShaking && shakeTimer > 0f)
        {
            shakeForce += time / 5;
            if (shakeForce > 1.25f) shakeForce = 1.25f;
            if (shakeDelay <= 0f)
            {
                shakeDelay = Time.deltaTime;
                Vector3 gotoPos = originalCamPos + (new Vector3(Random.Range(-1f, 1f),
                                                    Random.Range(-0.3f, 0.3f),
                                                    Random.Range(-1f, 1f)) * shakeForce * 3f);
                camPos.transform.localPosition = Vector3.Lerp(camPos.transform.localPosition, gotoPos, lerpDuration);
                
            }
        }      
        else
        {
            shakeForce -= time;
            if (shakeForce < 0f)
            {
                shakeForce = 0f;
                camPos.transform.localPosition = Vector3.Lerp(camPos.transform.localPosition, originalCamPos, lerpDuration);
                isShaking = false;
            }
            if (isShaking && shakeDelay <= 0f)
            {
                shakeDelay = Time.deltaTime;
                Vector3 gotoPos = originalCamPos + (new Vector3(Random.Range(-1f, 1f),
                                                    Random.Range(-0.3f, 0.3f),
                                                    Random.Range(-1f, 1f)) * shakeForce * 3f);
                camPos.transform.localPosition = Vector3.Lerp(camPos.transform.localPosition, gotoPos, lerpDuration);
            }
                       
        }
    }

    void Update()
    {

        m_Camera.transform.position = camPos.transform.position;
        m_Camera.transform.rotation = camPos.transform.rotation;    
        
        if (GameManager.Instance.mode == GameManager.Mode.freeCam) 
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                rot.x += (Input.mousePosition.y - m_v3MousePosition.y) * MouseSpeed;
                rot.y += (Input.mousePosition.x - m_v3MousePosition.x) * MouseSpeed;
                rot.x = Mathf.Clamp(rot.x, -90f, 90);
                camPos.transform.rotation = Quaternion.Euler(-rot.x, rot.y, 0.0f);                
            }
        } 
        if( GameManager.Instance.mode == GameManager.Mode.fpsCam || GameManager.Instance.mode == GameManager.Mode.adsCam)
        {
            switch (GameManager.Instance.mode)
            {
                case GameManager.Mode.adsCam:
                    m_cameraSpeed = 0.3f;
                    break;
                case GameManager.Mode.fpsCam:
                    m_cameraSpeed = 1f;
                    break;
            }
            rot.x += cameraJoystick.Vertical * m_cameraSpeed;
            rot.y += cameraJoystick.Horizontal * m_cameraSpeed;
            rot.x = Mathf.Clamp(rot.x, -90f, 90);
            camPos.transform.rotation = Quaternion.Euler(-rot.x, rot.y, 0.0f);
        }
        m_v3MousePosition = Input.mousePosition;
    }
    private void FixedUpdate()
    {
        
        shakeCamera2(Time.fixedDeltaTime);
    }
}
