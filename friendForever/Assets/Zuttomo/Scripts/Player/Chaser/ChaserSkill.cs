using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSkill
{
    public void StanSkil(GameObject chaserObject, RunnerController runnerController, PlayerStatus playerStatus)
    {
        float coolTime = playerStatus.stanCoolTime;
        float stanTime = playerStatus.stanTime;
        // Rayを飛ばして対象がいたらRunnerControllerへ登録
        if (coolTime == 0f)
        {
            Debug.Log("wan");
            // RunnerStateをStanに変更し、スタン時間を登録
            runnerController.RunnerStan(RunnerAbnormalState.stan);
            runnerController.m_stanTime = stanTime;
        }
    }

    public void ChaserInvisible(GameObject chaserObject, ChaserState state, float coolTime)
    {
        Color chaserColor = ChaserController.Instance.m_chaserColor;
        ChaserController.Instance.m_chaserState = state;
        Debug.Log("とぅっとぅるー");
        if (ChaserController.Instance.m_chaserState == ChaserState.invisible)
        {
            //Debug.Log("透明");
            if (coolTime == 0)
            {
                Debug.Log("透明");
                chaserObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(chaserColor.r, chaserColor.g, chaserColor.b, 0f);
                ChaserController.Instance.m_isInvisible = true;
            }
        }
    }
}
