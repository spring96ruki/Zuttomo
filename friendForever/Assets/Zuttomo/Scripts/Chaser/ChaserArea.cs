using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {
    public int KillRunnerNum;
    public int m_Chasernum;

    void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Runner")
            {
                Debug.Log("領域内");
                ChaserController.Instance.m_isTakePlayer = true;
                //GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();
                KillRunnerNum = other.GetComponent<RunnerController>().m_playerNum;
                GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().RunnerKill(KillRunnerNum);
                other.gameObject.GetComponent<PlayerAnimator>().DeathAnimation();
                //other.gameObject.SetActive(false);
                //GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();
            }
    }
}

