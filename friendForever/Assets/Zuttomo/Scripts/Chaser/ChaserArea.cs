using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserArea : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("捕まえたよ");
        Destroy(other.gameObject);
    }
}
