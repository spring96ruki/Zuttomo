using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraCheck : MonoBehaviour {

    [SerializeField]
    private Vector3 cameraPrePosition;
    private Transform player;
    private RaycastHit hit;
    [SerializeField]
    private float cameraMoveSpeed = 3f;

    void Start()
    {
        player = GameObject.Find("Tpause").transform;
        cameraPrePosition = transform.localPosition;
    }

    void Update()
    {

        if (Physics.Linecast(player.position + Vector3.up, transform.position, out hit, LayerMask.GetMask("Field")))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, cameraMoveSpeed * Time.deltaTime);
        }
        else
        {
            if (transform.localPosition != cameraPrePosition)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, cameraPrePosition, cameraMoveSpeed * Time.deltaTime);
            }
        }
    }
}
