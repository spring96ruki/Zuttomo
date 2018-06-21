using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {
	public int Kill_Count = 0;
    

    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		if (this.GetComponent<RunnerController> ().ChaserFlag == true) {
            
                
                if (other.tag == "Player")
                {
                    Debug.Log("捕まえたよ");
                    Destroy(other.gameObject);
                    Kill_Count++;
                    if (Kill_Count == 3)
                    {
                        Debug.Log("3kill");
                        GameObject.Find("GameController").GetComponent<GameController>().EndGame();
                    }
                }
            }
	}
}

