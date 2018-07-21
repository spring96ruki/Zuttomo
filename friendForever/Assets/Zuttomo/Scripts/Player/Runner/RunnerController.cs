using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    normal = 0,
    stan
}

public class RunnerController : MonoBehaviour
{
    bool isStan = false;
    RunnerState m_runnerState = RunnerState.normal;
    PlayerFlag m_playerFlag;
    float m_currentSpeed;
    Rigidbody m_rigidBody;

    internal PlayerInput m_playerInput;
    internal PlayerStatus m_playerStatus;
    internal PlayerMove m_playerMove;
    internal PlayerAnimator m_playerAnimator;
    internal RunnerSkill m_runnerSkill;

    float m_timer;
    //UIController m_uiController;
    Vector3 m_prevPos;

    public GameObject m_runner;
    public int m_playerNum;
    public float m_stanTime;
    public float m_stateTimer;
    //public GameObject m_push;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_playerFlag = PlayerFlag.Runner;
        m_playerInput = new PlayerInput();
        m_playerStatus = new PlayerStatus();
        m_playerMove = new PlayerMove();
        m_playerAnimator = new PlayerAnimator();
        m_runnerSkill = new RunnerSkill();
        //m_uiController = new UIController();
    }

    // Update is called once per frame
    void Update()
    {
        RunnerStanTime();
        //m_playerInput.PController(m_playerNum);
        RunnerMovement();
        m_playerAnimator.DownAnimation(m_playerStatus);
    }

    public void RunnerMovement()
    {
        if (m_playerStatus.isState)
        {
            m_playerMove.Move(m_playerFlag, m_playerAnimator, m_playerInput, m_playerStatus);
            RunnerButton();
        }
        //else
        //{
        //    m_stateTimer += Time.deltaTime;
        //    if (m_stateTimer >= 3)
        //    {
        //        m_playerStatus.isState = true;
        //        m_stateTimer = 0;
        //        HealthControll();
        //    }
        //}
    }

    public void RunnerStan(RunnerState state)
    {
        m_runnerState = state;
        if (m_runnerState == RunnerState.stan)
        {
            Debug.Log("スタンしたよ");
            // RunnerStateがStanに変更されたらisStanをtrueに変更
            isStan = true;
        }
    }

    public void RunnerStanTime()
    {
        // isStanがtrueになったらスタン処理開始
        if (isStan)
        {
            --m_stanTime;
            Debug.Log("通った");

            // スタン処理
            // 現在のスピードを別の変数に保持し、スピードを0に変更
            //m_currentSpeed = m_playerStatus.speed;
            m_playerStatus.firstSpeed = 0f;
            m_playerStatus.maxSpeed = 0f;

            if (m_stanTime < 0)
            {
                m_stanTime = 0;
            }
            if (m_stanTime == 0)
            {
                // stanTimeが0になったらisStanをfalseにする
                // RunnerStanをnormalに変更
                isStan = false;
                m_runnerState = RunnerState.normal;
                Debug.Log("スタン終わったよ");

                // isStanがfalseに変更されたら、スタン処理終了
                // スタン終了時に保持してたスピードをプレイヤーのステータスへ戻す
                if (!isStan)
                {
                    //m_playerStatus.speed = m_currentSpeed;
                    m_playerStatus.firstSpeed = 2;
                    m_playerStatus.maxSpeed = 3;
                }
            }
        }
        //m_playerMove.m_prevPos = m_playerMove.m_player.transform.position;
    }

    void OnCollisionEnter(Collision hit)
    {
		m_runnerSkill.HitEvent (hit, m_playerStatus.isState);
    }

	void OnCollisionStay(Collision check)
    {
		m_runnerSkill.CheckEvent(check , m_playerStatus , m_playerNum);
    }

    public void HealthControll()
    {
        if (m_playerFlag == PlayerFlag.Chaser)
        {
            m_playerStatus.speed = m_playerStatus.maxSpeed;
        }
        else
        {
            if (m_playerStatus.isHealth)
            {
                if (m_playerInput.Laxis_y >= 0.1f || m_playerInput.Laxis_y <= -0.1f || m_playerInput.Laxis_x >= 0.1f || m_playerInput.Laxis_x <= -0.1f)
                {
                    if (m_playerInput.button_RB)
                    {
                        Debug.Log("ダッシュ");
                        m_playerStatus.speed = m_playerStatus.maxSpeed;
                        m_playerStatus.health -= Time.deltaTime;
                        Debug.Log("減る");
                        //m_uiController.HealthUIControll();
                    }
                }

            }
            else
            {
                m_playerStatus.speed = m_playerStatus.firstSpeed;
            }

            if (m_playerStatus.health > m_playerStatus.maxHealth)
            {
                m_playerStatus.isHealth = true;
                m_playerStatus.health = m_playerStatus.maxHealth;
            }

            if (m_playerStatus.health <= 0f)
            {
                m_playerStatus.isHealth = false;
                m_playerStatus.health = 0f;
            }

            //ボタンが押されてなかったら
            if (m_playerInput.button_RB == false || m_prevPos == m_runner.transform.position || m_playerStatus.isHealth == false)
            {
                m_playerStatus.speed = m_playerStatus.firstSpeed;
                //スタミナ回復
                m_playerStatus.health += Time.deltaTime;
                Debug.Log("kaihuku");
                //m_uiController.HealthUIControll();
            }
        }
    }

    public void RunnerButton()
    {
        //if (!m_playerAnimator.m_action)
        //{
            if (m_playerInput.button_A)
            {
                Debug.Log("突き飛ばし");
                //m_playerAnimator.PushAnimation();
                //m_push.SetActive(true);
                m_timer = 0;
            }
            else
            {
                if (m_timer <= 0.5)
                {
                    m_timer += Time.deltaTime;
                    //m_push.SetActive(false);
                }
            }

            if (m_playerInput.button_B)
            {
                Debug.Log("決定");
            }

            if (m_playerInput.button_X)
            {
                m_runnerSkill.ItemEvent(m_playerStatus, m_playerNum);
            }

            if (m_playerInput.button_Y)
            {
                Debug.Log("Y");
            }
        //}
    }
}
