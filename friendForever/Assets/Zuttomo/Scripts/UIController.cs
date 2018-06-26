using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

    public Image m_healthUI;
    //public List<GameObject> m_ItemList = new List<GameObject>();
    RunnerInput m_runnerInput;
    RunnerStatus m_status;
    public Image m_item;
    

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
