using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSkill : MonoBehaviour {

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
        //if ()
        //{
        //    StartCoroutine(SkilStart(ChaserController.Instance.m_coolTime, ChaserController.Instance.m_maxCoolTime, ChaserController.Instance.m_stanTime));
        //}
        //else
        //{
        //    Debug.Log("誰もいないよ");
        //}
    }

    public IEnumerator SkilStart()
    {
        float coolTime = ChaserController.Instance.m_coolTime;
        float maxCoolTime = ChaserController.Instance.m_maxCoolTime;
        float skilTime = ChaserController.Instance.m_stanTime;
        if ( coolTime == 0f) {
            Debug.Log("スタンさせたよ");
            ChaserController.Instance.Init();
            yield return new WaitForSeconds(skilTime);
            Debug.Log("スタン終わったよ");
        }
    }

    void SkilStart_at_Method(float coolTime, float maxCoolTime, float skilTime)
    {
        if (coolTime == 0f)
        {
            Debug.Log("スタンさせたよ");
            RunnerController.Instance.RunnerStan(RunnerState.stan, skilTime);
        }
    }

    //public void SkillStart()
    //{
    //    if (coolTime == 0)
    //    {
    //        Init(maxCoolTime);
    //        Debug.Log("スタンさせたよ");
    //    }
    //}

    public void RunnerStan()
    {

    }
}
