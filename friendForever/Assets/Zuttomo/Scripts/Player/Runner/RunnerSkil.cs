using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkil
{
    public void ItimatsuEvent()
    {

    }

    public void DrugEvent(float drugTime, PlayerStatus runnerStatus)
    {
        float buffTime;
        buffTime = drugTime;
        while (buffTime > 0)
        {
            buffTime -= Time.deltaTime;
            runnerStatus.health = runnerStatus.maxHealth;
            if (buffTime <= 0)
            {
                runnerStatus.isHaveItem = false;
                break;
            }
        }
    }
}