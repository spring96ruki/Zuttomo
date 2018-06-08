using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int m_getdemoNnum;
	public GameObject runner;
	public GameObject demon;

    // Use this for initialization
    void Start () {
        int m_getdemonNum = SelectController.Getdemonplayer();
		Debug.Log (m_getdemonNum);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
