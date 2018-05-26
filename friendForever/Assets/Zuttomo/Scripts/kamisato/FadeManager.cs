using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FadeManager();
            }
            return instance;
        }
    }

    bool m_isFading = false;
    float m_fadeAlpha = 0;
    float m_LoadTime = 1.5f;
    public Color m_fadeColor = Color.black;

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


    // シーン遷移用コルーチン
    //シーン名
    //暗転にかかる時間(秒)
    //public IEnumerator Fade(bool isFade = true)
    //{

    //    float time = 0;
    //    float interval = 1.0f;
    //    while (time <= interval)
    //    {
    //        this.m_fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
    //        time += Time.deltaTime;
    //        yield return 0;
    //    }

    //    if (isFade == false)
    //    {
    //        time = 0;
    //        yield return  new WaitForSeconds(m_LoadTime);
    //        while (time <= interval)
    //        {
    //            this.m_fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
    //            time += Time.deltaTime;
    //            yield return 0;
    //        }
    //    }
    //}

    public IEnumerator FadeOut()
    {
        float time = 0;
        float interval = 1.0f;
        while (time <= interval)
        {
            Debug.Log("うっす");
            m_fadeColor.a = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
    }

    public IEnumerator FadeIn()
    {
        float time = 0;
        float interval = 1.0f;
        while (time <= interval)
        {
            Debug.Log("は？");
            m_fadeColor.a = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
    }
}
