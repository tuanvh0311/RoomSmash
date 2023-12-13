using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Joystick Joystick;
    public Transform camPos;
    public Rigidbody rb;

    private void Start()
    {
        GameManager.Instance.reloadScene += OnInit;
    }

    private void OnInit()
    {
        transform.position = new Vector3(0, 1f, 0);
        rb.velocity = Vector3.zero;
    }

    //public void FixedUpdate()
    //{
    //    if (Joystick.Vertical == 0 && Joystick.Horizontal == 0)
    //    {
    //        rb.velocity = new Vector3(0f, rb.velocity.y, 0f); ;
    //    }
    //    Vector3 direction = camPos.forward * Joystick.Vertical + camPos.right * Joystick.Horizontal;
    //    direction = new Vector3(direction.x, 0f, direction.z);
    //    rb.AddForce(direction.normalized * speed * (1f / (Time.fixedDeltaTime * Time.timeScale)), ForceMode.Force);
    //    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    //    if (flatVel.sqrMagnitude > speed)
    //    {
    //        Vector3 limitedVel = flatVel.normalized * speed;
    //        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    //    }

    //}
    public void FixedUpdate()
    {

        if (Joystick.Vertical != 0 || Joystick.Horizontal != 0)
        {
            Vector3 direction = camPos.forward * Joystick.Vertical + camPos.right * Joystick.Horizontal;
            direction = new Vector3(direction.x, 0f, direction.z);
            transform.position += direction.normalized * speed * Time.fixedDeltaTime / Time.timeScale;
        }
        
    }
}