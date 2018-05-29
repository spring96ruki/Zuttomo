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

    IEnumerator m_enumInvisible;

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
            m_enumInvisible = InvisibleSkil(chaserObject);
            if (m_enumInvisible != null)
            {
                bool ret = m_enumInvisible.MoveNext();
                if (ret == true)
                {
                    Debug.Log("逆転裁判");
                }
                else
                {
                    m_enumInvisible = null;
                }
            }
        }
    }

    IEnumerator InvisibleSkil(GameObject chaserObject)
    {
        var myColor = chaserObject.GetComponent<MeshRenderer>().material.color;
        InvisibleSkilAlpha(myColor, 0f, 1.0f);
        yield return null;
        InvisibleSkilAlpha(myColor, 1.0f, 0f);
    }

    void InvisibleSkilAlpha(Color myColor, float before, float after)
    {
        myColor = new Color(myColor.r, myColor.g, myColor.b, Mathf.Clamp(Time.time, before, after));
    }
}
