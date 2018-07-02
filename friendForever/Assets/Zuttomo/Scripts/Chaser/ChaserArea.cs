using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Runner")
        {
            Debug.Log("領域内");
            ChaserController.Instance.m_isTakePlayer = true;
            //other.gameObject.SetActive(false);
            //GameObject.Find("GameController").GetComponent<GameController>().GamePhaseAdd();


        }
    }
}

