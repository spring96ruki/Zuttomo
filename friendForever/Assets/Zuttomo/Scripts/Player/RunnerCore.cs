using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCore : MonoBehaviour {

    protected RunnerStatus m_status;
    [HideInInspector]
    public Rigidbody m_rigidbody;


	// Use this for initialization
	void Start () {
        m_status = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
