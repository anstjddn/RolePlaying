using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkspeed;
    [SerializeField] float Runspeed;
    [SerializeField] float Jumpspeed;
    private float curspeed;


    private Animator anim;
    private float yspeed;
    private CharacterController controller;
    private Vector3 movedir;
    private bool walk;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Fall();
    }
    private void Move()
    {

        
        if (movedir.magnitude == 0)
        {
            curspeed = Mathf.Lerp(curspeed, 0, 0.1f);
            anim.SetFloat("Movespeed", curspeed);
            return;
        }
        if (walk)
        {
            curspeed = Mathf.Lerp(curspeed, walkspeed, 0.1f);
        
        }
        else
        {
            curspeed = Mathf.Lerp(curspeed, Runspeed, 0.1f);
        }
       

        // y��ǥ�� ���ֹǷ� ���Ͱ� ª������ ���� ���͸� ũ�⸦ �ٲ��� �ʿ伺���յ�. ������ ũ�Ⱑ 1�κ��͸� ��ȯ�޴�
        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        // �ϴ� ���⸸ ���ϸ� normalized ��
        Vector3 RightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        controller.Move(forwardVec *movedir.z* curspeed * Time.deltaTime);
        controller.Move(RightVec * movedir.x * curspeed * Time.deltaTime);

        anim.SetFloat("Movespeed", curspeed);
        Quaternion lookratation = Quaternion.LookRotation(forwardVec * movedir.z + RightVec * movedir.x);
        // �ȿ����̸� look�� 0���
       // transform.rotation = lookratation;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookratation, 0.1f);
      
    }

    private void OnMove(InputValue value)
    {
        movedir.x = value.Get<Vector2>().x;
        movedir.z = value.Get<Vector2>().y;
    }
    private void Jump()
    {
        yspeed = Jumpspeed;
    }
    private void OnJump(InputValue value)
    {
        Jump();
    }
    private void Fall()
    {
        yspeed += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded && yspeed < 0)
        {
            yspeed = 0;
        }
      
        controller.Move(Vector3.up * yspeed * Time.deltaTime);
    }

   private void OnWalk(InputValue value)
    {
        walk = value.isPressed;
    }
 
}
