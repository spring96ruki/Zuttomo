using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    unknown,
    alive,
    dead
}

public enum RunnerDoingState
{
    unknown,
    idle,
    walk,
    run
}

public enum RunnerHaveItemState
{
    not,
    itimatsu,
    drug,
    amulets,
    test
}

public class RunnerController : MonoBehaviour {

    PlayerMove m_playerMove;
    PlayerInput m_playerInput;
    PlayerStatus m_playerStatus;
    PlayerCamera m_playerCamera;
    RunnerSkil m_runnerSkil;

    float deltaTime;
    string m_itemTagName;

    PlayerFlag m_playerFlag = PlayerFlag.Runner;
    RunnerState m_runnerState = RunnerState.alive;
    RunnerDoingState m_runnerDoingState = RunnerDoingState.idle;
    RunnerHaveItemState m_runnerHaveItemState = RunnerHaveItemState.not;

    public int m_playerNum;
    public bool isDash;
    public bool isTouchItem;
    public Rigidbody m_rigidBody;
    public GameObject m_player;
    public GameObject m_cameraObject;
    public GameObject m_itemObject;

    private void Awake()
    {
        isDash = false;
        isTouchItem = false;
        deltaTime = Time.deltaTime;
        m_playerMove = new PlayerMove();
        m_playerInput = new PlayerInput();
        m_playerStatus = new PlayerStatus();
        m_playerCamera = new PlayerCamera();
        m_runnerSkil = new RunnerSkil();
        m_playerStatus.StatusInit(m_playerFlag);
    }

    // Update is called once per frame
    void Update () {
        Button();
        BuffTime();
        HealthController();
        m_playerInput.PController(m_playerNum);
        m_playerCamera.CameraMovement(m_cameraObject, m_player);
    }

    private void FixedUpdate()
    {
        m_playerMove.RunnerMovement(m_rigidBody, m_runnerDoingState, m_playerInput, m_playerStatus);
    }

    private void OnCollisionEnter(Collision other)
    {
        isTouchItem = true;
        m_itemObject = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        isTouchItem = false;
        m_itemObject = null;
    }

    void Button()
    {
        if (m_playerInput.button_R)
        {
            m_runnerDoingState = RunnerDoingState.run;
        }
        else
        {
            m_runnerDoingState = RunnerDoingState.walk;
            m_playerStatus.health += Time.deltaTime;
        }

        if (m_playerInput.button_E)
        {
            if (m_playerStatus.isHaveItem)
            {
                HaveItemEvent(m_runnerHaveItemState);
            }

            if (isTouchItem)
            {
                if (!m_playerStatus.isHaveItem)
                {
                    isTouchItem = false;
                    m_playerStatus.isHaveItem = true;
                    ItemCheck(m_itemObject);
                    Destroy(m_itemObject);
                }
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
                if (m_runnerDoingState == RunnerDoingState.run)
                {
                    m_playerStatus.health = m_playerStatus.isBuff ? m_playerStatus.maxHealth : m_playerStatus.health - deltaTime;
                }
            }
        }
        else
        {
            m_playerInput.button_R = false;
            m_runnerDoingState = RunnerDoingState.walk;
        }
    }

    void BuffTime()
    {
        if (m_playerStatus.buffTime < 0)
        {
            m_playerStatus.isBuff = false;
            m_playerStatus.buffTime = 0;
        }

        m_playerStatus.buffTime -= deltaTime;
    }

    void ItemCheck(GameObject itemObject)
    {
        switch (itemObject.tag)
        {
            case ItemName.ITIMATSU:
                m_runnerHaveItemState = RunnerHaveItemState.itimatsu;
                break;
            case ItemName.DRUG:
                m_runnerHaveItemState = RunnerHaveItemState.drug;
                break;
            case ItemName.AMULETS:
                m_runnerHaveItemState = RunnerHaveItemState.amulets;
                break;
            case TagName.TestTag:
                Debug.Log("テスト判定だよ");
                m_runnerHaveItemState = RunnerHaveItemState.test;
                break;
        }
    }

    void HaveItemEvent(RunnerHaveItemState runnerHaveItemState)
    {
        switch (runnerHaveItemState)
        {
            case RunnerHaveItemState.itimatsu:
                break;
            case RunnerHaveItemState.drug:
                break;
            case RunnerHaveItemState.amulets:
                break;
            case RunnerHaveItemState.test:
                Debug.Log("テストアイテム使ったよ");
                m_runnerSkil.DrugEvent(m_playerStatus);
                break;
        }

        m_runnerHaveItemState = RunnerHaveItemState.not;
    }
}
