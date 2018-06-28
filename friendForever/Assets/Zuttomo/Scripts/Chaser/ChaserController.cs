using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible
}

public class ChaserController : SingletonMono<ChaserController> {
    
    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    ChaserMove m_ChaserMove;
    RunnerStatus m_runnerStatus;

    public float m_coolTime;
    public float m_maxCoolTime = 100f;
    public float m_stanTime;
    public float m_invisibleTime;
    public ChaserState m_chaserState;
    [HideInInspector]
    public float m_stateTimer;

    // Use this for initialization
    private void Start()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_ChaserMove = GetComponent<ChaserMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();

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
        m_coolTime = m_maxCoolTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log(m_coolTime);
        --m_coolTime;

        if (m_runnerStatus.isState == true)
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
        if (m_coolTime <= 0)
        {
            m_coolTime = 0;
        }

        // Rキー押して対象がいればスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChaserSkill.Instance.StanSkilStart(gameObject);
            RunnerController.Instance.m_stanTime = m_stanTime;
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
        m_runnerInput.PController();
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
