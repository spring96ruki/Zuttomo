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

    public void SkilTest(GameObject chaserObject)
    {
        float coolTime = ChaserController.Instance.m_coolTime;
        float maxCoolTime = ChaserController.Instance.m_maxCoolTime;
        float skilTime = ChaserController.Instance.m_stanTime;

        FindObject.Instance.FindPlayer(chaserObject);
        SkilStart_at_Method(coolTime, maxCoolTime, skilTime);
    }

    void SkilStart_at_Method(float coolTime, float maxCoolTime, float skilTime)
    {
        if (coolTime == 0f)
        {
            Debug.Log("wan");
            RunnerController.Instance.RunnerStan(RunnerState.stan, skilTime);
        }
    }
}
