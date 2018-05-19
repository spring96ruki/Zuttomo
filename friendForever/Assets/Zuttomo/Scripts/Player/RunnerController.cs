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

    RunnerCore runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;

    RunnerState m_state;

	// Use this for initialization
	void Start () {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        //m_runnerInput.PController(); 
	}

    public void RunnerStan(RunnerState state, float skilTime)
    {
        m_state = state;
        if (m_state == RunnerState.stan)
        {
            bool isSkil = true;
            --skilTime;
            if (isSkil == true) {
                for (float i = skilTime; i > 0; --i)
                {
                    runnerCore.m_rigidbody.velocity = Vector3.zero;
                    Debug.Log(skilTime);
                    if (skilTime == 0f)
                    {
                        isSkil = false;
                    }
                }
            }
            //if (skilTime > 0)
            //{
            //    runnerCore.m_rigidbody.velocity = Vector3.zero;
            //}
            Debug.Log("スタン終わったよ");
        }
    }

    private void FixedUpdate()
    {
        //m_runnerMove.Move();
        //m_runnerMove.Button();
    }
}
