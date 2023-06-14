using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    public LoadingUI loadingui;

    private BaseScene curScene;

    public BaseScene CurScene
    {
        get
        {
            if(curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            return curScene;
        }
    }


    public void LoadScene(string SceneName)
    {
        StartCoroutine(LoadingRoutin(SceneName));
    }
    private void Awake()
    {
        LoadingUI ui = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadingui = Instantiate(ui);
        loadingui.transform.SetParent(transform,false);
    }

    IEnumerator LoadingRoutin(string SceneName)
    {
      loadingui.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;     // �ε��߰��� ���Ӿ����� �����̸� �ȵǼ�
        loadingui.Setprogress(0f);
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(SceneName);
       // oper.allowSceneActivation = false; // �̰� Ʈ��� �ε��Ǹ� �ٷ� �Ѿ
        while (!oper.isDone)
        {
            loadingui.Setprogress(Mathf.Lerp(0f,0.5f,oper.progress));
            yield return null;
        }
        CurScene.LoadAsync();
        while (CurScene.progress < 1f)
        {
            loadingui.Setprogress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
            yield return null;
        }
        Time.timeScale = 1f;
        loadingui.Setprogress(1f);
        loadingui.Fadein();
        yield return new WaitForSecondsRealtime(1f);



    }
}
