using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    internal PlayerInput m_playerInput;

    public int m_playerNum;

    private void Awake()
    {
        m_playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update () {
        m_playerInput.PController(m_playerNum);
	}
}
