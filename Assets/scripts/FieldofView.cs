using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    [SerializeField] float Range;
    [SerializeField, Range(0f, 360f)] float angle;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask TargetMask;

    private void Update()
    {
        FindTarget();
    }
    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, TargetMask) ;

        foreach (Collider collider in colliders)
        {
            //2. �տ� �ִ��� (������ �̿��� ����)
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                                                                                                  
                                                                                                   
            {
                continue;
            }
            //�߰��� ��ֹ��� ������ ���̸� ���� �ε����°ɷ� Ȯ��
            float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, dirTarget, distanceToTarget, obstacleMask))
            {
                continue;
            }

            Debug.DrawRay(transform.position, dirTarget * distanceToTarget, Color.red);
           

        }
    }
    private void OnDrawGizmosSelected()
    {
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
