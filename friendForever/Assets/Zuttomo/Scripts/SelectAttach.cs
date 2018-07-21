using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAttach : MonoBehaviour {

    public static int m_getChasernum;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        int m_getChaserNum = SelectController.GetChaserplayer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
