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

    bool m_isChaserAlpha = false;
    IEnumerator m_enumInvisible;


    private void Start()
    {

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

    public void ChaserInvisible(GameObject chaserObject, float coolTime)
    {
        Debug.Log("とぅっとぅるー");
        //m_chaserColor = new Color(m_chaserColor.r, m_chaserColor.g, m_chaserColor.b, Mathf.Lerp(1.0f, 0f, 0.1f));
        //Color chaserColor = chaserObject.GetComponent<MeshRenderer>().material.color;
        //if (coolTime == 0)
        //{
        //    chaserObject.GetComponent<MeshRenderer>().material.color = new Color(chaserColor.r, chaserColor.g, chaserColor.b, 0f);
        //}
    }
}
