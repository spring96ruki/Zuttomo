using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour {

    protected RunnerStatus m_runnnerStatus;
    protected Rigidbody m_rigidbody;


    // Use this for initialization
    void Start()
    {
        m_runnnerStatus = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
