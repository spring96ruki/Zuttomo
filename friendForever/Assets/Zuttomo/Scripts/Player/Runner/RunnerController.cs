using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    unknown = 0,
    alive,
    dead
}

public enum RunnerDoingState
{
    unknown = 0,
    idle,
    walk,
    run
}

public enum RunnerHaveItemState
{
    not = 0,
    itimatsu,
    drug,
    amulets,
    test
}

public enum RunnerAbnormalState
{
    normal,
    stan
}

public class RunnerController : MonoBehaviour {

    PlayerMove m_playerMove;
    PlayerInput m_playerInput;
    PlayerStatus m_playerStatus;
    PlayerCamera m_playerCamera;
    RunnerSkil m_runnerSkil;
    ConvenientFunction m_convenient;

    float m_rayDistance = 0.1f;
    Vector3 m_forward = new Vector3(0f, 0f, 1f);
    Vector3 m_back = new Vector3(0f, 0f, -1f);
    Vector3 m_down = new Vector3(0f, -1f, 0f);
    Vector3[] charVector = new Vector3[3];

    float deltaTime;
    string m_itemTagName;

    PlayerFlag m_playerFlag = PlayerFlag.Runner;
    RunnerState m_state = RunnerState.alive;
    RunnerDoingState m_doingState = RunnerDoingState.idle;
    RunnerHaveItemState m_haveItemState = RunnerHaveItemState.not;
    RunnerAbnormalState m_abnormalState = RunnerAbnormalState.normal;

    public int m_playerNum;
    public bool isStan;
    public bool isDash;
    public bool isTouchItem;
    public bool isItimatsuActive;
    public float m_stanTime;
    public Rigidbody m_rigidBody;
    public GameObject m_player;
    public GameObject m_cameraObject;
    public GameObject m_itemObject;
    public GameObject m_haveItemObject;
    public GameObject m_plane;

    private void Awake()
    {
        isStan = false;
        isDash = false;
        isTouchItem = false;
        isItimatsuActive = false;
        deltaTime = Time.deltaTime;
        m_playerMove = new PlayerMove();
        m_playerInput = new PlayerInput();
        m_playerStatus = new PlayerStatus();
        m_playerCamera = new PlayerCamera();
        m_runnerSkil = new RunnerSkil();
        m_convenient = new ConvenientFunction();
        m_playerStatus.StatusInit(m_playerFlag);
    }

    // Update is called once per frame
    void Update () {
        AddCharVector(m_forward, m_back, m_down);
        m_convenient.Search(gameObject, m_plane, charVector[2], m_rayDistance, TagName.Plane);
        Button();
        BuffTime();
        RunnerStanTime();
        HealthController();
        m_playerInput.PController(m_playerNum);
        m_playerMove.RunnerMovement(m_player, m_rigidBody, m_doingState, m_playerInput, m_playerStatus);
        m_playerCamera.CameraMovement(m_cameraObject, m_player);
        Debug.Log("m_plane: " + m_plane);
        Debug.Log("itemObject: " + m_itemObject);
        Debug.Log("haveItemObject: " + m_haveItemObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != m_plane || other.gameObject.tag != TagName.Chaser)
        {
            isTouchItem = true;
            m_itemObject = other.gameObject;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isTouchItem = false;
        m_itemObject = null;
    }

    void AddCharVector(Vector3 forward, Vector3 back, Vector3 down)
    {
        charVector[0] = forward;
        charVector[1] = back;
        charVector[2] = down;
    }

    void Button()
    {
        // Rボタン押したらダッシュ
        if (m_playerInput.button_R)
        {
            m_doingState = RunnerDoingState.run;
        }
        else
        {
            m_doingState = RunnerDoingState.walk;
            m_playerStatus.health += Time.deltaTime;
        }

        // Eボタン押すとアイテム使用
        if (m_playerInput.button_E)
        {
            // アイテムを持っているかどうかの判定
            if (!m_playerStatus.isHaveItem)
            {
                // アイテムに触れていたら拾う
                if (isTouchItem)
                {
                    isTouchItem = false;
                    m_playerStatus.isHaveItem = true;
                    ItemCheck(m_itemObject);
                    Destroy(m_itemObject);
                }
            }
            else
            {
                HaveItemEvent(m_haveItemState);
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
                if (m_doingState == RunnerDoingState.run)
                {
                    m_playerStatus.health = m_playerStatus.isBuff ? m_playerStatus.maxHealth : m_playerStatus.health - deltaTime;
                }
            }
        }
        else
        {
            m_playerInput.button_R = false;
            m_doingState = RunnerDoingState.walk;
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
                m_haveItemState = RunnerHaveItemState.itimatsu;
                break;
            case ItemName.DRUG:
                m_haveItemState = RunnerHaveItemState.drug;
                break;
            case ItemName.AMULETS:
                m_haveItemState = RunnerHaveItemState.amulets;
                break;
            case TagName.TestTag:
                Debug.Log("テスト判定だよ");
                m_haveItemState = RunnerHaveItemState.test;
                break;
        }

        m_haveItemObject = GameController.Instance.itemStorage[0];
    }

    void HaveItemEvent(RunnerHaveItemState runnerHaveItemState)
    {
        switch (runnerHaveItemState)
        {
            case RunnerHaveItemState.itimatsu:
                GameObject itemItimatsu = Instantiate(m_haveItemObject, transform.position + charVector[0], transform.rotation);
                m_runnerSkil.ItimatsuEvent(itemItimatsu, isItimatsuActive);
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

        m_haveItemState = RunnerHaveItemState.not;
    }

    public void RunnerStan(RunnerAbnormalState state)
    {
        m_abnormalState = state;
        if (m_abnormalState == RunnerAbnormalState.stan)
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
            float currentSpeed = m_playerStatus.speed;
            m_playerStatus.speed = 0f;

            if (m_stanTime < 0)
            {
                m_stanTime = 0;
            }
            if (m_stanTime == 0)
            {
                // stanTimeが0になったらisStanをfalseにする
                // RunnerStanをnormalに変更
                isStan = false;
                m_abnormalState = RunnerAbnormalState.normal;
                Debug.Log("スタン終わったよ");

                // isStanがfalseに変更されたら、スタン処理終了
                // スタン終了時に保持してたスピードをプレイヤーのステータスへ戻す
                if (!isStan)
                {
                    m_playerStatus.speed = currentSpeed;
                }
            }
        }
    }
}
