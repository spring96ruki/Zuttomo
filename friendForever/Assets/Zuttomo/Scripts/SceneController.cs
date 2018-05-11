using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMono<SceneController> {

    public GameObject m_loadScreen;

    AsyncOperation m_async;
    StringBuilder m_sceneName;

    private void Start()
    {
        m_sceneName = new StringBuilder();
    }

    // Scene.Instanse.LoadSceneTest(SceneName.****)
    public void LoadScene(string sceneName)
    {
            m_sceneName.Length = 0;
            m_sceneName.Append(sceneName);
    }

    IEnumerator LoadStart()
    {
        m_async = SceneManager.LoadSceneAsync(m_sceneName.ToString());
        m_async.allowSceneActivation = false;
        yield return null;
        m_async.allowSceneActivation = true;
    }
}
