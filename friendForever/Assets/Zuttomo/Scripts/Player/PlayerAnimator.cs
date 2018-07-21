using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    AnimatorManager m_animatorManeger;
    [SerializeField]
    GameObject m_ChaserArea;

    IEnumerator m_enumerator;

    public bool m_action;

    void Awake()
    {
        m_animatorManeger = new AnimatorManager(GetComponentsInChildren<Animator>(true));
    }

    //走っている時のアニメーション
    public void MoveAnimation(PlayerInput playerInput, PlayerStatus playerStatus , float horizontal, float virtical)
    {
        if (playerInput.Laxis_y >= 0.1f || playerInput.Laxis_y <= -0.1f || playerInput.Laxis_x >= 0.1f || playerInput.Laxis_x <= -0.1f)
        {
            if (playerStatus.speed <= playerStatus.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 0.1f;
            }
            else if (playerStatus.speed >= playerStatus.firstSpeed)
            {
                m_animatorManeger.MoveSpeed = 1f;
            }
        }
        else
        {
            m_animatorManeger.MoveSpeed = 0f;
        }

        if (m_action)
        {
            playerStatus.speed = 0;
        }
    }

    public void MakeCoroutine(IEnumerator enumerator)
    {
        if (enumerator != null)
        {
            bool ret = enumerator.MoveNext();
            if (ret)
            {
                Debug.Log("Next. ");
            }
            else
            {
                Debug.Log("finished");
                enumerator = null;
            }
        }
    }

    //突き飛ばされた時のアニメーション
    public void DownAnimation(PlayerStatus playerStatus)
	{
        if (playerStatus.isState == true)
        {
            m_animatorManeger.Down = false;
        }
        else
        {
            m_animatorManeger.Down = true;
        }
    }

    IEnumerator ActionAnimation(System.Action callback)
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        Debug.Log("アニメーション終わった");
        callback();
    }

    //突き飛ばした時のアニメーション
    public void PushAnimation()
    {
        m_action = true;
        m_animatorManeger.SetAnimation(ParamName.Push);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => { m_action = false; }));
        StartCoroutine(ActionAnimation(() =>
        {
            m_action = false;
        }));
    }

    //薬のアイテムを使った時のアニメーション
    public void PillAnimation()
    {
        m_action = true;
        m_animatorManeger.SetAnimation(ParamName.Pill);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => 
        {
            //MakeCoroutine(DrugEvent());
            m_action = false;
        }));
        //StartCoroutine(ActionAnimation(() =>
        //{
        //    m_runnerSkill.StartCoroutine("DrugEvent");
        //    m_action = false;
        //}));
    }

    //お札を使った時のアニメーション
    public void BarrierAnimation()
    {
        m_action = true;
        m_animatorManeger.SetAnimation(ParamName.Barrier);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => { m_action = false; }));
        //StartCoroutine(ActionAnimation(() =>
        //{
        //    //m_runnerSkill.StartCoroutine("Invincible");
        //    m_action = false;
        //}));
    }

    //市松を投げる時のアニメーション
    public void ThrowAnimation()
    {
        m_action = true;
        m_animatorManeger.SetAnimation(ParamName.Throw);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => { m_action = false; }));
        //StartCoroutine(ActionAnimation(() =>
        //{
        //    //m_runnerSkill.ItimatuEvent();
        //    m_action = false;
        //}));
    }

    //runnerを捕まえる時のアニメーション
    public void KillAnimation()
    {
        m_action = true;
        m_ChaserArea.SetActive(true);
        m_animatorManeger.SetAnimation(ParamName.Kill);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => { m_action = false; }));
        //StartCoroutine(ActionAnimation(() =>
        //{
        //    m_ChaserArea.SetActive(false);
        //    m_action = false;
        //}));
    }

    //捕まった時のアニメーション
    public void DeathAnimation(GameController gameController, ResultController resultController, RunnerController runnerControlerr)
    {
        m_action = true;
        m_animatorManeger.SetAnimation(ParamName.Death);
        //アニメーション再生後の処理
        MakeCoroutine(ActionAnimation(() => 
        {
            resultController.RunnerEnd(runnerControlerr.m_playerNum, false);
            gameController.GamePhaseAdd(runnerControlerr.m_playerNum);
            gameObject.SetActive(false);

        }));
        //StartCoroutine(ActionAnimation(() =>
        //{
        //    resultController.RunnerEnd(runnerControlerr.m_playerNum, false);
        //    gameController.GamePhaseAdd(runnerControlerr.m_playerNum);
        //    gameObject.SetActive(false);
        //}));
    }

    public float GetAnimationTime
    {
        get
        {
            return m_animatorManeger.ActiveComponent.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
    }
}