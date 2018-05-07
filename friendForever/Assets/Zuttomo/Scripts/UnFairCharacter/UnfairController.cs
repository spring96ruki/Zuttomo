using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfairController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("憑いた");
        UnfairCharHauntPlayer(other.gameObject);
    }

    void UnfairCharHauntPlayer(GameObject player)
    {
        Debug.Log("ボクの効果を発揮するよ");
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
