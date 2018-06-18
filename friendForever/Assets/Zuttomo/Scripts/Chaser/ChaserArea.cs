using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		if (this.GetComponent<RunnerController> ().ChaserFlag == true) {
			if (other.tag == "Player") {
				Debug.Log ("捕まえたよ");
				Destroy (other.gameObject);
			}
		}
    }
}
