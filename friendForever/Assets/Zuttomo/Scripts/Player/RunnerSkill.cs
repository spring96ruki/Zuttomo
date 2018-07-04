using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkill : MonoBehaviour {

    [SerializeField]
    GameObject m_runnerArea;
    [SerializeField]
    GameObject m_UIController;
    UIController m_uIController;
    RunnerInput m_runnerInput;
    RunnerStatus m_runnerStatus;
    RunnerController m_runnerController;
    RunnerMove m_runnerMove;
    RunnerAnimator m_runnerAnimator;

    [SerializeField]
    float m_EventTime;
    public static int m_itemNum;
    float m_itemspeed = 1000;

	void Start()
	{
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerController = GetComponent<RunnerController>();
        //m_uIController = m_UIController.GetComponent<UIController>();
        m_runnerAnimator = GetComponent<RunnerAnimator>();
	}

	public void HitEvent(Collision hit)
    {
        switch(hit.gameObject.tag)
        {
            case TagName.Push:
                m_runnerStatus.isState = false;
                Debug.Log("当たった");
                break;
            case TagName.Itimatu:
                Debug.Log("当たった");
                m_runnerStatus.isState = false;
                Destroy(hit.gameObject);
                break;
        }

        if(hit.gameObject.name == "RunnerArea")
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
        }
    }

    public void CheckEvent(Collision check)
    {
        if (m_runnerStatus.ishave == false)
        {
            switch (check.gameObject.name)
            {
                case ItemName.itimatu:
                    Debug.Log("市松人形だよ");
                    if (m_runnerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        m_runnerStatus.ishave = true;
                        //アイテムの番号を1に変更
                        m_itemNum = 1;
                        m_uIController.ItemUIControll(m_itemNum, m_runnerStatus.runnerNum);
                        //拾ったアイテムを消去
                        Destroy(check.gameObject);
                    }
                    break;

                case ItemName.Drug:
                    Debug.Log("薬だよ");
                    if (m_runnerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        m_runnerStatus.ishave = true;
                        //アイテムの番号を2に変更
                        m_itemNum = 2;
                        m_uIController.ItemUIControll(m_itemNum, m_runnerStatus.runnerNum);
                        //拾ったアイテムを消去
                        Destroy(check.gameObject);
                    }
                    break;

                case ItemName.Ohuda:
                    Debug.Log("お札だよ");
                    if (m_runnerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        m_runnerStatus.ishave = true;
                        //アイテムの番号を3に変更
                        m_itemNum = 3;
                        m_uIController.ItemUIControll(m_itemNum, m_runnerStatus.runnerNum);
                        //拾ったアイテムを消去
                        Destroy(check.gameObject);
                    }
                    break;
            }
        }
    }

	public void ItemEvent()
	{
        if (m_runnerStatus.ishave == true)
        {
            switch (m_itemNum)
            {
                case 1:
                    Debug.Log("市松人形を投げたよ");
                    m_uIController.ItemUIControll(4, m_runnerStatus.runnerNum);
                    m_runnerAnimator.ThrowAnimation();
                    break;

                case 2:
                    Debug.Log("力が上がったよ");
                    m_runnerAnimator.PillAnimation();
                    m_runnerStatus.ishave = false;
                    break;

                case 3:
                    Debug.Log("無敵");
                    m_runnerAnimator.BarrierAnimation();
                    m_runnerStatus.ishave = false;
                    break;
            }
        }
	}

    public static int getItemNum()
    {
        return m_itemNum;
    }

    public void ItimatuEvent()
    {
        Vector3 force;
        GameObject bullets = Instantiate(m_runnerMove.m_item) as GameObject;
        force = gameObject.transform.forward * m_itemspeed;
        // Rigidbodyに力を加えて発射
        bullets.GetComponent<Rigidbody>().AddForce(force);
        // アイテムの位置を調整
        bullets.transform.position = m_runnerMove.m_FiringPosition.position;
        bullets.tag = "itimatu";
        m_runnerStatus.ishave = false;
    }

    IEnumerator DrugEvent()
    {
        Debug.Log("発動したよ");
        float m_buffTimer;
        //while文を指定した回数ループする
        m_buffTimer = m_EventTime;
        while (m_buffTimer > 0)
        {
            m_buffTimer -= Time.deltaTime;
            m_runnerStatus.health = m_runnerStatus.maxHealth;
            m_uIController.m_UI2List[m_runnerStatus.runnerNum - 1].fillAmount = m_buffTimer / m_EventTime;

            yield return null;
        }
        m_uIController.m_UI2List[m_runnerStatus.runnerNum - 1].fillAmount = 1;
        m_uIController.ItemUIControll(4, m_runnerStatus.runnerNum);
        Debug.Log("終わったよ");
    }

    IEnumerator Invincible()
    {
        Debug.Log("発動したよ");
        //レイヤーをPlayerInvincibleに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        m_runnerArea.SetActive(true);
        //while文を指定した回数ループする
        float m_invincibleTime = m_EventTime;
        while (m_invincibleTime > 0)
        {
            m_uIController.m_UI2List[m_runnerStatus.runnerNum - 1].fillAmount = m_invincibleTime / m_EventTime;
            m_invincibleTime -= Time.deltaTime;
            yield return null;
        }
        m_runnerArea.SetActive(false);
        m_uIController.m_UI2List[m_runnerStatus.runnerNum - 1].fillAmount = 1;
        m_uIController.ItemUIControll(4, m_runnerStatus.runnerNum);
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
        Debug.Log("効果が切れたよ");
    }
}
