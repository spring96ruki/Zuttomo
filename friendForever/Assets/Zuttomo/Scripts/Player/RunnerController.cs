using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    normal = 0,
    stan
}

public class RunnerController : SingletonMono<RunnerController>
{

    float m_stanTime;
    public float stanTime { get { return m_stanTime; } set { m_stanTime = value; } }
    [HideInInspector]
    public bool ChaserFlag;
    public float State_timer;
    float currentSpeed;
    //RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
    RunnerSkill m_runnerSkill;
    protected RunnerStatus m_status;
    [HideInInspector]
    public Rigidbody m_rigidBody;

    float m_currentSpeed;
    bool isStan = false;
    RunnerState m_state;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
		m_runnerSkill = GetComponent<RunnerSkill> ();
        m_rigidBody = GetComponent<Rigidbody>();
        m_runnerStatus.animator = GetComponent<Animator>();
        //初期ステータス
        m_runnerStatus.firstSpeed = 3;
        m_runnerStatus.maxSpeed = 5;
        m_runnerStatus.health = 5;
        m_runnerStatus.maxHealth = 5;
        m_runnerStatus.isState = true;
        m_runnerStatus.ishave = false;
        m_runnerStatus.animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
        RunnerStanTime();
        m_runnerInput.PController();
		if (transform.position.y < -10) {
			transform.position = new Vector3 (0, 3, 0);
		} 
    }

    public void RunnerStan(RunnerState state, float skilTime)
    {
        m_state = state;
        if (m_state == RunnerState.stan)
        {
            Debug.Log("スタンしたよ");
            // RunnerStateがStanに変更されたらisStanをtrueに変更
            isStan = true;
        }
    }

    public void RunnerStanTime()
    {
        Debug.Log(m_stanTime);
        // isStanがtrueになったらスタン処理開始
        if (isStan)
        {
            --m_stanTime;
            Debug.Log("通った");

            // スタン処理
            // 現在のスピードを別の変数に保持し、スピードを0に変更
            m_currentSpeed = m_runnerStatus.speed;
            m_runnerStatus.speed = 0f;

            if (m_stanTime < 0)
            {
                m_stanTime = 0;
            }
            if (m_stanTime == 0)
            {
                // stanTimeが0になったらisStanをfalseにする
                // RunnerStanをnormalに変更
                isStan = false;
                m_state = RunnerState.normal;
                Debug.Log("スタン終わったよ");

                // isStanがfalseに変更されたら、スタン処理終了
                // スタン終了時に保持してたスピードをプレイヤーのステータスへ戻す
                if (!isStan)
                {
                    m_runnerStatus.speed = m_currentSpeed;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_runnerStatus.isState == true)
        {
            m_runnerMove.Move();
            m_runnerMove.Button();
        }
        else
        {
            m_runnerStatus.animator.SetBool("HalfRun", false);
            m_runnerStatus.animator.SetBool("FullRun", false);
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

    void OnCollisionEnter(Collision hit)
    {
		m_runnerSkill.HitEvent (hit);
    }

	void OnCollisionStay(Collision check)
    {
		m_runnerSkill.CheckEvent(check);
    }
}
