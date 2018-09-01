using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkil : ConvenientFunction
{

    public void ItimatsuEvent()
    {

    }

    public void DrugEvent(PlayerStatus runnerStatus)
    {
        runnerStatus.isBuff = true;
        runnerStatus.buffTime = runnerStatus.maxBuffTime;
    }

    public void AmuletsEvent()
    {
        // 死ぬのを一回防ぐ
    }
}