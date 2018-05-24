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

    public bool isFind;

    public void Skil(GameObject chaserObject)
    {
        float coolTime = ChaserController.Instance.m_coolTime;
        float maxCoolTime = ChaserController.Instance.m_maxCoolTime;
        float skilTime = ChaserController.Instance.m_stanTime;

        FindPlayer(chaserObject);
        SkilStart(coolTime, maxCoolTime, skilTime);
    }

    void SkilStart(float coolTime, float maxCoolTime, float skilTime)
    {
        // Rayを飛ばして対象がいたらRunnerControllerへ登録
        if (coolTime == 0f)
        {
            Debug.Log("wan");
            // RunnerStateをStanに変更し、スタン時間を登録
            RunnerController.Instance.RunnerStan(RunnerState.stan, skilTime);
        }
    }
}
