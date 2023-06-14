using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitableAdaptor : MonoBehaviour, IHitable
{
    public UnityEvent<int> Ondamaged;
    public void TakeHit(int damage)
    {
        Ondamaged?.Invoke(damage);
    }
}
