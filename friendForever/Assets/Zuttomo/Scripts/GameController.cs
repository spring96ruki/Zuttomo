﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int m_getdemoNnum;
	public GameObject player;

    // Use this for initialization
    void Start () {
		for(int i = 0;i < 4;i++){
			var Player = Instantiate(player) as GameObject; 
			//Player = 
			Player.GetComponent<RunnerInput>().runnerNum = 100;
			if (m_getdemoNnum == i) {
				player.GetComponent<RunnerController>().demonFlag = true;
			}
		}
		int m_getdemonNum = SelectController.Getdemonplayer();
		Debug.Log (m_getdemonNum);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
