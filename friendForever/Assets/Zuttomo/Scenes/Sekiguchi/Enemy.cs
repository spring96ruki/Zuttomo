using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float m_rayDistance = 10f;
    //上下する幅
    [SerializeField]
    float height = 1.0f;
    //上下の動きが変化する速さ
    [SerializeField]
    float moveChangeSpeed = 2.0f;
    Transform findObject;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_rayDistance))
        {
            findObject = hit.collider.gameObject.transform;
        }

        //移動
        Vector3 movePos = Vector3.zero;
        movePos = (
            findObject == null ?
            new Vector3(transform.position.x, height + Mathf.Cos(Time.time * moveChangeSpeed) * height, transform.position.z) :
            new Vector3(findObject.position.x, findObject.position.y + Mathf.Cos(Time.time * moveChangeSpeed) * height, findObject.position.z)
            );

        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime);
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.transform.position);
    }
}
