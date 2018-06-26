using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {
	private int KillRunnerNum = 0;
    

    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Runner")
        {
            Debug.Log("捕まえたよ");
            other.gameObject.SetActive(false);
            GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();
            //RunnerKill(KillRunnerNum);
            KillRunnerNum = other.GetComponent<RunnerInput>().runnerNum;
            GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().RunnerKill(KillRunnerNum);

            GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GetGimmickItem_player[KillRunnerNum - 1] = 0;
            for (int i = 0; i < 7; i++) {
                if (GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GetGimmickItem_word[i] == KillRunnerNum) {
                    Debug.Log("gimmick");
                }
            }

        }
    }
}

