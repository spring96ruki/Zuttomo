using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerCore{

    PlayerInput playerInput;
    PlayerMove playerMove;

	// Use this for initialization
	void Start () {
        playerInput = GetComponent<PlayerInput>();
        playerMove = GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        playerInput.PController();
        
	}

    private void FixedUpdate()
    {
        playerMove.Move();
        playerMove.Button();
    }
}
