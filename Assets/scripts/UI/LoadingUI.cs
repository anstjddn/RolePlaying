using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
   [SerializeField] Slider Slider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Fadein()
    {
        anim.SetBool("active", true);
    }
    public void FadeOut()
    {
        anim.SetBool("active", false);
    }
    public void Setprogress(float progress)
    {
        Slider.value = progress;
    }

}
