using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour {

    public PlayerStatus m_status;
    public PlayerMove m_move;
    public PlayerInput m_input;
    public Rigidbody m_rigidbody;


	// Use this for initialization
	void Start () {
        m_status = GetComponent<PlayerStatus>();
        m_move = GetComponent<PlayerMove>();
        m_input = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
