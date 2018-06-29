using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animator管理クラス
/// </summary>
[System.Serializable]
public class AnimatorManager
{
    public enum ParamName
    {
        MoveSpeed,
        Push,
        Kill,
    }

    static readonly Dictionary<ParamName, int> m_paramHashDete = new Dictionary<ParamName, int>()
    {
        { ParamName.MoveSpeed,  Animator.StringToHash("MoveSpeed") },
        { ParamName.Push,       Animator.StringToHash("Push")},
        { ParamName.Kill,       Animator.StringToHash("Kill")},
    };

    public enum StateName
    {
        Idle,
        Move,
        Push,
        Kill,
    }

    static readonly Dictionary<StateName, int> m_stateHashDate = new Dictionary<StateName, int>()
    {
        { StateName.Idle,       Animator.StringToHash("Base Layer.Idle") },
        { StateName.Move,       Animator.StringToHash("Base Layer.Move") },
        { StateName.Push,     Animator.StringToHash("Bass Leyer.Push")},
        { StateName.Kill,       Animator.StringToHash("Bass Leyer.Kill")},
    };

    List<Animator> m_animatorList;

    public AnimatorManager(Animator[] m_animator)
    {
        m_animatorList = new List<Animator>(m_animator);
    }

    public Animator ActiveComponent
    {
        get
        {
            var ret = m_animatorList.Find(async => async.gameObject.activeSelf);
            return ret ?? m_animatorList[0];
        }
    }

    public bool IsActive
    {
        get
        {
            return ActiveComponent.gameObject.activeSelf;
        }
    }

    public int GetStateHash(StateName m_stateName)
    {
        return m_stateHashDate[m_stateName];
    }

    public int CurrentStateHash
    {
        get
        {
            return ActiveComponent.GetCurrentAnimatorStateInfo(0).fullPathHash;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return ActiveComponent.GetFloat(m_paramHashDete[ParamName.MoveSpeed]);
        }
        set
        {
            if (IsActive)
            {
                ActiveComponent.SetFloat(m_paramHashDete[ParamName.MoveSpeed], value);
            }
        }
    }

    public void SetPush()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Push]);
        }
    }

    public void SetKill()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Kill]);
        }
    }
}
