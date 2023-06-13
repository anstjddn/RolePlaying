using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRector : MonoBehaviour, ISoundable
{

    public void Listen(Transform trans)
    {
        transform.LookAt(trans.position);      
    }

   
}
