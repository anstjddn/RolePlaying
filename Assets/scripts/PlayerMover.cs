using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{

    [SerializeField] bool Debug;
    [SerializeField] float walkspeed;
    [SerializeField] float Runspeed;
    [SerializeField] float Jumpspeed;
    [SerializeField] float WalkStepRange;
    [SerializeField] float RunStepRange;
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
    float Laststeptime = 0.5f;
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
       

        // y좌표를 없애므로 벡터가 짧아진다 따라서 벡터를 크기를 바꿔줄 필요성이잇따. 벡터의 크기가 1인벡터를 반환받는
        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        // 일단 방향만 원하면 normalized 써
        Vector3 RightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        controller.Move(forwardVec *movedir.z* curspeed * Time.deltaTime);
        controller.Move(RightVec * movedir.x * curspeed * Time.deltaTime);

        anim.SetFloat("Movespeed", curspeed);
        Quaternion lookratation = Quaternion.LookRotation(forwardVec * movedir.z + RightVec * movedir.x);
        // 안움직이면 look을 0취급
       // transform.rotation = lookratation;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookratation, 0.1f);

        Laststeptime -= Time.deltaTime;
        if (Laststeptime < 0)
        {
            Laststeptime = 0.5f;
            GenerateFootStepSound();
        }
    }
    private void GenerateFootStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, walk ? WalkStepRange : RunStepRange);
        foreach(Collider collider in colliders)
        {
            ISoundable soundable = collider.GetComponent<ISoundable>();
            soundable?.Listen(transform);
        }
    }

    public void OnMove(InputValue value)
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
    private void OnDrawGizmosSelected()
    {
        if (!Debug)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, WalkStepRange);
        Gizmos.DrawWireSphere(transform.position, RunStepRange);

    }

}
