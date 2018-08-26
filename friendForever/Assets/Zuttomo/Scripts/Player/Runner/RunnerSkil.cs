using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkil : ConvenientFunction
{

    public void ItimatsuEvent()
    {

    }

    public void DrugEvent(PlayerStatus runnerStatus , float drugTime)
    {

    }

    //IEnumerator DrugEvent(PlayerStatus runnerStatus, float drugTime)
    //{
    //    float buffTime;
    //    buffTime = drugTime;
    //    while (buffTime > 0)
    //    {
    //        buffTime -= Time.deltaTime;
    //        runnerStatus.health = runnerStatus.maxHealth;
    //        Debug.Log("バフってるよ");
    //        Debug.Log("buffTime: " + buffTime);
    //    }
    //    yield return null;
    //}
}