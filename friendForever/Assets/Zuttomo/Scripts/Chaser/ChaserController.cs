using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible,
    stan
}

public class ChaserController : SingletonMono<ChaserController> {
    
    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    ChaserMove m_ChaserMove;
    RunnerStatus m_runnerStatus;
    UIController m_uIController;

    public GameObject UIController;

    public float m_coolTime;
    public float m_maxCoolTime = 100f;
    public float m_stanTime;
    public float m_invisibleTime;
    public ChaserState m_chaserState;
    [HideInInspector]
    public float State_timer;
    

    // Use this for initialization
    private void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_ChaserMove = GetComponent<ChaserMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();

        m_uIController = UIController.GetComponent<UIController>();

    //初期ステータス
        m_runnerStatus.firstSpeed = 10;
        m_runnerStatus.maxSpeed = 10;
        m_runnerStatus.speed = m_runnerStatus.firstSpeed;
        m_runnerStatus.health = 0;
        m_runnerStatus.maxHealth = 0;
        m_runnerStatus.isState = true;
        m_runnerStatus.ishave = false;
        m_runnerStatus.isBuff = false;
        m_runnerStatus.isInvincible = false;
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

        if (m_runnerStatus.isState == true)
        {
            //m_ChaserMove.Move();
           // m_ChaserMove.DemonButton();
        }
        else
        {
            State_timer += Time.deltaTime;
            //Vector3 force;
            //force = transform.position * 200;
            // Rigidbodyに力を加えて発射
            //GetComponent<Rigidbody>().AddForce(force);
            if (State_timer >= 3)
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
            StanInit();
        }

        // Rキー押して対象がいればスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChaserSkill.Instance.StanSkilStart(gameObject);
            RunnerController.Instance.stanTime = m_stanTime;
            m_uIController.StanOn(m_coolTime, m_maxCoolTime);
            m_chaserState = ChaserState.stan;

            //m_uIController.InvisibleOn(m_coolTime, m_maxCoolTime);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_chaserState = ChaserState.invisible;
            m_uIController.GetInvisibleImage().fillAmount = 0;
            //ChaserSkill.Instance.InvisibleSkilStart(gameObject, m_coolTime, m_invisibleTime);
        }

        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            --m_coolTime;
            //ChaserSkill.Instance.ChaserInvisible(gameObject, m_coolTime);
            m_uIController.InvisibleOn(m_coolTime,m_maxCoolTime);
            //m_uIController.StanOn(m_coolTime, m_maxCoolTime);

            if (m_coolTime == 0)
            {
                m_uIController.GetInvisibleImage().fillAmount = 1f;
                m_chaserState = ChaserState.normal;
            }
        }

        if (m_chaserState == ChaserState.stan)
        {
            Debug.Log("透明化");
            --m_coolTime;
            //ChaserSkill.Instance.Chaser(gameObject, m_coolTime);
            m_uIController.StanOn(m_coolTime, m_maxCoolTime);
            //m_uIController.StanOn(m_coolTime, m_maxCoolTime);

            if (m_coolTime == 0)
            {
                m_uIController.GetStanImage().fillAmount = 1f;
                m_chaserState = ChaserState.normal;
            }
        }
        m_runnerInput.PController();
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == TagName.Push)
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            State_timer = 0;
        }
    }
}
