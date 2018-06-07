using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int m_getdemoNnum;

    


    // Use this for initialization
    void Start () {
        int m_getdemonNum = SelectController.Getdemonplayer();
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
}
