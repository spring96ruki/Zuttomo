using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMono<SceneController> {

    //public GameObject m_loadScreen;
    //bool m_isFading = false;
   
    AsyncOperation m_async;
    StringBuilder m_sceneName;

    [SerializeField]
    private FadeManager fadeManager;


    private void Start()
    {
        m_sceneName = new StringBuilder();
    }

    // Scene.Instanse.LoadScene(SceneName.****)
    public void LoadScene(string sceneName)
    {
        m_sceneName.Length = 0;
        m_sceneName.Append(sceneName);
        Debug.Log("第一関門");
        Debug.Log("第一関門突破");
        StartCoroutine(LoadStart());
    }

    IEnumerator LoadStart()
    {
        m_async = SceneManager.LoadSceneAsync(m_sceneName.ToString());
        m_async.allowSceneActivation = false;
        yield return fadeManager.FadeOut();
        m_async.allowSceneActivation = true;
        yield return fadeManager.FadeIn();
       
        
        
    }



}
