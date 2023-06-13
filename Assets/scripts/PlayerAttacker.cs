using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] int damage;
    [SerializeField] float Range;
    [SerializeField,Range(0,360)] float angle;
    private Animator anim;

    private float cosResult;
   

    private void Awake()
    {
        anim = GetComponent<Animator>();
      //  cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad); //오래걸릴꺼같은 값들은 미리 계산해서 사용- 캐싱
    }

   private void  Attack()
    {
        anim.SetTrigger("Attack");
    }
    private void OnAttack(InputValue Value)
    {
        Attack();
    }
    public void AttackTiming()
    {

        //1. 범위 안에 있는지 확인
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range);

        foreach(Collider collider in colliders)
        {
            //2. 앞에 있는지 (내적을 이용한 각도)
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad)) //호도법: 파이씀
                                                                                      //Mathf.Deg2Rad 이게 호도법으로 변환
                                                                                      //60도 공격할려면 반절 쳐야해서 0.5f
            {
                continue;
            }
            // 내적의 결과 + 면 앞때리는거 -면뒤 때리는걸로 판단
            //Dot이 내적함수
            // 내적할때 두 좌표가 있으면 두 좌표 x좌표 곱하고 더하기 y좌표하는게 더 최적화 된다.


            IHitable hitable = collider.GetComponent<IHitable>(); 
            hitable?.TakeHit(damage);
        }

    }
    private void OnDrawGizmosSelected()
    {

        if (!debug)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * Range, Color.blue);
        Debug.DrawRay(transform.position, leftDir * Range, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

}
