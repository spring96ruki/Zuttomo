using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {
	public int Kill_Count = 0;

    private void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Runner")
            {
                Debug.Log("捕まえたよ");
            other.gameObject.SetActive(false);
                GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();
        }
        }
}

