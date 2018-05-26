using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
    Renderer rend;
    public float State_timer;

	void Start () {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        rend = GetComponent<Renderer>();
	}

    void Update()
    {
        m_runnerInput.PController();
    }

    private void FixedUpdate()
    {
        if (m_runnerStatus.isState == true)
        {
            m_runnerMove.Move();
            m_runnerMove.Button();
        } else {
            State_timer += Time.deltaTime;
            if (State_timer >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Push")
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            State_timer = 0;
        }

        if (hit.gameObject.tag == "item")
        {
            Debug.Log("当たった");
            m_runnerMove.m_rigidbody.AddForce(Vector3.zero.normalized * 10f);
            Destroy(hit.gameObject);
        }
    }

    void OnCollisionStay(Collision col)
	{
        CheckEvent(col);
	}

    void CheckEvent(Collision col){

        if (m_runnerStatus.ishave == false)
        {
            if (col.gameObject.name == "Sphere")
            {
                Debug.Log("市松人形だよ");
                if (m_runnerInput.button_B == true)
                {
                    m_runnerStatus.ishave = true;
                    m_runnerMove.m_item.tag = "item";
                    m_runnerMove.m_itemNum = 1;
                    Destroy(col.gameObject);
                }
            }

            if(col.gameObject.name == "Capsule")
            {
                if(m_runnerInput.button_B == true)
                {
                    Debug.Log("薬だよ");
                    m_runnerStatus.ishave = true;
                    m_runnerMove.m_itemNum = 2;
                    Destroy(col.gameObject);
                }
            }

        } else {
            Debug.Log("これ以上は持てないよ");
        }
    }
}
