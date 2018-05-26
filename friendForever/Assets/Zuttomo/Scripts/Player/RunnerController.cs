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
    public float stanTime{ get { return m_stanTime; } set { m_stanTime = value; } }
    public float State_timar;

    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
    Renderer rend;
    bool isStan = false;

    RunnerState m_state;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RunnerStanTime();
        m_runnerInput.PController();
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
        Debug.Log(stanTime);
        // isStanがtrueになったらスタン処理開始
        if (isStan == true)
        {
            --stanTime;
            Debug.Log("通った");

            // スタン処理
            m_rigidBody.constraints = RigidbodyConstraints.FreezePosition;

            if (stanTime < 0)
            {
                stanTime = 0;
            }
            if (stanTime == 0)
            {
                // stanTimeが0になったらisStanをfalseにする
                // RunnerStanをnormalに変更
                isStan = false;
                m_state = RunnerState.normal;
                Debug.Log("スタン終わったよ");

                // isStanがfalseに変更されたら、スタン処理終了
                if (isStan == false)
                {
                    m_rigidBody.constraints = RigidbodyConstraints.None;
                    m_rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
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
            State_timar += Time.deltaTime;
            if (State_timar >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Push")
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            State_timar = 0;
        }

        if (hit.gameObject.tag == "item")
        {
            Debug.Log("当たった");
            m_runnerMove.m_rigidbody.AddForce(Vector3.zero.normalized * 10f);
            Destroy(hit.gameObject);
        }
    }

    void OnCollisionStay(Collision col)
    {
        CheckEvent(col);
    }

    void CheckEvent(Collision col)
    {

        if (m_runnerStatus.ishave == false)
        {
            if (col.gameObject.name == "Sphere")
            {
                Debug.Log("市松人形だよ");
                if (m_runnerInput.button_B == true)
                {
                    m_runnerStatus.ishave = true;
                    m_runnerMove.m_item.tag = "item";
                    m_runnerMove.m_itemNum = 1;
                    Destroy(col.gameObject);
                }
            }

            if (col.gameObject.name == "Capsule")
            {
                Debug.Log("薬だよ");
                if (m_runnerInput.button_B == true)
                {
                    m_runnerStatus.ishave = true;
                    m_runnerMove.m_itemNum = 2;
                    Destroy(col.gameObject);
                }
            }

        }
        else
        {
            Debug.Log("これ以上は持てないよ");
        }
    }
}
