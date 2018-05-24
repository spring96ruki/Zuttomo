﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : SingletonMono<ChaserController> {

    public float m_coolTime;
    public float m_maxCoolTime = 100f;
    public float m_stanTime;

    // Use this for initialization
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_coolTime = m_maxCoolTime;
    }

    // Update is called once per frame
    void Update () {
        --m_coolTime;
        if (m_coolTime <= 0)
        {
            m_coolTime = 0;
        }

        // Rキー押して対象がいればスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChaserSkill.Instance.Skil(gameObject);
            RunnerController.Instance.stanTime = m_stanTime;
        }
	}
}
