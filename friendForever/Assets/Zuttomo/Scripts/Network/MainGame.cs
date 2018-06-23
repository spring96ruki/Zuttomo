using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    public int m_playerNum;
    public GameObject [] playerArray;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    TransportTCP m_transportTCP;

    public enum GameState
    {
        HOST_SELECT = 0,
        MAIN_GAME,
        LEAVE,
        ERROR
    }
    public GameState m_state = GameState.HOST_SELECT;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
