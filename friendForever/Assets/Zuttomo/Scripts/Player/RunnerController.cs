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
    public float State_timar;

    public float stanTime
    {
        get
        {
            return m_stanTime;
        }
        set
        {
            m_stanTime = value;
        }
    }

    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;

    bool isStan = false;

    RunnerState m_state;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	//void Start () {
 //       m_runnerInput = GetComponent<RunnerInput>();
 //       m_runnerMove = GetComponent<RunnerMove>();
 //       m_rigidBody = GetComponent<Rigidbody>();
 //       DontDestroyOnLoad(gameObject);
	//}
	
	// Update is called once per frame
	void Update () {
        //RunnerStanTime();
        m_runnerInput.PController();
        Debug.Log(isStan);
        Debug.Log(m_state);
	}

    public void RunnerStan(RunnerState state, float skilTime)
    {
        m_state = state;
        if (m_state == RunnerState.stan)
        {
            Debug.Log("スタンしたよ");
            isStan = true;
        }
    }

    public void RunnerStanTime()
    {
        Debug.Log(stanTime);
        if (isStan == true)
        {
            --stanTime;
            Debug.Log("通った");
            m_rigidBody.velocity = Vector3.zero;
            if (stanTime < 0)
            {
                stanTime = 0;
            }
            if (stanTime == 0)
            {
                isStan = false;
                m_state = RunnerState.normal;
                Debug.Log("スタン終わったよ");
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
                    m_runnerMove.ItemNum = 1;
                    Destroy(col.gameObject);
                }
            }

            if (col.gameObject.name == "Capsule")
            {
                Debug.Log("薬だよ");
                if (m_runnerInput.button_B == true)
                {
                    m_runnerStatus.ishave = true;
                    m_runnerMove.ItemNum = 2;
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
