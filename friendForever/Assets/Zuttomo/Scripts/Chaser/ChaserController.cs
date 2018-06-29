using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible
}

public class ChaserController : SingletonMono<ChaserController> {

    public Color m_chaserColor;
    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    ChaserMove m_ChaserMove;
    RunnerStatus m_runnerStatus;

    public float m_stanCoolTime;
    public float m_maxStanCoolTime = 100f;
    public float m_invisibleCoolTime;
    public float m_maxInvisibleCoolTime;
    public float m_stanTime;
    float m_invisibleTime;
    public float m_maxInvisibleTime;
    public ChaserState m_chaserState;
    [HideInInspector]
    public float m_stateTimer;

    public bool m_isInvisible;

    // Use this for initialization
    private void Start()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_ChaserMove = GetComponent<ChaserMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_chaserColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;

        //初期ステータス
        m_runnerStatus.firstSpeed = 10;
        m_runnerStatus.maxSpeed = 10;
        m_runnerStatus.speed = m_runnerStatus.firstSpeed;
        m_runnerStatus.health = 0;
        m_runnerStatus.maxHealth = 0;
        m_runnerStatus.isState = true;
        m_runnerStatus.ishave = false;
        m_runnerStatus.animator = GetComponent<Animator>();

        StanInit();
        m_chaserState = ChaserState.normal;
    }

    public void StanInit()
    {
        m_stanCoolTime = m_maxStanCoolTime;
    }

    public void InvisibleInit()
    {
        m_invisibleCoolTime = m_maxInvisibleCoolTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log(m_coolTime);
        --m_stanCoolTime;
        --m_invisibleCoolTime;

        if (m_runnerStatus.isState)
        {
            m_ChaserMove.Move();
            m_ChaserMove.DemonButton();
        }
        else
        {
            m_stateTimer += Time.deltaTime;
            //Vector3 force;
            //force = transform.position * 200;
            // Rigidbodyに力を加えて発射
            //GetComponent<Rigidbody>().AddForce(force);
            if (m_stateTimer >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        CoolTime();

        // Rキー押してcoolTimeが0ならスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChaserSkill.Instance.StanSkilStart(gameObject);
            RunnerController.Instance.stanTime = m_stanTime;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_chaserState = ChaserState.invisible;
            m_invisibleTime = m_maxInvisibleTime;
        }

        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            ChaserSkill.Instance.ChaserInvisible(gameObject, m_chaserState, m_invisibleCoolTime);
        }

        m_runnerInput.PController();
        ChaserInvisibleTime();
	}

    void CoolTime()
    {
        if (m_invisibleCoolTime <= 0)
        {
            m_invisibleCoolTime = 0;
        }
        if (m_stanCoolTime <= 0)
        {
            m_stanCoolTime = 0;
        }
    }

    void ChaserInvisibleTime()
    {
        Debug.Log(m_invisibleTime);
        // m_isInvisibleがtrueになったら透明化開始
        if (m_isInvisible)
        {
            --m_invisibleTime;
            if (m_invisibleTime < 0f)
            {
                m_invisibleTime = 0f;
            }
            if (m_invisibleTime == 0f)
            {
                // 透明化の時間が終わったら、m_isInvisibleとchaserStateをノーマルに戻す
                m_isInvisible = false;
                m_chaserState = ChaserState.normal;
                if (!m_isInvisible)
                {
                    GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(m_chaserColor.r, m_chaserColor.g, m_chaserColor.b, 1f);
                }
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == TagName.Push)
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            m_stateTimer = 0;
        }
    }
}
