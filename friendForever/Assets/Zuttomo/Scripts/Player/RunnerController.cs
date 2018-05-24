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

    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;

    bool isStan = false;

    RunnerState m_state;

	// Use this for initialization
	void Start () {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        RunnerStanTime();
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
        //m_runnerMove.Move();
        //m_runnerMove.Button();
    }
}
