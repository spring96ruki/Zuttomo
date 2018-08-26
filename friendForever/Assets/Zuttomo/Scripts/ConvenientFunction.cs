using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvenientFunction{

    public void FindPlayer(GameObject player, float rayDistance)
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            switch (hit.collider.tag)
            {
                case TagName.Player_1P:
                    Debug.Log("1p見つけた");
                    break;
                case TagName.Player_2P:
                    Debug.Log("2p見つけた");
                    break;
                case TagName.Player_3P:
                    Debug.Log("3p見つけた");
                    break;
                case TagName.TestTag:
                    Debug.Log("TestTargetを見つけた");
                    break;
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
