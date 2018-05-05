using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerCore{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_input.PController();
        
	}

    private void FixedUpdate()
    {
        m_move.Move();
        m_move.Button();
    }
}
