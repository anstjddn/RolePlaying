using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class newplayerMove : MonoBehaviour
{
    private CharacterController controller;


    [SerializeField] float walkspeed;
    [SerializeField] float Runspeed;
    private float curspeed;
    Vector3 movedir;
    private bool iswalk;

    [SerializeField] float Jumpspeed;
    private float yspeed;
  
    private Animator anim;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        gravity();
    }
    
    private void Jump()
    {
        yspeed = Jumpspeed;

    }
    private void OnJump(InputValue value)
    {

        Jump();
    }

    private void Move()
    {
        


        if(movedir.magnitude == 0)
        {
            anim.SetFloat("Movespeed", curspeed);
            curspeed= Mathf.Lerp(curspeed, 0, 0.1f);
            return;
        }

        if (iswalk)
        {
            curspeed = Mathf.Lerp(curspeed, walkspeed, 0.1f);
        }
        else
        {
            curspeed = Mathf.Lerp(curspeed, Runspeed, 0.1f);
        }

        anim.SetFloat("Movespeed", curspeed);


        Vector3 PlayerForwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 PlayerRightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        controller.Move(PlayerForwardVec * movedir.z* curspeed * Time.deltaTime);
        controller.Move(PlayerRightVec *movedir.x* curspeed * Time.deltaTime);

        Quaternion Lookratation = Quaternion.LookRotation(PlayerForwardVec * movedir.z + PlayerRightVec * movedir.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, Lookratation, 0.1f);



    }
    private void OnMove(InputValue value)
    {
        movedir.x = value.Get<Vector2>().x;
        movedir.z = value.Get<Vector2>().y;
    }
    private void gravity()
    {
        yspeed += Physics.gravity.y * Time.deltaTime;       // 중력설정
        if (controller.isGrounded && yspeed < 0)
        {
            yspeed = 0;
        }
        controller.Move(Vector3.up * yspeed * Time.deltaTime);      // 중력 계쏙 적용

    }
    private void OnWalk(InputValue value)
    {
        iswalk = value.isPressed;
    }

}
