using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damage;

    Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }
    public void EnableWeaPon()
    {
        coll.enabled = true; // 컴포넌트 활성화
    }


    public void DisableWeaPon()
    {
        coll.enabled = false; // 컴포넌트 비활성화
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        hitable?.TakeHit(damage);
    }
}
