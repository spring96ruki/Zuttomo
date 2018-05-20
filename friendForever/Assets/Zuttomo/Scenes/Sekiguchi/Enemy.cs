using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float m_rayDistance = 10f;
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

        if(findObject != null)
        {
            transform.position = Vector3.Lerp(transform.position, findObject.position, Time.deltaTime);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.transform.position);
    }
}
