using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public GameObject camPos = null;
    public GameObject shootPos = null;
    private Vector3 m_v3MousePosition;
    public float MouseSpeed = 0.1f;
    public Joystick cameraJoystick;
    private float m_cameraSpeed;
    Quaternion rot;

    private Camera m_Camera;
    void Start()
    {
        GameManager.Instance.reloadScene += OnInit;
        OnInit();
    }

    void OnInit()
    {
        camPos.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        m_Camera = Camera.main;
        rot = camPos.transform.rotation;
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
}
