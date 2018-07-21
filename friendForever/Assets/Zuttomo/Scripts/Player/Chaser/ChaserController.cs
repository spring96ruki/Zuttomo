using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible
}

public class ChaserController : SingletonMono<ChaserController>
{
    float m_invisibleTime;
    Rigidbody m_rigidBody;

    RunnerController m_runnerController;
    PlayerInput m_playerInput;
    PlayerMove m_playerMove;
    PlayerStatus m_playerStatus;
    PlayerAnimator m_playerAnimator;
    ChaserSkill m_chaserSkil;

    UIController m_uIController;

    public GameObject UIController;

    public int m_playerNum;
    public bool m_isTakePlayer;
    public bool m_isInvisible;
    public Color m_chaserColor;
    public float m_stanCoolTime;
    public float m_maxStanCoolTime = 100f;
    public float m_invisibleCoolTime;
    public float m_maxInvisibleCoolTime = 100f;
    public float m_stanTime = 50;
    public float m_maxInvisibleTime = 50;
    public float m_stateTimer;
    public ChaserState m_chaserState;

    public float m_timer;
    public GameObject m_camera;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_chaserColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        m_uIController = GameObject.Find("UIController").GetComponent<UIController>();
        //m_uIController = GetComponent<UIController>();

        m_chaserState = ChaserState.normal;
        m_runnerController = new RunnerController();
        m_playerInput = new PlayerInput();
        m_playerMove = new PlayerMove();
        m_playerStatus = new PlayerStatus();
        m_playerAnimator = new PlayerAnimator();
        m_chaserSkil = new ChaserSkill();
    }
    void DebugSkil()
    {
        // Rキー押してcoolTimeが0ならスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_chaserSkil.StanSkilStart(gameObject);
            m_runnerController.m_stanTime = m_stanTime;
        }

        // Tキー押してcoolTimeが0なら透明化開始
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_chaserState = ChaserState.invisible;
            m_invisibleTime = m_maxInvisibleTime;
        }
    }

    void DebugLog()
    {
        Debug.Log("buttonA: " + m_playerInput.button_A);
        Debug.Log("buttonRB: " + m_playerInput.button_RB);
        Debug.Log("buttonX: " + m_playerInput.button_X);
        Debug.Log("buttonY: " + m_playerInput.button_Y);
        Debug.Log("buttonB: " +m_playerInput.button_B);

        Debug.Log("捕まってるか" + m_isTakePlayer);
    }

    // Update is called once per frame
    void Update () {
        CoolTime();
        ChaserInvisibleTime();
        m_playerInput.PController(m_playerNum);
        DebugSkil();
        DebugLog();

        if (m_playerStatus.isState)
        {
            //m_playerMove.ChaserMove(m_camera);
            ChaserButton();
        }
        else
        {
            m_stateTimer += Time.deltaTime;
            if (m_stateTimer >= 3)
            {
                m_playerStatus.isState = true;
            }
        }

        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            m_chaserSkil.ChaserInvisible(gameObject, m_chaserState, m_invisibleCoolTime);
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 3, 0);
        }
    }

    void CoolTime()
    {
        m_uIController.InvisibleOn( m_invisibleTime, m_maxInvisibleTime);
        m_uIController.StanOn(m_runnerController.m_stanTime, m_stanTime);

        if (m_invisibleTime <= 0)
        {
            m_uIController.GetInvisibleImage().fillAmount = 1f;
        }
        if (m_stanTime <= 0)
        {
            m_uIController.GetStanImage().fillAmount = 1f;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Runner")
        {
            Debug.Log("領域内");
            m_isTakePlayer = true;
            //GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();

            int killRunnerNum = other.GetComponent<RunnerController>().m_playerNum;
            GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().RunnerKill(killRunnerNum);
            //m_playerAnimator.DeathAnimation();
            //other.gameObject.SetActive(false);
            //GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();
        }
    }

    public void ChaserButton()
    {

        if (m_playerInput.button_A)
        {
            Debug.Log("スキル_1");
            m_chaserSkil.StanSkilStart(gameObject);
            m_runnerController.m_stanTime = m_stanTime;
            m_uIController.GetStanImage().fillAmount = 0f;
        }

        if (m_playerInput.button_B)
        {
            if (m_isTakePlayer)
            {
                Debug.Log("タッチ");
            }
            m_playerAnimator.KillAnimation();
            //m_timer = 0;
        }
        //else
        //{
        //    if (m_timer <= 0.5)
        //    {
        //        m_timer += Time.deltaTime;
        //    }
        //}

        if (m_playerInput.button_X)
        {
            if (m_invisibleCoolTime == 0f)
            {
                Debug.Log("スキル_2");
                m_chaserState = ChaserState.invisible;
                m_invisibleTime = m_maxInvisibleTime;
                m_isInvisible = true;
                m_uIController.GetInvisibleImage().fillAmount = 0f;
            }
        }

        if (m_playerInput.button_Y)
        {
            Debug.Log("Y");
        }
    }
}
