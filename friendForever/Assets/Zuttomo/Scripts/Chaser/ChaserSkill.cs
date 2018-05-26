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

        FindPlayer(chaserObject);

        float coolTime = ChaserController.Instance.m_coolTime;
        float maxCoolTime = ChaserController.Instance.m_maxCoolTime;
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
            RunnerController.Instance.RunnerStan(RunnerState.stan, skilTime);
        }
    }

    public void InvisibleSkilStart(GameObject chaserObject, float coolTime, float skilTime)
    {
        if (coolTime == 0f)
        {
            Debug.Log("うぃーっす");
            InvisibleSkil(chaserObject);
        }
    }

    void InvisibleSkil(GameObject chaserObject)
    {
        Color objectAlpha = chaserObject.GetComponent<MeshRenderer>().material.color;
        while (objectAlpha.a > 0)
        {
            Debug.Log("透明度: " + objectAlpha.a);
            objectAlpha.a -= 0.1f;
        }
    }

    //IEnumerator InvisibleSkil(GameObject chaserObject)
    //{
    //    yield return null;
    //}
}
