using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMove : MonoBehaviour {

    public GameObject cube;

    float speed = 10f;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxisRaw(GamePadName.xAxis);
        move.z = Input.GetAxisRaw(GamePadName.zAxis);
        rb.velocity = move * speed;
	}
}
