using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSkill : FindObject {

    static ChaserSkill instance;
    public static ChaserSkill Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ChaserSkill();
            }
            return instance;
        }
    }

    public void StanSkilStart(GameObject chaserObject)
    {

        //FindPlayer(chaserObject);

        float coolTime = ChaserController.Instance.m_stanCoolTime;
        float skilTime = ChaserController.Instance.m_stanTime;
        StanSkil(coolTime, skilTime);
    }

    void StanSkil(float coolTime, float skilTime)
    {

        // Rayを飛ばして対象がいたらRunnerControllerへ登録
        if (coolTime == 0f)
        {
            Debug.Log("wan");
            // RunnerStateをStanに変更し、スタン時間を登録
            RunnerController.Instance.RunnerStan(RunnerState.stan);
        }
    }

    public void ChaserInvisible(GameObject chaserObject, ChaserState state, float coolTime)
    {
        Color chaserColor = ChaserController.Instance.m_chaserColor;
        ChaserController.Instance.m_chaserState = state;
        Debug.Log("とぅっとぅるー");
        if (ChaserController.Instance.m_chaserState == ChaserState.invisible)
        {
            if (coolTime == 0)
            {
                chaserObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(chaserColor.r, chaserColor.g, chaserColor.b, 0f);
                ChaserController.Instance.m_isInvisible = true;
            }
        }
    }
}
