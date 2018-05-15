using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : RunnerCore
{

    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;

	// Use this for initialization
	void Start () {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        m_runnerInput.PController();
        
	}

    private void FixedUpdate()
    {
        m_runnerMove.Move();
        m_runnerMove.Button();
    }
}
