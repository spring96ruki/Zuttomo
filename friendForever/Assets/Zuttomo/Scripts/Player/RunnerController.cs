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
    bool isStan = false;
    RunnerState m_state;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
	RunnerSkill m_runnerSkill;
    PlayerAnimator m_runnerAnimator;
    protected RunnerStatus m_status;
    [HideInInspector]
    public Rigidbody m_rigidbody;
    bool isStan = false;

    float m_currentSpeed;
    float m_stanTime;
    public int m_playerNum;
    public float stanTime { get { return m_stanTime; } set { m_stanTime = value; } }
    public bool ChaserFlag;
    public float State_timer;
    public Rigidbody m_rigidBody;

    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
		m_runnerSkill = GetComponent<RunnerSkill> ();
        m_rigidBody = GetComponent<Rigidbody>();
        m_runnerAnimator = GetComponent<PlayerAnimator>();
        //初期ステータス
        m_runnerStatus.firstSpeed = 2;
        m_runnerStatus.maxSpeed = 3;
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
        m_runnerInput.PController(m_playerNum);

        if (transform.position.y < -10) {
			transform.position = new Vector3 (0, 3, 0);
		}
    }

    public void RunnerStan(RunnerState state)
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
        //Debug.Log(m_stanTime);
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
            //Debug.Log("RBINPUT" + m_runnerInput.button_RB);
            m_runnerMove.Move();
            m_runnerMove.Button();
        }
        else
        {
            State_timer += Time.deltaTime;
            if (State_timer >= 3)
            {
                m_runnerStatus.isState = true;
                State_timer = 0;
            }
        }
        m_runnerAnimator.DownAnimation();
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
