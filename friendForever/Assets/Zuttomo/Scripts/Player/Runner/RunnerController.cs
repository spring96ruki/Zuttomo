using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    unknown,
    idle,
    walk,
    run
}

public class RunnerController : MonoBehaviour {

    PlayerMove m_playerMove;
    PlayerInput m_playerInput;
    PlayerStatus m_playerStatus;
    PlayerCamera m_playerCamera;
    RunnerSkil m_runnerSkil;

    string m_itemTagName;
    PlayerFlag m_playerFlag = PlayerFlag.Runner;
    RunnerState m_runnerState = RunnerState.idle;

    public int m_playerNum;
    public bool isDash;
    public bool isPenalty;
    public bool isTouchItem;
    public float m_buffTime;
    public Rigidbody m_rigidBody;
    public GameObject m_player;
    public GameObject m_cameraObject;

    private void Awake()
    {
        isDash = false;
        isTouchItem = false;
        m_playerMove = new PlayerMove();
        m_playerInput = new PlayerInput();
        m_playerStatus = new PlayerStatus();
        m_playerCamera = new PlayerCamera();
        m_runnerSkil = new RunnerSkil();
        m_playerStatus.StatusInit(m_playerFlag);
    }

    // Update is called once per frame
    void Update () {
        m_playerInput.PController(m_playerNum);
        m_playerCamera.CameraMovement(m_cameraObject, m_player);
        RunnerButton();
        HealthController();
        Debug.Log("isTouchItem: " + isTouchItem);
        Debug.Log("itemTagName: " + m_itemTagName);
	}

    private void FixedUpdate()
    {
        m_playerMove.RunnerMovement(m_rigidBody, m_runnerState, m_playerInput, m_playerStatus);
    }

    private void OnCollisionEnter(Collision other)
    {
        isTouchItem = true;
        m_itemTagName = other.gameObject.tag;
    }

    private void OnCollisionExit(Collision other)
    {
        isTouchItem = false;
        m_itemTagName = null;
    }

    void RunnerButton()
    {
        Debug.Log("スタミナ: " + m_playerStatus.health);
        //Debug.Log("スピード: " + m_playerStatus.speed);

        if (m_playerInput.button_R)
        {
            m_runnerState = RunnerState.run;
        }
        else
        {
            m_runnerState = RunnerState.walk;
            m_playerStatus.health += Time.deltaTime;
        }
        if (m_playerInput.button_E)
        {
            if (isTouchItem)
            {
                CheckItem(m_itemTagName);
            }
        }
    }

    void HealthController()
    {
        if (m_playerStatus.health >= m_playerStatus.maxHealth)
        {
            isDash = true;
            m_playerStatus.health = m_playerStatus.maxHealth;
        }
        if (m_playerStatus.health < 0)
        {
            isDash = false;
            m_playerStatus.health = 0;
        }

        if (isDash)
        {
            if (m_playerStatus.health > 0)
            {
                if (m_runnerState == RunnerState.run)
                {
                    m_playerStatus.health -= Time.deltaTime;
                }
            }
        }
        else
        {
            m_playerInput.button_R = false;
            m_runnerState = RunnerState.walk;
        }
    }

    void CheckItem(string itemTagName)
    {
        if (!m_playerStatus.isHaveItem)
        {
            switch (itemTagName)
            {
                case ItemName.ITIMATSU:
                    break;
                case ItemName.DRUG:
                    m_runnerSkil.DrugEvent(m_buffTime, m_playerStatus);
                    break;
                case ItemName.AMULETS:
                    break;
                case TagName.TestTag:
                    Debug.Log("テスト判定だよ");
                    break;
            }
        }
    }
}
