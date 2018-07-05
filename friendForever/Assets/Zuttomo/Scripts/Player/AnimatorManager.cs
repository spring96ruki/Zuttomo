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
        Pill,
        Down,
        Death,
        Barrier,
        Throw,
    }

    static readonly Dictionary<ParamName, int> m_paramHashDete = new Dictionary<ParamName, int>()
    {
        { ParamName.MoveSpeed,  Animator.StringToHash("MoveSpeed") },
        { ParamName.Push,       Animator.StringToHash("Push")},
        { ParamName.Kill,       Animator.StringToHash("Kill")},
        { ParamName.Pill,       Animator.StringToHash("Pill")},
        { ParamName.Down,       Animator.StringToHash("Down")},
        { ParamName.Death,      Animator.StringToHash("Death")},
        { ParamName.Barrier,    Animator.StringToHash("Barrier")},
        { ParamName.Throw,      Animator.StringToHash("Throw")},

    };

    public enum StateName
    {
        Idle,
        Move,
        Push,
        Kill,
        Pill,
        Down,
        Death,
        Barrier,
        Throw,
    }

    static readonly Dictionary<StateName, int> m_stateHashDate = new Dictionary<StateName, int>()
    {
        { StateName.Idle,       Animator.StringToHash("Base Layer.Idle") },
        { StateName.Move,       Animator.StringToHash("Base Layer.Move") },
        { StateName.Push,       Animator.StringToHash("Bass Leyer.Push")},
        { StateName.Kill,       Animator.StringToHash("Bass Leyer.Kill")},
        { StateName.Pill,       Animator.StringToHash("Bass Leyer.Pill")},
        { StateName.Down,       Animator.StringToHash("Bass Leyer.Down")},
        { StateName.Death,      Animator.StringToHash("Bass Leyer.Death")},
        { StateName.Barrier,    Animator.StringToHash("Bass Leyer.Barrier")},
        { StateName.Throw,      Animator.StringToHash("Bass Leyer.Throw")},
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
            var ret = m_animatorList.Find(a => a.gameObject.activeSelf);
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

    public void SetPill()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Pill]);
        }
    }

    public void SetBarrier()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Barrier]);
        }
    }

    public void SetThrow()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Throw]);
        }
    }

    public void SetDeath()
    {
        if (IsActive)
        {
            ActiveComponent.SetTrigger(m_paramHashDete[ParamName.Death]);
        }
    }

    public bool Down
    {
        get
        {
            return ActiveComponent.GetBool(m_paramHashDete[ParamName.Down]);
        }
        set
        {
            if (IsActive)
            {
                ActiveComponent.SetBool(m_paramHashDete[ParamName.Down], value);
            }
        }
    }

}
