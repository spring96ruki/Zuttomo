using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible
}

public class ChaserController : SingletonMono<ChaserController> {

    public float m_coolTime;
    public float m_maxCoolTime = 100f;
    public float m_stanTime;
    public float m_invisibleTime;
    public ChaserState m_chaserState;

    // Use this for initialization
    private void Start()
    {
        StanInit();
        m_chaserState = ChaserState.normal;
    }

    public void StanInit()
    {
        m_coolTime = m_maxCoolTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log(m_coolTime);
        --m_coolTime;
    }
    // Update is called once per frame
    void Update () {
        if (m_coolTime <= 0)
        {
            m_coolTime = 0;
        }

        // Rキー押して対象がいればスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChaserSkill.Instance.StanSkilStart(gameObject);
            RunnerController.Instance.stanTime = m_stanTime;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_chaserState = ChaserState.invisible;
            //ChaserSkill.Instance.InvisibleSkilStart(gameObject, m_coolTime, m_invisibleTime);
        }

        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            ChaserSkill.Instance.ChaserInvisible(gameObject, m_coolTime);
        }
    }
}
