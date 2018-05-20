using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 0.5f;
    Camera mainCamera;

	void Start () {
        mainCamera = Camera.main;
	}
	
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);

        if (move != Vector3.zero)
        {
            transform.position += move * moveSpeed;
            mainCamera.transform.position = transform.position + new Vector3(0, 8, -15);
        }
    }
}
