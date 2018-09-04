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
    PlayerInput m_playerInput;
    PlayerMove m_playerMove;
    PlayerStatus m_playerStatus;
    ChaserSkill m_chaserSkil;
    RunnerController m_runnerController;

    float deltaTime;

    public ChaserState m_chaserState;

    public int m_playerNum;
    public bool m_isTakePlayer;
    public bool m_isInvisible;
    public GameObject m_chaser;
    public GameObject m_camera;
    public Color m_chaserColor;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_chaserColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        deltaTime = Time.deltaTime;
        m_chaserState = ChaserState.normal;
        m_playerInput = new PlayerInput();
        m_playerMove = new PlayerMove();
        m_playerStatus = new PlayerStatus();
        m_chaserSkil = new ChaserSkill();
        m_runnerController = new RunnerController();
    }

    // Update is called once per frame
    void Update()
    {
        ChaserInvisibleTime();
        m_playerInput.PController(m_playerNum);
        Button();
        CoolTimer();
        DebugLog();

        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            m_chaserSkil.ChaserInvisible(gameObject, m_chaserState, m_playerStatus.invisibleCoolTime);
        }
    }

    void CoolTimer()
    {
        m_playerStatus.invisibleCoolTime -= deltaTime;
        m_playerStatus.stanCoolTime -= deltaTime;
    }

    void Button()
    {
        // Rキー押してcoolTimeが0ならスタン開始
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_chaserSkil.StanSkil(gameObject, m_runnerController, m_playerStatus);
        }

        // Tキー押してcoolTimeが0なら透明化開始
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_chaserState = ChaserState.invisible;
            m_playerStatus.invisibleTime = m_playerStatus.maxInvisibleTime;
        }
    }

    void DebugLog()
    {
        Debug.Log("buttonA: " + m_playerInput.button_A);
        Debug.Log("buttonRB: " + m_playerInput.button_RB);
        Debug.Log("buttonX: " + m_playerInput.button_X);
        Debug.Log("buttonY: " + m_playerInput.button_Y);
        Debug.Log("buttonB: " + m_playerInput.button_B);

        Debug.Log("捕まってるか" + m_isTakePlayer);
    }

    void ChaserInvisibleTime()
    {
        Debug.Log(m_playerStatus.invisibleTime);
        // m_isInvisibleがtrueになったら透明化開始
        if (m_isInvisible)
        {
            --m_playerStatus.invisibleTime;
            if (m_playerStatus.invisibleTime < 0f)
            {
                m_playerStatus.invisibleTime = 0f;
            }
            if (m_playerStatus.invisibleTime == 0f)
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
}
