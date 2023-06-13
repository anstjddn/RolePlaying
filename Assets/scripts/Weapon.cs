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
        coll.enabled = true; // ������Ʈ Ȱ��ȭ
    }


    public void DisableWeaPon()
    {
        coll.enabled = false; // ������Ʈ ��Ȱ��ȭ
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        hitable?.TakeHit(damage);
    }
}
