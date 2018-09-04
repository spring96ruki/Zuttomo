using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkil
{

    public void ItimatsuEvent(GameObject itemItimatsu, bool isItimatsuActive)
    {
        isItimatsuActive = true;
        itemItimatsu.tag = TagName.Item;
        ItimatsuAddScript(itemItimatsu, isItimatsuActive);
    }

    void ItimatsuAddScript(GameObject itimatsu, bool isActive)
    {
        if (isActive)
        {
            itimatsu.AddComponent<ItimatsuController>();
        }
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