using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScreen : SingletonMono<PlayerScreen> {

    public GameObject m_buttonObj;
    public Button m_button;
    public GameObject m_screenObj;
    public Text m_screenTitle;

    void Start()
    {
        m_buttonObj = GameObject.Find("SkillButton");
    }

    // 初期化メソッド
    public void Init()
    {
        ScreenClear();
    }

    public void ScreenSet(PlayerFlag state)
    {
        //RemoveEvent();
        switch (state)
        {
            case PlayerFlag.Runner:
                break;
            case PlayerFlag.Chaser:
                break;
        }
    }

    //void RemoveEvent()
    //{
    //    m_buttonObj.GetComponent<Button>().onClick.RemoveAllListeners();
    //    m_buttonObj.GetComponent<Button>().text = "";
    //    m_buttonTextRight.text = "";
    //}

    public void ScreenClear()
    {
        m_screenObj.SetActive(false);
        //RemoveEvent();
    }
}
