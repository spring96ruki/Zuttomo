using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMono<SceneController> {

    //public GameObject m_loadScreen;
    public Color m_fadeColor = Color.black;

    bool m_isFading = false;
    float m_fadeAlpha = 0;
    AsyncOperation m_async;
    StringBuilder m_sceneName;

    public void OnGUI()
    {

        // Fade
        if (m_isFading)
        {
            //色と透明度を更新して白テクスチャを描画
            this.m_fadeColor.a = this.m_fadeAlpha;
            GUI.color = this.m_fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

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
        FadeManager.Instance.FadeOut();
        Debug.Log("第一関門突破");
        StartCoroutine(LoadStart());
    }

    IEnumerator LoadStart()
    {
        m_async = SceneManager.LoadSceneAsync(m_sceneName.ToString());
        m_async.allowSceneActivation = false;
        yield return null;
        FadeManager.Instance.FadeIn();
        m_async.allowSceneActivation = true;
    }

    public IEnumerator FadeOut()
    {
        float time = 0;
        float interval = 1.0f;
        while (time <= interval)
        {
            this.m_fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
    }


}
