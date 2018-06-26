using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

    public Image m_healthUI;
    public Image m_item;
    RunnerInput m_runnerInput;
    RunnerStatus m_status;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
