using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChaserState
{
    normal = 0,
    invisible
}

public class ChaserController : SingletonMono<ChaserController> {

    PlayerMove m_playerMove;
    PlayerInput m_playerInput;
    PlayerStatus m_playerStatus;
    PlayerCamera m_playerCamera;
    ChaserSkill m_chaserSkil;
    ConvenientFunction m_convenient;
    RunnerController m_runnerController;

    float deltaTime;

    PlayerFlag m_chaserFlag = PlayerFlag.Chaser;
    public ChaserState m_chaserState = ChaserState.normal;

    public int m_playerNum;
    public bool m_isTakePlayer;
    public bool m_isInvisible;
    public Rigidbody m_rigidBody;
    public GameObject m_player;
    public GameObject m_camera;
    public Color m_chaserColor;

    private void Start()
    {
        deltaTime = Time.deltaTime;
        m_playerInput = new PlayerInput();
        m_playerMove = new PlayerMove();
        m_playerStatus = new PlayerStatus();
        m_playerCamera = new PlayerCamera();
        m_chaserSkil = new ChaserSkill();
        m_convenient = new ConvenientFunction();
        m_runnerController = new RunnerController();
        m_chaserColor = GetComponentInChildren<MeshRenderer>().material.color;
        m_playerStatus.StatusInit(m_chaserFlag);
    }

    // Update is called once per frame
    void Update()
    {
        ChaserInvisible();
        m_playerInput.PController(m_playerNum);
        m_playerMove.ChaserMovement(m_player, m_rigidBody, m_playerInput, m_playerStatus);
        Button();
        CoolTimer();
        //DebugLog();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("範囲内だよ");
        if (other.gameObject.tag == TagName.Runner)
        {
            m_isTakePlayer = true;
            Debug.Log("プレイヤーが範囲内だよ");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_isTakePlayer = false;
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

    void ChaserInvisible()
    {
        if (m_chaserState == ChaserState.invisible)
        {
            Debug.Log("透明化");
            m_chaserSkil.ChaserInvisible(gameObject, m_chaserState, m_playerStatus.invisibleCoolTime);
        }

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
