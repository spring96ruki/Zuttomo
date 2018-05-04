using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Behavior {
    
	// Use this for initialization
	void Start () 
    {
	}
    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        base.PController();
        base.Move();
        base.Button();
    }
}
