using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvenientFunction{

    public void Search(GameObject myselfObject, GameObject objectStorage, Vector3 vector, float rayDistance, string tagName)
    {
        Ray ray = new Ray(myselfObject.transform.position, vector);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == tagName)
            {
                objectStorage = hit.collider.gameObject;
            }
        }
    }

    public void CoroutineMoveNext(IEnumerator coroutine)
    {
        IEnumerator localEnumeRator = coroutine;
        if (localEnumeRator != null)
        {
            localEnumeRator.MoveNext();
        }
    }
}
