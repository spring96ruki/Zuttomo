using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkill : MonoBehaviour {


    [SerializeField]
    GameObject m_runnerArea;
    [SerializeField]
    GameObject m_UIController;
    RunnerInput m_runnerInput;
    RunnerStatus m_runnerStatus;
    RunnerController m_runnerController;
    RunnerMove m_runnerMove;
    UIController m_uIController;
    [SerializeField]
    float m_EventTime;

    public static int m_itemNum;

	void Start()
	{
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerController = GetComponent<RunnerController>();
        m_uIController = m_UIController.GetComponent<UIController>();
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
            Debug.Log("当たった");
            m_runnerStatus.isState = false;
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
                        m_uIController.ItemUIControll(m_itemNum,m_runnerInput.runnerNum);
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
                        m_uIController.ItemUIControll(m_itemNum, m_runnerInput.runnerNum);
                        //拾ったアイテムを消去
                        Destroy(check.gameObject);
                    }
                    break;

                case ItemName.Ohuda:
                    //Debug.Log("お札だよ");
                    if (m_runnerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        m_runnerStatus.ishave = true;
                        //アイテムの番号を3に変更
                        m_itemNum = 3;
                        m_uIController.ItemUIControll(m_itemNum, m_runnerInput.runnerNum);
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
                    Vector3 force;
                    GameObject bullets = Instantiate(m_runnerMove.m_item) as GameObject;
                    force = this.gameObject.transform.forward * m_runnerMove.m_itemspeed;
                    // Rigidbodyに力を加えて発射
                    bullets.GetComponent<Rigidbody>().AddForce(force);
                    // アイテムの位置を調整
                    bullets.transform.position = m_runnerMove.m_FiringPosition.position;
                    bullets.tag = "Itimatu";
                    m_uIController.ItemUIControll(4, m_runnerInput.runnerNum);
                    m_runnerStatus.ishave = false;
                    break;

                case 2:
                    Debug.Log("力が上がったよ");
                    StartCoroutine("DrugEvent");
                    m_runnerStatus.ishave = false;
                    break;

                case 3:
                    Debug.Log("無敵");
                    StartCoroutine("Invincible");
                    m_runnerStatus.ishave = false;
                    break;
            }
        }
	}

    public static int getItemNum()
    {
        return m_itemNum;
    }

    IEnumerator DrugEvent()
    {
        Debug.Log("始まったよ");
        float m_DrugTimer;
        m_DrugTimer = m_EventTime;
        while (m_DrugTimer > 0)
        {
            m_runnerStatus.health = m_runnerStatus.maxHealth;
            m_uIController.m_UI2List[m_runnerInput.runnerNum - 1].fillAmount = m_DrugTimer / m_EventTime;
            m_DrugTimer -= Time.deltaTime;
            yield return null;
        }
        m_uIController.m_UI2List[m_runnerInput.runnerNum - 1].fillAmount = 1;
        m_uIController.ItemUIControll(4, m_runnerInput.runnerNum);
        Debug.Log("終わったよ");
    }

    IEnumerator Invincible()
    {
        Debug.Log("始まったよ");
        //レイヤーをPlayerInvincibleに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        m_runnerArea.SetActive(true);
        //while文を3秒間ループする
        float m_invincibleTime = m_EventTime;
        while (m_invincibleTime > 0)
        {
            m_uIController.m_UI2List[m_runnerInput.runnerNum - 1].fillAmount = m_invincibleTime / m_EventTime;
            m_invincibleTime -= Time.deltaTime;
            yield return null;
        }
        m_runnerArea.SetActive(false);
        m_uIController.m_UI2List[m_runnerInput.runnerNum - 1].fillAmount = 1;
        m_uIController.ItemUIControll(4, m_runnerInput.runnerNum);
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
        Debug.Log("終わったよ");
    }
}
