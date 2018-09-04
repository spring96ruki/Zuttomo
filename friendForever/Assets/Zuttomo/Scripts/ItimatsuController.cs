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

    bool IsTouch;
    float m_rayDistance = 10f;
    float m_height = 0.2f;
    float m_moveSpeed = 3.0f;
    Vector3 nextPos;
    GameObject m_findPlayer;

    ConvenientFunction m_convenient;

    ItimatsuState m_itimatsuState = ItimatsuState.unknown;

    void Awake()
    {
        CheckMyself();
        m_convenient = new ConvenientFunction();

        if (m_itimatsuState == ItimatsuState.npc)
        {
            UpdateNextPos();
        }
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        IsTouch = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsTouch = false;
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
        //移動する位置
        Vector3 movePos = new Vector3(0f, 0f, 0f);

        if (m_findPlayer == null)
        {
            movePos = new Vector3(nextPos.x, m_height, nextPos.z);
            SearchPlayer();
        }
        else
        {
            ChasePlayer(ref movePos);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * m_moveSpeed);

        Quaternion lookRotation = Quaternion.LookRotation((movePos - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    void SearchPlayer()
    {
        Vector3 forward = new Vector3(0f, 0f, 1f);
        m_convenient.Search(gameObject, m_findPlayer, forward, m_rayDistance, TagName.Player);
        Debug.Log(m_findPlayer);

        //目的地に近づいた時
        if (Mathf.Abs(transform.position.x - nextPos.x) < 1.0f && Mathf.Abs(transform.position.z - nextPos.z) < 1.0f)
        {
            UpdateNextPos();
        }
    }

    void ChasePlayer(ref Vector3 movePos)
    {

        if (IsTouch)
        {
            //接触時に位置を知らせる
            Debug.Log(m_findPlayer.transform.position);
            movePos = m_findPlayer.transform.position;
        }
        else
        {
            movePos = new Vector3(m_findPlayer.transform.position.x, m_findPlayer.transform.position.y , m_findPlayer.transform.position.z);
        }

        if (Vector3.Distance(transform.position, m_findPlayer.transform.position) > m_rayDistance)
        {
            //ターゲットと一定距離離れたら見失う
            m_findPlayer = null;
        }

    }

    void UpdateNextPos()
    {
        nextPos = new Vector3(transform.position.x + Random.Range(-50.0f, 50.0f), m_height, transform.position.z + Random.Range(-10.0f, 10.0f));
    }
}
