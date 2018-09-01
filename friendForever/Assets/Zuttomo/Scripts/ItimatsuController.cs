using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItimatsuState
{
    unknown,
    item,
    npc
}

public class ItimatsuController : MonoBehaviour {

    public ItimatsuState m_itimatsuState = ItimatsuState.unknown;

    void Awake()
    {
        CheckMyself();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void CheckMyself()
    {
        switch (gameObject.tag)
        {
            case TagName.Item:
                m_itimatsuState = ItimatsuState.item;
                break;
            case TagName.NPC:
                m_itimatsuState = ItimatsuState.npc;
                break;
        }
    }

    void Movement()
    {

    }
}
