using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCore : MonoBehaviour {

    protected RunnerStatus m_status;
    public Rigidbody m_rigidbody;
    public static int m_getdemonNum;
    [SerializeField, Header("プレイヤーの番号")]
    public int runnerNum;

    // Use this for initialization
    void Start() { 
        m_status = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
        int m_getdemonNum = SelectController.Getdemonplayer();
        if (m_getdemonNum == runnerNum)
        {
            //GameObject.Find("testPlayer" + runnerNum);//SetActive(false);
            Debug.Log("runner" + runnerNum);
        }
        else
        {
            //GameObject.Find("testChaser" + runnerNum);//.SetActive(false);
            Debug.Log("demon" + runnerNum);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}
