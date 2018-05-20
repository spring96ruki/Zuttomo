using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {

    float dt;
    float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dt += Time.deltaTime;
        time += Time.time;
        //Debug.Log("dt: " + dt);
        //Debug.Log("Time.deltaTime: " + Time.deltaTime);
        //Debug.Log("time: " + time);
        //Debug.Log("Time.time" + Time.time);
	}
}
