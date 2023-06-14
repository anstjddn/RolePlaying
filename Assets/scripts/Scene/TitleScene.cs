using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
   public void StartButton()
    {
        GameManager.Scene.LoadScene("GameScene");
    }

    protected override IEnumerator LoadingRoutin()
    {
        yield return null;
    }
}


