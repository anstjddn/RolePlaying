using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerAttack : MonoBehaviour
{
    [SerializeField] int damege;
    [SerializeField, Range(0f, 360f)] float angle;
    [SerializeField] float Range;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void OnAttack(InputValue value)
    {
        Attack();
    }

    public void AttackTiming()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range);

        foreach (Collider collider in colliders)
        {
            Vector3 directdir = (collider.transform.position-transform.position).normalized;
            if (Vector3.Dot(transform.forward, directdir)<Mathf.Cos(angle*0.5f*Mathf.Deg2Rad))
            {
                continue;
            }
            IHitable hitable = collider.GetComponent<IHitable>();
            hitable?.TakeHit(damege);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Vector3 SerializeRight = angletodir(transform.eulerAngles.y+ angle * 0.5f);
        Vector3 Serializeleft = angletodir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, SerializeRight * Range, Color.blue);
        Debug.DrawRay(transform.position, Serializeleft * Range, Color.blue);


    }

   private Vector3 angletodir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

}

