using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkill : MonoBehaviour {

    [SerializeField]
    GameObject m_runnerArea;
    RunnerInput m_runnerInput;
    RunnerStatus m_runnerStatus;
    RunnerController m_runnerController;
    RunnerMove m_runnerMove;

    int m_itemNum;
    [SerializeField]
    float m_EventaTimer;

	void Start()
	{
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerController = GetComponent<RunnerController>();
	}

	public void HitEvent(Collision hit)
    {
        switch(hit.gameObject.tag)
        {
            case TagName.Push:
                m_runnerStatus.isState = false;
                Debug.Log("当たった");
                m_runnerController.State_timer = 0;
                break;
            case TagName.Itimatu:
                Debug.Log("当たった");
                m_runnerStatus.isState = false;
                m_runnerController.State_timer = 0;
                Destroy(hit.gameObject);
                break;
        }

        if(hit.gameObject.name == "RunnerArea")
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            m_runnerController.State_timer = 0;
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
                    bullets.tag = "itimatu";
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

    IEnumerator DrugEvent()
    {
        float m_buffTimer;
        m_buffTimer = m_EventaTimer;
        while (m_buffTimer > 0)
        {
            m_buffTimer -= Time.deltaTime;
            m_runnerStatus.health = m_runnerStatus.maxHealth;
            yield return null;
            Debug.Log(m_buffTimer);
        }
    }

    IEnumerator Invincible()
    {
        //レイヤーをPlayerInvincibleに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        m_runnerArea.SetActive(true);
        //while文を3秒間ループする
        float m_invincibleTime = m_EventaTimer;
        while (m_invincibleTime > 0)
        {
            m_invincibleTime -= Time.deltaTime;
            yield return null;
            Debug.Log(m_invincibleTime);
        }
        m_runnerArea.SetActive(false);
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
