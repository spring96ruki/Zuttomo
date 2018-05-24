using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObject : MonoBehaviour {

    float m_rayDistance = 10f;

    public void FindPlayer(GameObject player)
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_rayDistance))
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
}
