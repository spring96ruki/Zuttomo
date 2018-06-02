using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 0.5f;
    Camera mainCamera;
    Vector3 firstPos;

	void Start () {
        mainCamera = Camera.main;
        firstPos = transform.position;
	}
	
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);

        if (move != Vector3.zero)
        {
            transform.position += move * moveSpeed;
            mainCamera.transform.position = transform.position + new Vector3(0, 8, -15);

            var diff = transform.position - firstPos;
            transform.rotation = Quaternion.LookRotation(diff);
            firstPos = transform.position;
        }
    }
}
