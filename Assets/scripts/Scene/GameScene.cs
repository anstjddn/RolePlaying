using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    public GameObject PlayerPrefs;
    public Transform playerPosition;
    public CinemachineFreeLook FreeLook;
    public GameObject monsterprefs;
    public Vector3 monsterposition;



    protected override IEnumerator LoadingRoutin()
    {

        float randomx = Random.RandomRange(-20, 0);
        float randomz = Random.RandomRange(-20, 0);

        progress = 0f;
        Debug.Log("���� ���� ����");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.5f;
        Debug.Log("���� ���� ����");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.8f;
        Debug.Log("���� ���� ����");
        GameObject player = Instantiate(PlayerPrefs, playerPosition.position, playerPosition.rotation) ;
        GameObject monster = Instantiate(monsterprefs, new Vector3(randomx,0,randomz),Quaternion.identity);
        yield return new WaitForSecondsRealtime(1f);
        FreeLook.Follow = player.transform;
        FreeLook.LookAt = player.transform;
        yield return new WaitForSecondsRealtime(1f);
        progress = 1f;
        yield return new WaitForSecondsRealtime(1f);
    }


}
