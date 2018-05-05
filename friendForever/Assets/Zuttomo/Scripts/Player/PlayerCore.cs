using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour {

    protected PlayerStatus m_status;
    protected Rigidbody m_rigidbody;


	// Use this for initialization
	void Start () {
        m_status = GetComponent<PlayerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
