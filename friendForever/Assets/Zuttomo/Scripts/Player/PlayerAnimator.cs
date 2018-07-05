using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    RunnerInput m_runnerInput;
    RunnerStatus m_status;
    AnimatorManager m_animatorManeger;
    RunnerSkill m_runnerSkill;
    [SerializeField]
    GameObject m_ChaserArea;

    public bool m_action;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
        m_runnerSkill = GetComponent<RunnerSkill>();
        m_animatorManeger = new AnimatorManager(GetComponentsInChildren<Animator>(true));
    }

    //走っている時のアニメーション
    public void MoveAnimation(float horizontal, float virtical)
    {
        if (m_runnerInput.Laxis_y >= 0.1f || m_runnerInput.Laxis_y <= -0.1f || m_runnerInput.Laxis_x >= 0.1f || m_runnerInput.Laxis_x <= -0.1f)
        {
            if (m_status.speed <= m_status.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 0.1f;
            }
            else if (m_status.speed >= m_status.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 1f;
            }
        }
        else
        {
            m_animatorManeger.MoveSpeed = 0f;
        }

        if(m_action)
        {
            m_status.speed = 0;
        }
    }

    //突き飛ばされた時のアニメーション
    public void DownAnimation()
	{
        if (m_status.isState == true)
        {
            m_animatorManeger.Down = false;
        } else {
            m_animatorManeger.Down = true;
        }
	}

    //突き飛ばした時のアニメーション
	public void PushAnimation()
    {
        m_action = true;
        m_animatorManeger.SetPush();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_action = false;
        }));
    }

    //薬のアイテムを使った時のアニメーション
    public void PillAnimation()
    {
        m_action = true;
        m_animatorManeger.SetPill();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_runnerSkill.StartCoroutine("DrugEvent");
            m_action = false;
        }));
    }

    //お札を使った時のアニメーション
    public void BarrierAnimation()
    {
        m_action = true;
        m_animatorManeger.SetBarrier();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_runnerSkill.StartCoroutine("Invincible");
            m_action = false;
        }));
    }

    //市松を投げる時のアニメーション
    public void ThrowAnimation()
    {
        m_action = true;
        m_animatorManeger.SetThrow();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_runnerSkill.ItimatuEvent();
            m_action = false;
        }));
    }

    //runnerを捕まえる時のアニメーション
    public void KillAnimation()
    {
        m_action = true;
        m_ChaserArea.SetActive(true);
        m_animatorManeger.SetKill();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            m_ChaserArea.SetActive(false);
            m_action = false;
        }));
    }

    //捕まった時のアニメーション
    public void DeathAnimation()
    {
        m_action = true;
        m_animatorManeger.SetDeath();
        //アニメーション再生後の処理
        StartCoroutine(ActionAnimation(() =>
        {
            gameObject.SetActive(false);
        }));
    }

    IEnumerator ActionAnimation(System.Action callback)
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        Debug.Log("終わったよ");
        callback();
    }

    public float GetAnimationTime
    {
        get
        {
            return m_animatorManeger.ActiveComponent.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
}