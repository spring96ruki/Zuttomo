using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCore : MonoBehaviour {

    protected RunnerStatus m_status;
    public Rigidbody m_rigidbody;
    public static int m_getdemonNum;
    [SerializeField, Header("プレイヤーの番号")]
    public int runnerNum;
	public GameObject runner;
	public GameObject demon;

    // Use this for initialization
    void Start() {
        m_status = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
        int m_getdemonNum = SelectController.Getdemonplayer();
		Debug.Log ("demon:"+this);
        if (m_getdemonNum == runnerNum)
        {
			runner.SetActive(true);
			Debug.Log("プレイヤー" + runnerNum + ":demon");
        }
        else
        {
			demon.SetActive(true);
            Debug.Log("プレイヤー" + runnerNum + ":runner");
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}
