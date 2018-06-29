using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAnimator : MonoBehaviour
{

    RunnerInput m_runnerInput;
    RunnerStatus m_status;
    AnimatorManager m_animator;

    public bool m_action;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
        m_animator = new AnimatorManager(GetComponentsInChildren<Animator>(true));
    }

    public void MoveAnimation(float horizontal, float virtical)
    {
        if (m_runnerInput.Laxis_y >= 0.1f || m_runnerInput.Laxis_y <= -0.1f || m_runnerInput.Laxis_x >= 0.1f || m_runnerInput.Laxis_x <= -0.1f)
        {
            if (m_status.speed <= m_status.firstSpeed)
            {
                m_animator.MoveSpeed = 0.1f;
            }
            else if (m_status.speed >= m_status.firstSpeed)
            {
                m_animator.MoveSpeed = 1f;
            }
        }
        else
        {
            m_animator.MoveSpeed = 0f;
        }

        if(m_action)
        {
            m_status.speed = 0;
        }
    }

    public void PushAnimatio()
    {
        m_action = true;
        m_animator.SetPush();
        StartCoroutine(ActionAnimation(() =>
        {
            m_action = false;
        }));
    }

    IEnumerator ActionAnimation(System.Action callback)
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        //yield return new WaitForAnimation(m_animator.ActiveComponent, 0);
        callback();
    }
}