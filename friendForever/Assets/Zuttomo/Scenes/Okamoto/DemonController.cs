using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : SingletonMono<DemonController>
{
    [HideInInspector]
    public float State_timar;

    Rigidbody m_rigidBody;
    RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    DemonMove m_DemonMove;
    RunnerStatus m_runnerStatus;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_DemonMove = GetComponent<DemonMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () 
    {
        //初期ステータス
        m_runnerStatus.firstSpeed = 10;
        m_runnerStatus.maxSpeed = 10;
        m_runnerStatus.speed = m_runnerStatus.firstSpeed;
        m_runnerStatus.health = 0;
        m_runnerStatus.maxHealth = 0;
        m_runnerStatus.isState = true;
        m_runnerStatus.ishave = false;
        m_runnerStatus.isBuff = false;
        m_runnerStatus.isInvincible = false;
        m_runnerStatus.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_runnerInput.PController();
	}

    private void FixedUpdate()
    {
        if (m_runnerStatus.isState == true)
        {
            m_DemonMove.Move();
            m_DemonMove.DemonButton();
        }
        else
        {
            State_timar += Time.deltaTime;
            Vector3 force;
            force = transform.position * 200;
            // Rigidbodyに力を加えて発射
            GetComponent<Rigidbody>().AddForce(force);
            if (State_timar >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == TagName.Push)
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            State_timar = 0;
        }
    }
}
