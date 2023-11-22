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
    private float length = 0f;
    Quaternion rot;

    private Camera m_Camera;
    void Start()
    {
        GameManager.Instance.reloadScene += OnInit;
        OnInit();
    }

    void OnInit()
    {
        CameraController.Instance = this;
        originalCamPos = camPos.transform.position;
        camPos.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_Camera = Camera.main;
        rot = camPos.transform.rotation;
        m_Camera.transform.position = camPos.transform.position;
    }
    public void shakeCamera(float duration)
    {
        duration -= 1f;
        isShaking = true;
        if(shakeTimer < duration)
        shakeTimer = duration;
        
    }

    void Update()
    {

        m_Camera.transform.position = camPos.transform.position;
        m_Camera.transform.rotation = camPos.transform.rotation;
        shootPos.transform.rotation = camPos.transform.rotation;      
        
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
        shakeTimer -= Time.fixedDeltaTime;
        shakeDelay -= Time.fixedDeltaTime;
        if (isShaking && shakeTimer > 0f)
        {
            shakeForce += Time.fixedDeltaTime / 5;
            if (shakeForce > 1f) shakeForce = 1.25f;
            if (shakeDelay <= 0f)
            {
                shakeDelay = Time.fixedDeltaTime * 2f;
                camPos.transform.position = originalCamPos + new Vector3(Random.Range(-1, 1),
                                                            Random.Range(-0.1f, 0.1f),
                                                            Random.Range(-1, 1)) * shakeForce;
            }
        }

        //camPos.transform.position = Vector3.Lerp(camPos.transform.position, gotoPos, Time.fixedDeltaTime);
        //Vector3 gotoPos = originalCamPos + (new Vector3(Random.Range(-1, 1),
        //                                        Random.Range(-0.1f, 0.1f),
        //                                        Random.Range(0, 0.5f)) * shakeForce * 15f);
        //if (length > 0.01f)
        //{

        //    gotoPos = originalCamPos + new Vector3(Random.Range(-1, 1),
        //                                        Random.Range(-0.1f, 0.1f),
        //                                        Random.Range(-1, 1)) * shakeForce;



        //}
        //else
        //{
        //    gotoPos = originalCamPos + (new Vector3(Random.Range(-1, 1),
        //                                        Random.Range(-0.1f, 0.1f),
        //                                        Random.Range(0, 0.5f)) * shakeForce * 15f);
        //    length = Vector3.Distance(camPos.transform.position, gotoPos);
        //    Debug.Log(length);

        //}


        //else
        //{
        //    camPos.transform.position = Vector3.Lerp(camPos.transform.position, originalCamPos, Time.fixedDeltaTime);
        //    camPos.transform.position = originalCamPos;
        //}
        else
        {
            camPos.transform.position = originalCamPos;
            shakeForce -= Time.fixedDeltaTime;
            if (shakeForce < 0f)
            {
                shakeForce = 0f;
                isShaking = false;
            }
            if (shakeDelay <= 0f)
            {
                shakeDelay = 0.01f;
                camPos.transform.position = originalCamPos + new Vector3(Random.Range(-1, 1),
                                                    Random.Range(-0.1f, 0.1f),
                                                    Random.Range(-1, 1)) * shakeForce;
            }


        }
    }
}
