using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
    Renderer rend;
    public float State_timar;

	void Start () {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        rend = GetComponent<Renderer>();
	}

    void Update()
    {
        m_runnerInput.PController();
    }

    private void FixedUpdate()
    {
        if (m_runnerStatus.isState == true)
        {
            m_runnerMove.Move();
            m_runnerMove.Button();
            rend.material.color = Color.white;
        } else {
            State_timar += Time.deltaTime;
            if (State_timar >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Push")
        {
            m_runnerStatus.isState = false;
            rend.material.color = Color.blue;
            Debug.Log("当たった");
            State_timar = 0;
        }

        if (hit.gameObject.tag == "item")
        {
            Debug.Log("当たった");
        }
    }
}
